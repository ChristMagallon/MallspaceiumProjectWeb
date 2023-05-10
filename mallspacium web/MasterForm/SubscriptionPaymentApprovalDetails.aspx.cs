using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.MasterForm
{
    public partial class SubscriptionPaymentApprovalDetails : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            if (!IsPostBack)
            {
                if(Request.QueryString["userEmail"] != null)
                {
                    string email = Request.QueryString["userEmail"];
                    getSubscriptionPaymentDetails(email);
                }           
            }
        }

        public async void getSubscriptionPaymentDetails(String email)
        {
            // Use the shop name to retrieve the data from Firestore
            Query query = database.Collection("SubscriptionPaymentApproval").WhereEqualTo("userEmail", email);
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
                string subID = subDoc.GetValue<string>("subscriptionID");
                string subType = subDoc.GetValue<string>("subscriptionType");
                string price = subDoc.GetValue<string>("price");
                string payment = subDoc.GetValue<string>("paymentFile");

                // Display the data
                transactionIdLabel.Text = transID;
                emailLabel.Text = uemail;
                firstNameLabel.Text = fname;
                lastNameLabel.Text = lname;
                userRoleLabel.Text = role;
                subscriptionIdLabel.Text = subID;
                subscriptionTypeLabel.Text = subType;
                priceLabel.Text = price;
                imageHiddenField.Value = payment;
            }
            else
            {
                Response.Write("<script>alert('Error: Subscription Not Found.');</script>");
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
            string url = "PopUpSubscriptionPaymentImage.aspx?userEmail=" + userEmail;

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
            approvedActivity();
        }

        // Approved Payment Methodd
        private async void approvedPayment()
        {
            String userEmail = Request.QueryString["userEmail"]?.ToString() ?? "";

            string status = "Active";
            string basicSubscription = "Basic Subscription";
            string advancedSubscription = "Advanced Subscription";
            string premiumSubscription = "Premium Subscription";

            if (subscriptionTypeLabel.Text.Equals(basicSubscription))
            {
                // Get current date time and the expected expiration date
                DateTime currentDate = DateTime.UtcNow;
                DateTime expirationDate = currentDate.AddMonths(1);
                string startDate = currentDate.ToString("yyyy-MM-dd HH:mm:ss");
                string endDate = expirationDate.ToString("yyyy-MM-dd HH:mm:ss");

                // Create a new collection reference
                DocumentReference documentRef = database.Collection("AdminManageSubscription").Document(userEmail);

                // Set the data for the new document
                Dictionary<string, object> dataInsert = new Dictionary<string, object>
                {
                    {"subscriptionID", subscriptionIdLabel.Text},
                    {"subscriptionType", subscriptionTypeLabel.Text},
                    {"price", priceLabel.Text},
                    {"userEmail", emailLabel.Text},
                    {"userRole", userRoleLabel.Text},
                    {"firstName", firstNameLabel.Text},
                    {"lastName", lastNameLabel.Text},
                    {"startDate", startDate},
                    {"endDate", endDate},
                    {"status", status}
                };

                // Set the data in the Firestore document
                await documentRef.SetAsync(dataInsert);
                removeDocumentID(userEmail);
                sendApprovedNotif();

                // Display a message
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('A subscription payment has been approved.');", true);
                // Redirect to another page after a delay
                string url = "SubscriptionPaymentApproval.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 500);", true);
            }
            else if (subscriptionTypeLabel.Text.Equals(advancedSubscription))
            {
                // Get current date time and the expected expiration date
                DateTime currentDate = DateTime.UtcNow;
                DateTime expirationDate = currentDate.AddMonths(3);
                string startDate = currentDate.ToString("yyyy-MM-dd HH:mm:ss");
                string endDate = expirationDate.ToString("yyyy-MM-dd HH:mm:ss");

                // Create a new collection reference
                DocumentReference documentRef = database.Collection("AdminManageSubscription").Document(userEmail);

                // Set the data for the new document
                Dictionary<string, object> dataInsert = new Dictionary<string, object>
                {
                    {"subscriptionID", subscriptionIdLabel.Text},
                    {"subscriptionType", subscriptionTypeLabel.Text},
                    {"price", priceLabel.Text},
                    {"userEmail", emailLabel.Text},
                    {"userRole", userRoleLabel.Text},
                    {"firstName", firstNameLabel.Text},
                    {"lastName", lastNameLabel.Text},
                    {"startDate", startDate},
                    {"endDate", endDate},
                    {"status", status}
                };

                // Set the data in the Firestore document
                await documentRef.SetAsync(dataInsert);
                removeDocumentID(userEmail);
                sendApprovedNotif();

                // Display a message
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('A subscription payment has been approved.');", true);
                // Redirect to another page after a delay
                string url = "SubscriptionPaymentApproval.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 500);", true);
            }
            else if (subscriptionTypeLabel.Text.Equals(premiumSubscription))
            {
                // Get current date time and the expected expiration date
                DateTime currentDate = DateTime.UtcNow;
                DateTime expirationDate = currentDate.AddMonths(5);
                string startDate = currentDate.ToString("yyyy-MM-dd HH:mm:ss");
                string endDate = expirationDate.ToString("yyyy-MM-dd HH:mm:ss");

                // Create a new collection reference
                DocumentReference documentRef = database.Collection("AdminManageSubscription").Document(userEmail);

                // Set the data for the new document
                Dictionary<string, object> dataInsert = new Dictionary<string, object>
                {
                    {"subscriptionID", subscriptionIdLabel.Text},
                    {"subscriptionType", subscriptionTypeLabel.Text},
                    {"price", priceLabel.Text},
                    {"userEmail", emailLabel.Text},
                    {"userRole", userRoleLabel.Text},
                    {"firstName", firstNameLabel.Text},
                    {"lastName", lastNameLabel.Text},
                    {"startDate", startDate},
                    {"endDate", endDate},
                    {"status", status}
                };

                // Set the data in the Firestore document
                await documentRef.SetAsync(dataInsert);
                removeDocumentID(userEmail);
                sendApprovedNotif();

                // Display a message
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('A subscription payment has been approved.');", true);
                // Redirect to another page after a delay
                string url = "SubscriptionPaymentApproval.aspx";
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
                string favID = "SUBPAY" + randomIDNumber.ToString();

                // Get current date time 
                DateTime currentDate = DateTime.UtcNow;
                string date = currentDate.ToString("yyyy-MM-dd HH:mm:ss");

                // Specify the name of the document using a variable or a string literal
                string documentName = "Payment successfully approved! " + favID;
                DocumentReference notifRef = database.Collection("Users").Document(userEmail).Collection("Notification").Document(documentName);
                Dictionary<string, object> data = new Dictionary<string, object>
                {
                    {"notifDetail", "You are now finally subscribed to " + subscriptionTypeLabel.Text + " . Please enjoy what the subscription has to offer, and happy browsing." },
                    {"notifImage", "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/br+S/7c/23////bAEMAAgEBAgEBAgICAgICAgIDBQMDAwMDBgQEAwUHBgcHBwYHBwgJCwkICAoIBwcKDQoKCwwMDAwHCQ4PDQwOCwwMDP/bAEMBAgICAwMDBgMDBgwIBwgMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDP/AABEIAk8CWQMBIgACEQEDEQH/xAAfAAABBQEBAQEBAQAAAAAAAAAAAQIDBAUGBwgJCgv/xAC1EAACAQMDAgQDBQUEBAAAAX0BAgMABBEFEiExQQYTUWEHInEUMoGRoQgjQrHBFVLR8CQzYnKCCQoWFxgZGiUmJygpKjQ1Njc4OTpDREVGR0hJSlNUVVZXWFlaY2RlZmdoaWpzdHV2d3h5eoOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4eLj5OXm5+jp6vHy8/T19vf4+fr/xAAfAQADAQEBAQEBAQEBAAAAAAAAAQIDBAUGBwgJCgv/xAC1EQACAQIEBAMEBwUEBAABAncAAQIDEQQFITEGEkFRB2FxEyIygQgUQpGhscEJIzNS8BVictEKFiQ04SXxFxgZGiYnKCkqNTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqCg4SFhoeIiYqSk5SVlpeYmZqio6Slpqeoqaqys7S1tre4ubrCw8TFxsfIycrS09TV1tfY2dri4+Tl5ufo6ery8/T19vf4+fr/2gAMAwEAAhEDEQA/AP38ooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACg8iiigBpbBoz/AL3618pf8Fv/ABxrXw2/4Ja/FrXPDusapoGtafaWTWuoaddSWt1bFtQtUYpJGQykqzKcEZDEdDX86Dft2/HDP/JZPit/4Vt/7f8ATWuqhhXUXMmcmIxSpS5Wj+tzf/nFG/8Aziv5I/8Ahu/44f8ARZfit/4Vt/8A/HaP+G7/AI4f9Fl+K3/hW3//AMdrb+z5dzD+0o/yn9bm/wDzijf/AJxX8kf/AA3f8cP+iy/Fb/wrb/8A+O0f8N3/ABw/6LL8Vv8Awrb/AP8AjtH9ny7h/aUf5T+tzf8A5xRv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Ov75XH1HFfm3+0h/wal+KNFmnvPhN8StL1q15aPTPFFs1ncoOoQXMIZHPbLRR1+4W0elGOK2p4ipDZnPUwtOe6P5Y/jN/wAEcv2mvgPJN/bPwh8UX9rbgs13oKprVvtH8WbZncD/AH1BHpXzr4o8K6p4Hv3t9c0rVdFuYjteHUbOS0kU/SQIa/sk8pc/pVHXfC2m+J7M2+pWNlqFu3WO6gWZD+DAiuqOYP7SOWWWx+yz+NdLuGUfJLDJ7rIG/lmneap/5aL+fT9K/rj8QfsP/BjxWzNqnwl+GeoSOcs9x4YspGb8TFmsaP8A4Jv/ALPsUm5fgj8Jg3/YqWP/AMbrT+0I9jP+zZdz+TJr6FDhprdfbzVB/I/4V0/gL4PeL/itera+FvCfijxNcSEBU0rSZ7wknoP3aEfniv6zfDX7Ivwp8Fyq+jfDP4e6Sy9Gs/DlnCw/FYxXfWWnQ6dbJDbwxwwxjCpGoRVHsBxUvMOyKjlveR/Mr8D/APggx+1F8cHikX4byeD7CQ5a88UX0emiIe8OZJz/AN+hX3T+zR/waj6NpbW998X/AIkX2tODmTSPC1v9it2x2a6mDSMOxKJGSOlfsOEA7DnrxS44rnqYypLbQ6KeBpR1ep4/+y5+wn8Jf2NNGax+G/gXQfDTOAJ72KHzb+7GMfvbmQtNJ9GbHoBXryxhR0H5U4KAOlFc0pN6s6oxUVZBjmiiikUFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQB//2Q==" },
                    {"notifDate", date }
                };
                await notifRef.SetAsync(data);
            }
        }

        // Delete the document from the collection
        private async Task removeDocumentID(string userEmail)
        {
            // Get reference to the user's document
            DocumentReference userRef = database.Collection("SubscriptionPaymentApproval").Document(userEmail);

            // Check if the document exists
            DocumentSnapshot userSnapshot = await userRef.GetSnapshotAsync();
            if (!userSnapshot.Exists)
            {
                return;
            }

            // Delete the email document
            await userRef.DeleteAsync();
        }

        public async void approvedActivity()
        {
            //auto generated unique id
            Random random = new Random();
            int randomIDNumber = random.Next(100000, 999999);
            string activityID = "ACT" + randomIDNumber.ToString();

            //Get current date time and the expected expiration date
            DateTime currentDate = DateTime.Now;
            string date = currentDate.ToString("yyyy-MM-dd HH:mm:ss");

            DocumentReference userRef = database.Collection("AdminActivity").Document(activityID);
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                { "id", activityID },
                { "activity", (string)Application.Get("usernameget") + " approved shop owner " + emailLabel.Text + " subscription payment."},
                { "email", emailLabel.Text },
                { "userRole", userRoleLabel.Text },
                { "date", date }
            };
            await userRef.SetAsync(data1);
        }

        protected void disapproveButton_Click(object sender, EventArgs e)
        {
            sendDisapprovedNotif();
            disapprovedActivity();
        }

        // Send notification to the user if disapproved
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
                string favID = "NOTIF" + randomIDNumber.ToString();

                // Specify the name of the document using a variable or a string literal
                string documentName = "Payment is disapproved! " + favID;
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
                string url = "SubscriptionPaymentApproval.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 500);", true);
            }
        }

        public async void disapprovedActivity()
        {
            //auto generated unique id
            Random random = new Random();
            int randomIDNumber = random.Next(100000, 999999);
            string activityID = "ACT" + randomIDNumber.ToString();

            //Get current date time and the expected expiration date
            DateTime currentDate = DateTime.Now;
            string date = currentDate.ToString("yyyy-MM-dd HH:mm:ss");

            DocumentReference userRef = database.Collection("AdminActivity").Document(activityID);
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                { "id", activityID },
                { "activity", (string)Application.Get("usernameget") + " disapproved shop owner " + emailLabel.Text + " subscription payment."},
                { "email", emailLabel.Text },
                { "userRole", userRoleLabel.Text },
                { "date", date }
            };
            await userRef.SetAsync(data1);
        }
    }
}