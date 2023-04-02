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
            banUser("username");
        }

        protected void unbanButton_Click(object sender, EventArgs e)
        {
            unbanUser("username");
        }

        protected void sendButton_Click(object sender, EventArgs e)
        {
            sendWarningMessage();
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
            usernameLabel.Text = Request.QueryString["username"].ToString();
            accountTypeLabel.Text = Request.QueryString["accountType"].ToString();
            dateCreatedLabel.Text = Request.QueryString["dateCreated"].ToString();
            emailLabel.Text = Request.QueryString["email"].ToString();
            addressLabel.Text = Request.QueryString["address"].ToString();
            contactNumberLabel.Text = Request.QueryString["contactNumber"].ToString();  
        }
        

        // Ban a user
        public async void banUser(string username)
        {
            var bannedUsersCollection = database.Collection("AdminBannedUsers");
            var userDocRef = bannedUsersCollection.Document(usernameLabel.Text);

            // Create a new document for the banned user
            var bannedUserData = new Dictionary<string, object>
            {
                {"username", usernameLabel.Text},
                {"accountType", accountTypeLabel.Text },
                {"dateCreated",dateCreatedLabel.Text },
                {"email",emailLabel.Text },
                {"address",addressLabel.Text },
                {"contactNumber", contactNumberLabel.Text}
        };
            await userDocRef.SetAsync(bannedUserData);

            string message = "Successfully Banned User";
            string script = "alert('" + message + "')";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
        }


        // Unban a user
        protected async void unbanUser(string username)
        {
            
            var bannedUsersCollection = database.Collection("AdminBannedUsers");
            var userDocRef = bannedUsersCollection.Document(usernameLabel.Text);

            await userDocRef.DeleteAsync();

            string message = "Successfully Unbanned User";
            string script = "alert('" + message + "')";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
        }


        public async void sendWarningMessage()
        {
            //auto generated unique id
            Guid id = Guid.NewGuid();
            string uniqueId = id.ToString();

            //get the current UTC date and time
            DateTime now = DateTime.UtcNow;

            // Format the date and time as a string
            string dateString = now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

            DocumentReference userRef = database.Collection("Users").Document(emailLabel.Text).Collection("Notification").Document(uniqueId);
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                { "id", uniqueId },
                { "message", warningMessageTextbox.Text },
                { "date", dateString }
            };

            if (warningMessageValidator.IsValid)
            {
                await userRef.SetAsync(data1);

                // Display a message
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('Successfully Send Warning Message!');", true);

                // Redirect to another page after a delay
                string url = "ManageUserForm.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 500);", true);
            }
        }
    }
}