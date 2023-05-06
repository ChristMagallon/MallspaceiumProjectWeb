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
    public partial class AdvertisementPaymentApprovalDetails : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            if (!IsPostBack)
            {
                if (Request.QueryString["userEmail"] != null)
                {
                    string email = Request.QueryString["userEmail"];
                    getAdvertisementnPaymentDetails(email);
                }
            }
        }

        public async void getAdvertisementnPaymentDetails(String email)
        {
            // Use the shop name to retrieve the data from Firestore
            Query query = database.Collection("AdvertisementPaymentApproval").WhereEqualTo("userEmail", email);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // Get the first document from the query result (assuming there's only one matching document)
            DocumentSnapshot subDoc = snapshot.Documents.FirstOrDefault();
            if (subDoc != null)
            {
                // Retrieve the data from the document
                string transID = subDoc.GetValue<string>("transactionID");
                string uemail = subDoc.GetValue<string>("userEmail");
                string fname = subDoc.GetValue<string>("firstName");
                string lname = subDoc.GetValue<string>("lastName");
                string role = subDoc.GetValue<string>("userRole");
                string adsID = subDoc.GetValue<string>("advertisementID");
                string price = subDoc.GetValue<string>("price");
                string payment = subDoc.GetValue<string>("paymentFile");
                string adsProdID = subDoc.GetValue<string>("adsProductID");
                string adsProdName = subDoc.GetValue<string>("adsProductName");
                string adsProdImage = subDoc.GetValue<string>("adsProductImage");
                string adsProdDesc = subDoc.GetValue<string>("adsProductDesc");
                string adsProdShopName = subDoc.GetValue<string>("adsProductShopName");
                string adsProdDate = subDoc.GetValue<string>("adsProductDate");

                // Convert the image string to a byte array
                byte[] imageBytes;
                if (string.IsNullOrEmpty(adsProdImage))
                {
                    // If the image string is null or empty, use the default image instead
                    imageBytes = File.ReadAllBytes(Server.MapPath("/Images/no-image.jpg"));
                }
                else
                {
                    imageBytes = Convert.FromBase64String(adsProdImage);
                }
                // Set the image in the FileUpload control
                string imageBase64String = Convert.ToBase64String(imageBytes);
                string imageSrc = $"data:image/png;base64,{imageBase64String}";
                productImage.ImageUrl = imageSrc;

                // Display the data
                transactionIdLabel.Text = transID;
                emailLabel.Text = uemail;
                firstNameLabel.Text = fname;
                lastNameLabel.Text = lname;
                userRoleLabel.Text = role;
                advertisementIdLabel.Text = adsID;
                priceLabel.Text = price;
                imageHiddenField.Value = payment;
                advertisementProductIdLabel.Text = adsProdID;
                productNameLabel.Text = adsProdName;
                productImageHiddenField.Value = adsProdImage;
                descriptionLabel.Text = adsProdDesc;
                shopNameLabel.Text = adsProdShopName;
                dateLabel.Text = adsProdDate;

            }
            else
            {
                Response.Write("<script>alert('Error: Advertisement Not Found.');</script>");
            }

        }

        protected void viewpaymentDetailsButton_Click(object sender, EventArgs e)
        {
            viewPaymentImage();
        }

        public void viewPaymentImage()
        {
            string userEmail = emailLabel.Text;

            // Get the URL for the webform with the image control
            string url = "PopUpAdvetisementPaymentImage.aspx?userEmail=" + userEmail;

            // Set the height and width of the popup window
            int height = 800;
            int width = 800;

            // Open the popup window
            string script = $"window.open('{url}&height={height}&width={width}', '_blank', 'height={height},width={width},status=yes,toolbar=no,menubar=no,location=no,resizable=no');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupWindow", script, true);
        }

        protected void approveButton_Click(object sender, EventArgs e)
        {
            approvedPayment();
        }

        // Approved Payment Methodd
        private async void approvedPayment()
        {
            String userEmail = Request.QueryString["userEmail"]?.ToString() ?? "";

            // Get current date time and the expected expiration date
            DateTime currentDate = DateTime.UtcNow;
            string date = currentDate.ToString("yyyy-MM-dd HH:mm:ss");

            // Use the shop name to retrieve the data from Firestore
            Query query = database.Collection("AdvertisementPaymentApproval").WhereEqualTo("userEmail", userEmail);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // Get the first document from the query result (assuming there's only one matching document)
            DocumentSnapshot subDoc = snapshot.Documents.FirstOrDefault();
            if (subDoc != null)
            {
                string image = subDoc.GetValue<string>("adsProductImage");

                // Convert the image string to a byte array
                byte[] adsProdImage;
                if (string.IsNullOrEmpty(image))
                {
                    // If the image string is null or empty, use the default image instead
                    adsProdImage = File.ReadAllBytes(Server.MapPath("/Images/no-image.jpg"));
                }
                else
                {
                    adsProdImage = Convert.FromBase64String(image);
                }

                // Create a new collection reference for "AllAdvertisement"
                CollectionReference allAdsCollection = database.Collection("AllAdvertisement");

                // Create a new document reference with the ID of advertisementIdLabel.Text
                DocumentReference allAdsDoc = allAdsCollection.Document(advertisementIdLabel.Text);

                // Set the data for the new document in "AllAdvertisement" collection
                Dictionary<string, object> allAdsDataInsert = new Dictionary<string, object>
                {
                    {"userEmail", userEmail},
                    {"adsProdShopName", shopNameLabel.Text},
                    {"adsProdId", advertisementProductIdLabel.Text},
                    {"adsProdName", productNameLabel.Text},
                    {"adsProdDesc", descriptionLabel.Text},
                    {"adsProdImage", adsProdImage},
                    {"adsProdDate", date}
                };

                // Set the data in the Firestore document
                await allAdsDoc.SetAsync(allAdsDataInsert);

                // Create a new collection reference for "Users"
                CollectionReference usersCollection = database.Collection("Users");

                // Create a new document reference with the ID of advertisementIdLabel.Text
                DocumentReference userAdsDoc = usersCollection.Document(userEmail).Collection("Advertisement").Document(advertisementIdLabel.Text);

                // Set the data for the new document in "Users" collection
                Dictionary<string, object> userAdsDataInsert = new Dictionary<string, object>
                {
                    {"adsProdShopName", shopNameLabel.Text},
                    {"adsProdId", advertisementProductIdLabel.Text},
                    {"adsProdName", productNameLabel.Text},
                    {"adsProdDesc", descriptionLabel.Text},
                    {"adsProdImage", adsProdImage},
                    {"adsProdDate", date}
                };

                // Set the data in the Firestore document
                await userAdsDoc.SetAsync(userAdsDataInsert);

                removeDocumentID(userEmail);
                sendApprovedNotif();

                // Display a message
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('A advertisement payment has been approved.');", true);
                // Redirect to another page after a delay
                string url = "AdvertisementPaymentApproval.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 500);", true);
            }
        }

        // Send notification to the user if approved
        private async void sendApprovedNotif()
        {
            String userEmail = Request.QueryString["userEmail"]?.ToString() ?? "";

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
                string documentName = "Payment successfully approved! " + notifID;
                DocumentReference notifRef = database.Collection("Users").Document(userEmail).Collection("Notification").Document(documentName);
                Dictionary<string, object> data = new Dictionary<string, object>
                {
                    {"notifDetail", "Your advertised product is now finally posted. Go check it out!" },
                    {"notifImage", "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" }
                };
                await notifRef.SetAsync(data);
            }
        }

        // Delete the document from the collection
        private async Task removeDocumentID(string userEmail)
        {
            // Get reference to the user's document
            DocumentReference userRef = database.Collection("AdvertisementPaymentApproval").Document(userEmail);

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
            String userEmail = Request.QueryString["userEmail"]?.ToString() ?? "";

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
                string documentName = "Payment is disapproved! " + notifID;
                DocumentReference notifRef = database.Collection("Users").Document(emailLabel.Text).Collection("Notification").Document(documentName);
                Dictionary<string, object> data = new Dictionary<string, object>
                {
                    {"notifDetail", "If there's something wrong with your payment, please contact us." },
                    {"notifImage", "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" }
                };
                await notifRef.SetAsync(data);

                // Display a message
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('A subscription payment has been disapproved.');", true);
                // Redirect to another page after a delay
                string url = "AdvertisementPaymentApproval.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 500);", true);
            }
        }
    }
}