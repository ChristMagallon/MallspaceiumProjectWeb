using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.form
{
    public partial class CheckoutSummaryPage : System.Web.UI.Page
    {
        FirestoreDb db;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("mallspaceium");

            retrieveSubscriptionData();
        }

        protected void ProceedButton_Click(object sender, EventArgs e)
        {
            purchaseSubscription();
        }

        private void retrieveSubscriptionData()
        {
            // Retrieve the subscription data from the session variable
            SubscriptionData subscriptionData = (SubscriptionData)Session["SubscriptionData"];

            // Use the subscription data as needed
            string price = subscriptionData.Price;

            SubscriptionPriceLabel.Text = price;
            TotalPayLabel.Text = price;
        }

        private async void purchaseSubscription()
        {
            // Retrieve the subscription data from the session variable
            SubscriptionData subscriptionData = (SubscriptionData)Session["SubscriptionData"];

            // Use the subscription data as needed
            string subscriptionType = subscriptionData.SubscriptionType;
            string price = subscriptionData.Price;
            string userEmail = subscriptionData.UserEmail;
            string firstName = subscriptionData.FirstName;
            string lastName = subscriptionData.LastName;
            string userRole = subscriptionData.UserRole;

            // Query the Firestore collection for a user with a specific email address
            CollectionReference usersRef = db.Collection("Users");
            DocumentReference docRef = usersRef.Document((string)Application.Get("usernameget"));

            // Retrieve the document data asynchronously
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            // Generate random ID number
            Random random = new Random();
            int randomIDNumber = random.Next(100000, 1000000);
            string subscriptionID = "SUB" + randomIDNumber.ToString();

            // Generate transaction number by get the current date and time
            DateTime now = DateTime.UtcNow;
            // Generate a random number between 1000 and 9999
            int randomNumber = random.Next(1000, 10000);
            // Combine the date and random number to create the transaction number
            string transactionID = string.Format("{0:yyyyMMddHHmmss}{1}", now, randomNumber);

            // Create a new collection reference
            DocumentReference documentRef = db.Collection("SubscriptionPaymentApproval").Document(userEmail);

            if (PaymentFileUpload.HasFile)
            {
                //Create an instance of Bitmap from the uploaded file using the FileUpload control
                Bitmap image = new Bitmap(PaymentFileUpload.PostedFile.InputStream);
                MemoryStream stream = new MemoryStream();
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] bytes = stream.ToArray();

                //Convert the Bitmap image to a Base64 string
                string base64String = Convert.ToBase64String(bytes);

                // Set the data for the new document
                Dictionary<string, object> data = new Dictionary<string, object>
                    {
                        { "transactionID", transactionID},
                        { "subscriptionID", subscriptionID},
                        { "subscriptionType", subscriptionType},
                        { "price", price},
                        { "paymentFile", base64String},
                        { "userEmail", userEmail},
                        { "firstName", firstName},
                        { "lastName", lastName},
                        { "userRole", userRole}
                    };

                // Set the data in the Firestore document
                await documentRef.SetAsync(data);
            }

            // Redirect back to subscription page
            if (userRole == "Shopper")
            {
                Response.Redirect("~/Shopper/SubscriptionPage.aspx", false);
            }
            else if (userRole == "ShopOwner")
            {
                Response.Redirect("~/ShopOwner/SubscriptionPage.aspx", false);
            }   
        }
    }
}