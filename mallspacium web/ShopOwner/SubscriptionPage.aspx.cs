using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Globalization;
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

            getCurrentSubDetails();
            getUserSubDetails();
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
                DateTime expirationDate = currentDate.AddMonths(1);
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
                    {"subscriptionType", BasicSubscriptionLabel.Text.ToString()},
                    {"price", BasicSubPriceLabel.Text.ToString()},
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
            }
            else
            {
                // Document does not exist
            }

            // Redirect to the same page to apply the changes
            Response.Redirect(Request.RawUrl, false);
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

                // Check if the document exists
                DocumentSnapshot documentSnapshot = await documentRef.GetSnapshotAsync();
                if (documentSnapshot.Exists)
                {
                    // Document exists, update the fields
                    Dictionary<string, object> dataUpdate = new Dictionary<string, object>
                {
                    {"subscriptionType", AdvancedSubscriptionLabel.Text.ToString()},
                    {"price", AdvancedSubPriceLabel.Text.ToString()},
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
                        {"subscriptionType", AdvancedSubscriptionLabel.Text.ToString()},
                        {"price", AdvancedSubPriceLabel.Text.ToString()},
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
            // Redirect to the same page to apply the changes
            Response.Redirect(Request.RawUrl, false);
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
                DateTime expirationDate = currentDate.AddMonths(5);
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
                        {"subscriptionType", PremiumSubscriptionLabel.Text.ToString()},
                        {"price", PremiumSubPriceLabel.Text.ToString()},
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
                        {"subscriptionType", PremiumSubscriptionLabel.Text.ToString()},
                        {"price",PremiumSubPriceLabel.Text.ToString()},
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
            // Redirect to the same page to apply the changes
            Response.Redirect(Request.RawUrl, false);
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
                CurrentSubscriptionLabel.Text = subscriptionType.ToString();

                if (subscriptionType == BasicSubscriptionLabel.Text)
                {
                    BasicSubButton.Enabled = false;
                }
                else if (subscriptionType == AdvancedSubscriptionLabel.Text)
                {
                    AdvanceSubButton.Enabled = false;
                }
                else if (subscriptionType == PremiumSubscriptionLabel.Text)
                {
                    PremiumSubButton.Enabled = false;
                }
                else
                {
                    BasicSubButton.Enabled = true;
                    AdvanceSubButton.Enabled = true;
                    PremiumSubButton.Enabled = true;
                }
            }
        }

        // Check the user subscription status
        public async void getUserSubDetails()
        {
            DateTime currentDate = DateTime.Now;
            bool isSubscriptionExpired = true;

            // Query the Firestore collection for a user with a specific email address
            CollectionReference subscriptionRef = db.Collection("AdminManageSubscription");
            DocumentReference docRef = subscriptionRef.Document((string)Application.Get("usernameget"));

            // Retrieve the document data asynchronously
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            // Check if the document exists
            if (snapshot.Exists)
            {
                // Get the data as a Dictionary
                Dictionary<string, object> data = snapshot.ToDictionary();

                DateTime startDate;
                DateTime endDate;

                if (DateTime.TryParseExact(data["startDate"].ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate) &&
                    DateTime.TryParseExact(data["endDate"].ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
                {
                    if (currentDate >= startDate && currentDate <= endDate)
                    {
                        // User subscription is still active
                        isSubscriptionExpired = false;
                    }
                }
            }

            if (isSubscriptionExpired)
            {
                revertSubscription();
            }
        }

        // Revert the subscription back to free
        public async void revertSubscription()
        {
            string subscriptionType = "Free";
            string subscriptionPrice = "0.00";
            string status = "Expired";
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
        }

        protected void ViewSubscriptionButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ShopOwner/MyCurrentSubscriptionPage.aspx", false);
        }
    }
}
