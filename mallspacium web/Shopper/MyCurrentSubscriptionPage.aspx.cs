using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.Shopper
{
    public partial class MyCurrentSubscription : System.Web.UI.Page
    {
        FirestoreDb db;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("mallspaceium");

            getCurrentSubDetails();
        }

        public async void getCurrentSubDetails()
        {
            // Query the Firestore collection for a user with a specific email address
            CollectionReference usersRef = db.Collection("AdminManageSubscription");
            DocumentReference docRef = usersRef.Document((string)Application.Get("usernameget"));

            // Retrieve the document data asynchronously
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            // Check if the document exists
            if (snapshot.Exists)
            {
                // Get the data as a Dictionary
                Dictionary<string, object> data = snapshot.ToDictionary();
                // Access the specific field you want
                SubscriptionTypeLabel.Text = data["subscriptionType"].ToString();
                PriceLabel.Text = data["price"].ToString();
                StatusLabel.Text = data["status"].ToString();
            }
            else
            {
                // Document does not exist
            }
        }

        protected void CancelSubscriptionButton_Click(object sender, EventArgs e)
        {
            cancelSubscription();
        }

        public async void cancelSubscription()
        {
            String subscriptionType = "Free";
            String subscriptionPrice = "0.00";
            String status = "Cancelled";
            // Query the Firestore collection for a user with a specific email address
            CollectionReference usersRef = db.Collection("Users");
            DocumentReference docRef = usersRef.Document((string)Application.Get("usernameget"));

            // Retrieve the document data asynchronously
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            // Check if the document exists
            if (snapshot.Exists)
            {
                // Get the data as a Dictionary
                Dictionary<string, object> data = snapshot.ToDictionary();
                // Access the specific field you want
                string userEmail = data["email"].ToString();
                string userRole = data["userRole"].ToString();

                // Generate random ID number
                Random random = new Random();
                int randomIDNumber = random.Next(100000, 999999);
                string subscriptionID = "SUB" + randomIDNumber.ToString();

                // Get current date time and the expected expiration date
                DateTime currentDate = DateTime.UtcNow;
                DateTime expirationDate = currentDate.AddMonths(3);
                string startDate = currentDate.ToString("yyyy-MM-dd HH:mm:ss");
                string endDate = expirationDate.ToString("yyyy-MM-dd HH:mm:ss");

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
                        {"price", subscriptionPrice},
                        {"subscriptionID", subscriptionID},
                        {"startDate", FieldValue.Delete},
                        {"endDate", FieldValue.Delete},
                        {"status", status}
                    };

                    // Update the data in the Firestore document
                    await documentRef.UpdateAsync(dataUpdate);
                }
                else
                {
                    // Set the data for the new document
                    Dictionary<string, object> dataInsert = new Dictionary<string, object>
                    {
                        {"subscriptionID", subscriptionID},
                        {"subscriptionType", subscriptionType},
                        {"price", subscriptionPrice},
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
            Response.Redirect("~/Shopper/SubscriptionPage.aspx", false);
        }
    }
}