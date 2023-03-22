using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mallspacium_web.ShopOwner
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        FirestoreDb db;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("mallspaceium");
        }

        protected void BasicSubButton_Click(object sender, EventArgs e)
        {
            basicSubscription();
        }

        protected void AdvanceSubButton_Click(object sender, EventArgs e)
        {
            advancedSubscription();
        }

        protected void PremiumSubButton_Click(object sender, EventArgs e)
        {
            premiumSubscription();
        }

        public async void basicSubscription()
        {
            String status = "Active";
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

                // Do something with the field value

                // Generate random ID number
                Random random = new Random();
                int randomIDNumber = random.Next(100000, 999999);
                string subscriptionID = "SUB" + randomIDNumber.ToString();

                // Get current date time and the expected expiration date
                DateTime currentDate = DateTime.Now;
                DateTime expirationDate = currentDate.AddMonths(3);
                string startDate = currentDate.ToString("yyyy-MM-dd HH:mm:ss");
                string endDate = expirationDate.ToString("yyyy-MM-dd HH:mm:ss");


                // Create a new collection reference
                DocumentReference documentRef = db.Collection("AdminManageSubscription").Document(userEmail);

                // Set the data for the new document
                Dictionary<string, object> dataInsert = new Dictionary<string, object>
                {
                    {"subscriptionID", subscriptionID},
                    {"subscriptionType", BasicSubscriptionLabel.Text.ToString()},
                    {"price", BasicSubPriceLabel.Text.ToString()},
                    {"userEmail", userEmail},
                    {"userRole", userRole},
                    {"startDate", startDate},
                    {"endDate", endDate},
                    {"status", status}
                };

                // Set the data in the Firestore document
                await documentRef.SetAsync(dataInsert);
            }
            else
            {
                // Document does not exist
            }
        }

        public async void advancedSubscription()
        {
            String status = "Active";
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

                // Do something with the field value

                // Generate random ID number
                Random random = new Random();
                int randomIDNumber = random.Next(100000, 999999);
                string subscriptionID = "SUB" + randomIDNumber.ToString();

                // Get current date time and the expected expiration date
                DateTime currentDate = DateTime.Now;
                DateTime expirationDate = currentDate.AddMonths(3);
                string startDate = currentDate.ToString("yyyy-MM-dd HH:mm:ss");
                string endDate = expirationDate.ToString("yyyy-MM-dd HH:mm:ss");


                // Create a new collection reference
                DocumentReference documentRef = db.Collection("AdminManageSubscription").Document(userEmail);

                // Set the data for the new document
                Dictionary<string, object> dataInsert = new Dictionary<string, object>
                {
                    {"subscriptionID", subscriptionID},
                    {"subscriptionType", BasicSubscriptionLabel.Text.ToString()},
                    {"price", BasicSubPriceLabel.Text.ToString()},
                    {"userEmail", userEmail},
                    {"userRole", userRole},
                    {"startDate", startDate},
                    {"endDate", endDate},
                    {"status", status}
                };

                // Set the data in the Firestore document
                await documentRef.SetAsync(dataInsert);
            }
            else
            {
                // Document does not exist
            }
        }

        public async void premiumSubscription()
        {
            String status = "Active";
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

                // Do something with the field value

                // Generate random ID number
                Random random = new Random();
                int randomIDNumber = random.Next(100000, 999999);
                string subscriptionID = "SUB" + randomIDNumber.ToString();

                // Get current date time and the expected expiration date
                DateTime currentDate = DateTime.Now;
                DateTime expirationDate = currentDate.AddMonths(3);
                string startDate = currentDate.ToString("yyyy-MM-dd HH:mm:ss");
                string endDate = expirationDate.ToString("yyyy-MM-dd HH:mm:ss");


                // Create a new collection reference
                DocumentReference documentRef = db.Collection("AdminManageSubscription").Document(userEmail);

                // Set the data for the new document
                Dictionary<string, object> dataInsert = new Dictionary<string, object>
                {
                    {"subscriptionID", subscriptionID},
                    {"subscriptionType", BasicSubscriptionLabel.Text.ToString()},
                    {"price", BasicSubPriceLabel.Text.ToString()},
                    {"userEmail", userEmail},
                    {"userRole", userRole},
                    {"startDate", startDate},
                    {"endDate", endDate},
                    {"status", status}
                };

                // Set the data in the Firestore document
                await documentRef.SetAsync(dataInsert);
            }
            else
            {
                // Document does not exist
            }
        }
    }
}
