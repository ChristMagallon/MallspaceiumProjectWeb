using System;
using System.Collections.Generic;
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
            string subscriptionID = subscriptionData.SubscriptionID;
            string subscriptionType = subscriptionData.SubscriptionType;
            string price = subscriptionData.Price;
            string userEmail = subscriptionData.UserEmail;
            string userRole = subscriptionData.UserRole;
            string startDate = subscriptionData.StartDate;
            string endDate = subscriptionData.EndDate;
            string status = subscriptionData.Status;

            SubscriptionPriceLabel.Text = price;
            TotalPayLabel.Text = price;


            // Get the current date and time
            DateTime now = DateTime.UtcNow;

            // Generate a random number between 1000 and 9999
            Random random = new Random();
            int randomNumber = random.Next(1000, 10000);

            // Combine the date and random number to create the transaction number
            string transactionNumber = string.Format("{0:yyyyMMddHHmmss}{1}", now, randomNumber);
            TransactionNoLabel.Text = transactionNumber;
        }

        private async void purchaseSubscription()
        {
            // Retrieve the subscription data from the session variable
            SubscriptionData subscriptionData = (SubscriptionData)Session["SubscriptionData"];

            // Use the subscription data as needed
            string subscriptionID = subscriptionData.SubscriptionID;
            string subscriptionType = subscriptionData.SubscriptionType;
            string price = subscriptionData.Price;
            string userEmail = subscriptionData.UserEmail;
            string userRole = subscriptionData.UserRole;
            string startDate = subscriptionData.StartDate;
            string endDate = subscriptionData.EndDate;
            string status = subscriptionData.Status;

            // Query the Firestore collection for a user with a specific email address
            CollectionReference usersRef = db.Collection("Users");
            DocumentReference docRef = usersRef.Document((string)Application.Get("usernameget"));

            // Retrieve the document data asynchronously
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            // Check if the document exists
            if (snapshot.Exists)
            {
                // Create a new collection reference
                DocumentReference documentRef = db.Collection("AdminManageSubscription").Document(userEmail);

                // Check if the document exists
                DocumentSnapshot documentSnapshot = await documentRef.GetSnapshotAsync();
                if (documentSnapshot.Exists)
                {
                    // Document exists, update the fields
                    Dictionary<string, object> dataUpdate = new Dictionary<string, object>
                    {
                        {"subscriptionType", subscriptionType},
                        {"price", price},
                        {"startDate", startDate},
                        {"endDate", endDate},
                        {"status", status}
                    };

                    // Update the data in the Firestore document
                    await documentRef.UpdateAsync(dataUpdate);
                }
                else
                {
                    // Document does not exist, create a new document and set the data
                    Dictionary<string, object> dataInsert = new Dictionary<string, object>
                    {
                        {"subscriptionID", subscriptionID},
                        {"subscriptionType", subscriptionType},
                        {"price", price},
                        {"userEmail", userEmail},
                        {"userRole", userRole},
                        {"startDate", startDate},
                        {"endDate", endDate},
                        {"status", status}
                    };
                    // Set the data in the Firestore document
                    await documentRef.SetAsync(dataInsert);
                }
            }
            else
            {
                // Document does not exist
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