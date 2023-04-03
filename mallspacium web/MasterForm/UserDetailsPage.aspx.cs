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
                { "date", date }
            };
            await userRef.SetAsync(data1);
        }


        public async void sendWarningMessage()
        {
            //auto generated unique id
            Random random = new Random();
            int randomIDNumber = random.Next(100000, 999999);
            string notofID = "NOTIF" + randomIDNumber.ToString();

            //Get current date time and the expected expiration date
            DateTime currentDate = DateTime.Now;
            string date = currentDate.ToString("yyyy-MM-dd HH:mm:ss");

            DocumentReference userRef = database.Collection("Users").Document(emailLabel.Text).Collection("Notification").Document(notofID);
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                { "id", notofID },
                { "message", warningMessageTextbox.Text },
                { "date", date }
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
                { "date", date }
            };
            await userRef.SetAsync(data1);
        }
    }   
}
