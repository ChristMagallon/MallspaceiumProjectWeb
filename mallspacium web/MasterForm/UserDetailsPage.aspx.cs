 using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.AdditionalForm
{
    public partial class UserDetailsPage : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
           string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            checkbannedaccount();
        }

        protected void banButton_Click(object sender, EventArgs e)
        {
            banUser();
            banActivity();
        }

        protected void unbanButton_Click(object sender, EventArgs e)
        {
            unbanUser();
            unbanActivity();
        }

        protected void sendButton_Click(object sender, EventArgs e)
        {
            sendWarningMessage();
            sendWarningMessageActivity();
        }

        public async void checkbannedaccount()
        {
            Boolean choice = false;
            // Query the Firestore collection for a user with a specific email address
            CollectionReference usersRef = database.Collection("AdminBannedUsers");
            Query query = usersRef.WhereEqualTo("email", Request.QueryString["email"].ToString());

            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // Iterate over the results to find the user
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                if (document.Exists)
                {
                    // Do something with the user document
                    showData();
                    banButton.Enabled = false;
                    unbanButton.Enabled = true;
                    choice = true;
                }
            }
            if (choice == false)
            {
                showData();
                banButton.Enabled = true;
                unbanButton.Enabled = false;
            }
        }


        public void showData()
        {
            idLabel.Text = Request.QueryString["userID"].ToString();
            emailLabel.Text = Request.QueryString["email"].ToString();
            userRoleLabel.Text = Request.QueryString["userRole"].ToString();
            addressLabel.Text = Request.QueryString["address"].ToString();
            contactNumberLabel.Text = Request.QueryString["contactNumber"].ToString();
            dateCreatedLabel.Text = Request.QueryString["dateCreated"].ToString();
        }
        

        // Ban a user
        public async void banUser()
        {
            var bannedUsersCollection = database.Collection("AdminBannedUsers");
            var userDocRef = bannedUsersCollection.Document(emailLabel.Text);

            // Create a new document for the banned user
            var bannedUserData = new Dictionary<string, object>
            {
                {"userID", idLabel.Text},
                {"email",emailLabel.Text },
                {"accountType", userRoleLabel.Text },
                {"address",addressLabel.Text },
                {"contactNumber", contactNumberLabel.Text},
                {"dateCreated", dateCreatedLabel.Text }
            };
            await userDocRef.SetAsync(bannedUserData);

            // Display a message
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('Successfully Banned User!');", true);

            // Redirect to another page after a delay
            string url = "ManageUserForm.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 500);", true);
        }

        public async void banActivity()
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
                { "activity", (string)Application.Get("usernameget") + " banned user " + emailLabel.Text },
                { "email", emailLabel.Text },
                { "userRole", userRoleLabel.Text },
                { "date", date }
            };
            await userRef.SetAsync(data1);
        }


        // Unban a user
        protected async void unbanUser()
        {
            var bannedUsersCollection = database.Collection("AdminBannedUsers");
            var userDocRef = bannedUsersCollection.Document(emailLabel.Text);

            await userDocRef.DeleteAsync();

            // Display a message
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('Successfully Unbanned User!');", true);

            // Redirect to another page after a delay
            string url = "ManageUserForm.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 500);", true);
        }


        public async void unbanActivity()
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
                { "activity", (string)Application.Get("usernameget") + " unbanned user " + emailLabel.Text },
                { "email", emailLabel.Text },
                { "userRole", userRoleLabel.Text },
                { "date", date }
            };
            await userRef.SetAsync(data1);
        }


        public async void sendWarningMessage()
        {
            //auto generated unique id
            Random random = new Random();
            int randomIDNumber = random.Next(100000, 999999);
            string notifID = "NOTIF" + randomIDNumber.ToString();

            //Get current date time and the expected expiration date
            DateTime currentDate = DateTime.Now;
            string date = currentDate.ToString("yyyy-MM-dd HH:mm:ss");

            string documentName = "Warning Message! " + notifID;
            DocumentReference userRef = database.Collection("Users").Document(emailLabel.Text).Collection("Notification").Document(documentName);
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                { "notifDetail", warningMessageTextbox.Text },
                { "notifImage", "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" }
            };

            await userRef.SetAsync(data1);

            // Display a message
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('Successfully Send Warning Message!');", true);

            // Redirect to another page after a delay
            string url = "ManageUserForm.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 500);", true);
        }

        public async void sendWarningMessageActivity()
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
                { "activity", (string)Application.Get("usernameget") + " send warning message to user " + emailLabel.Text },
                { "email", emailLabel.Text },
                { "userRole", userRoleLabel.Text },
                { "date", date }
            };
            await userRef.SetAsync(data1);
        }
    }   
}
