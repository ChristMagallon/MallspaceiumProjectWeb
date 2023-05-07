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
                    {"adsProdImage", productImageHiddenField.Value},
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
                    {"adsProdImage", productImageHiddenField.Value},
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