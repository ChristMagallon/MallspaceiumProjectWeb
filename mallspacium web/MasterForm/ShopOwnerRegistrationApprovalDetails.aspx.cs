using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.MasterForm
{
    public partial class ShopOwnerRegistrationApprovalDetails : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            if (!IsPostBack)
            {
                if (Request.QueryString["email"] != null)
                {
                    string email = Request.QueryString["email"];
                    getShopOwnerRegistrationDetails(email);
                }
            }
        }

        public async void getShopOwnerRegistrationDetails(String email)
        {
            // Use the shop name to retrieve the data from Firestore
            Query query = database.Collection("ShopOwnerRegistrationApproval").WhereEqualTo("email", email);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // Get the first document from the query result (assuming there's only one matching document)
            DocumentSnapshot shopDoc = snapshot.Documents.FirstOrDefault();
            if (shopDoc != null)
            {
                // Retrieve the data from the document
                string userID = shopDoc.GetValue<string>("userID");
                string fname = shopDoc.GetValue<string>("firstName");
                string lname = shopDoc.GetValue<string>("lastName");
                string image = shopDoc.GetValue<string>("shopImage");
                string shopName = shopDoc.GetValue<string>("shopName");
                string desc = shopDoc.GetValue<string>("shopDescription");
                string phoneNumber = shopDoc.GetValue<string>("phoneNumber");
                string address = shopDoc.GetValue<string>("address");
                string username = shopDoc.GetValue<string>("username");
                string role = shopDoc.GetValue<string>("userRole");
                string date = shopDoc.GetValue<string>("dateCreated");

                // Convert the image string to a byte array
                byte[] imageBytes;
                if (string.IsNullOrEmpty(image))
                {
                    // If the image string is null or empty, use the default image instead
                    imageBytes = File.ReadAllBytes(Server.MapPath("/Images/no-image.jpg"));
                }
                else
                {
                    imageBytes = Convert.FromBase64String(image);
                }
                // Set the image in the FileUpload control
                string imageBase64String = Convert.ToBase64String(imageBytes);
                string imageSrc = $"data:image/png;base64,{imageBase64String}";
                shopImage.ImageUrl = imageSrc;

                // Display the data
                userIdLabel.Text = userID;
                emailLabel.Text = email;
                firstNameLabel.Text = fname;
                lastNameLabel.Text = lname;
                shopNameLabel.Text = shopName;
                descriptionLabel.Text = desc;
                phoneNumberLabel.Text = phoneNumber;
                addressLabel.Text = address;
                usernameLabel.Text = username;
                userRoleLabel.Text = role;
                dateLabel.Text = date;
                shopImageHiddenField.Value = image;
            }
            else
            {
                Response.Write("<script>alert('Error: Advertisement Not Found.');</script>");
            }
        }

        protected void viewShopPermitDetailsButton_Click(object sender, EventArgs e)
        {
            viewPermitImage();
        }

        public void viewPermitImage()
        {
            string email = emailLabel.Text;

            // Get the URL for the webform with the image control
            string url = "PopUpShopOwnerPermitImage.aspx?email=" + email;

            // Set the height and width of the popup window
            int height = 800;
            int width = 800;

            // Open the popup window
            string script = $"window.open('{url}&height={height}&width={width}', '_blank', 'height={height},width={width},status=yes,toolbar=no,menubar=no,location=no,resizable=no');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupWindow", script, true);
        }

        protected void approveButton_Click(object sender, EventArgs e)
        {
            approvedRegistration();
        }

        public async void approvedRegistration()
        {
            String userEmail = Request.QueryString["email"]?.ToString() ?? "";

            // Use the shop name to retrieve the data from Firestore
            Query query = database.Collection("ShopOwnerRegistrationApproval").WhereEqualTo("email", userEmail);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // Get the first document from the query result (assuming there's only one matching document)
            DocumentSnapshot shopDoc = snapshot.Documents.FirstOrDefault();
            if (shopDoc != null)
            {
                string image = shopDoc.GetValue<string>("shopImage");
                string password = shopDoc.GetValue<string>("password");
                string confirmPassword = shopDoc.GetValue<string>("confirmPassword");
                bool verified = shopDoc.GetValue<bool>("verified");

                // Convert the image string to a byte array
                byte[] shopImage;
                if (string.IsNullOrEmpty(image))
                {
                    // If the image string is null or empty, use the default image instead
                    shopImage = File.ReadAllBytes(Server.MapPath("/Images/no-image.jpg"));
                }
                else
                {
                    shopImage = Convert.FromBase64String(image);
                }

                // Create a new collection reference
                DocumentReference doc = database.Collection("Users").Document(userEmail);

                // Set the data for the new document
                Dictionary<string, object> data = new Dictionary<string, object>
                {
                    {"userID", userIdLabel.Text},
                    {"firstName", firstNameLabel.Text},
                    {"lastName", lastNameLabel.Text},
                    {"shopImage", image},
                    {"shopName", shopNameLabel.Text},
                    {"shopDescription", descriptionLabel.Text},
                    {"email", emailLabel.Text},
                    {"phoneNumber", phoneNumberLabel.Text},
                    {"address", addressLabel.Text},
                    {"username", usernameLabel.Text},
                    {"password", password},
                    {"confirmPassword", confirmPassword},
                    {"userRole", userRoleLabel.Text},
                    {"dateCreated", dateLabel.Text },
                    {"counterPopularity", 0 },
                    {"certifiedShopOwner", true },
                    {"userNotif", true },
                    {"verified", verified}
                };

                // Set the data in the Firestore document
                await doc.SetAsync(data);
                removeDocumentID(userEmail);
                sendApprovedNotif();

                // Display a message
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('A shop owner account registration has been approved.');", true);
                // Redirect to another page after a delay
                string url = "ShopOwnerRegistrationApproval.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 500);", true);
            }
        }

        // Send notification to the user if approved
        private async void sendApprovedNotif()
        {
            String userEmail = Request.QueryString["email"]?.ToString() ?? "";

            // Query the Firestore collection for a user with a specific email address
            CollectionReference usersRef = database.Collection("Users");
            DocumentReference docRef = usersRef.Document(userEmail);

            // Retrieve the document data asynchronously
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            // Check if the document exists
            if (snapshot.Exists)
            {
                //auto generated unique id
                Random random = new Random();
                int randomIDNumber = random.Next(100000, 999999);
                string notifID = "NOTIF" + randomIDNumber.ToString();

                // Specify the name of the document using a variable or a string literal
                string documentName = "Registration is completed " + notifID;
                DocumentReference notifRef = database.Collection("Users").Document(userEmail).Collection("Notification").Document(documentName);
                Dictionary<string, object> data = new Dictionary<string, object>
                {
                    {"notifDetail", "Your account registration " + userEmail + " is successfully approved!" },
                    {"notifImage", "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" }
                };
                await notifRef.SetAsync(data);
            }
        }

        // Delete the document from the collection
        private async Task removeDocumentID(string userEmail)
        {
            // Get reference to the user's document
            DocumentReference userRef = database.Collection("ShopOwnerRegistrationApproval").Document(userEmail);

            // Check if the document exists
            DocumentSnapshot userSnapshot = await userRef.GetSnapshotAsync();
            if (!userSnapshot.Exists)
            {
                return;
            }

            // Delete the email document
            await userRef.DeleteAsync();
        }


        protected void disapproveButton_Click(object sender, EventArgs e)
        {
            sendDisapprovedNotif();
        }

        private async void sendDisapprovedNotif()
        {    
            String userEmail = Request.QueryString["email"]?.ToString() ?? "";
            removeDocumentID(userEmail);


            // Display a message
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('Shop owner account registration has been disapproved.');", true);
            // Redirect to another page after a delay
            string url = "AdvertisementPaymentApproval.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 500);", true);
        }
    }
}