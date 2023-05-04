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
                    {"shopImage", shopImage},
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
                    {"certifiedShopOwner", true },
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