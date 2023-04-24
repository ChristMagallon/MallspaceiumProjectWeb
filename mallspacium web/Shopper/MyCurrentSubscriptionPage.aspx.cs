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
                string subscriptionType = data["subscriptionType"].ToString();
                SubscriptionTypeLabel.Text = subscriptionType;
                PriceLabel.Text = data["price"].ToString();
                StatusLabel.Text = data["status"].ToString();

                // Check the subscription type
                if (subscriptionType == "Free")
                {
                    CancelSubscriptionButton.Enabled = false;
                }
                else
                {
                    CancelSubscriptionButton.Enabled = true;
                }
            }
        }

        protected void CancelSubscriptionButton_Click(object sender, EventArgs e)
        {
            cancelSubscription();
        }

        public async void cancelSubscription()
        {
            string subscriptionType = "Free";
            string subscriptionPrice = "0.00";
            string status = "Cancelled";
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

                // Create a new collection reference
                DocumentReference subscriptionRef = db.Collection("AdminManageSubscription").Document(userEmail);

                // Check if the document exists
                DocumentSnapshot subscriptionSnapshot = await subscriptionRef.GetSnapshotAsync();
                if (subscriptionSnapshot.Exists)
                {
                    // Document exists, update the fields
                    Dictionary<string, object> dataUpdate = new Dictionary<string, object>
                    {
                        {"subscriptionType", subscriptionType},
                        {"price", subscriptionPrice},
                        {"startDate", "n/a"},
                        {"endDate", "n/a"},
                        {"status", status}
                    };

                    // Update the data in the Firestore document
                    await subscriptionRef.UpdateAsync(dataUpdate);
                    Response.Write("<script>alert('Your subscription has been cancelled and your account has been downgraded to the Free subscription.');</script>");
                }
                else
                {
                    // Set the data for the new document
                    Dictionary<string, object> dataInsert = new Dictionary<string, object>
                    {
                        {"subscriptionType", subscriptionType},
                        {"price", subscriptionPrice},
                        {"userEmail", userEmail},
                        {"userRole", userRole},
                        {"startDate", "n/a"},
                        {"endDate", "n/a"},
                        {"status", status}
                    };

                    // Set the data in the Firestore document
                    await subscriptionRef.SetAsync(dataInsert);
                }
            }
            Response.Redirect("~/Shopper/SubscriptionPage.aspx", false);
        }
    }
}