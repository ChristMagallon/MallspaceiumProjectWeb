using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

            showData();
        }

        public void showData()
        {

            usernameLabel.Text = Request.QueryString["username"].ToString();
            idLabel.Text = Request.QueryString["id"].ToString();
            accountTypeLabel.Text = Request.QueryString["accountType"].ToString();
            dateCreatedLabel.Text = Request.QueryString["dateCreated"].ToString();
            emailLabel.Text = Request.QueryString["email"].ToString();
            addressLabel.Text = Request.QueryString["address"].ToString();
            contactNumberLabel.Text = Request.QueryString["contactNumber"].ToString();
            
        }

        protected void banButton_Click(object sender, EventArgs e)
        {
            banUser("username");
        }

        protected void unbanButton_Click(object sender, EventArgs e)
        {
            unbanUser("username");
        }

        // Ban a user
        public async void banUser(string username)
        {
            var bannedUsersCollection = database.Collection("AdminBannedUsers");
            var userDocRef = bannedUsersCollection.Document(username);

            // Create a new document for the banned user
            var bannedUserData = new Dictionary<string, object>
            {
                {"username", username}
            // add any other relevant information about the banned user
            };
            await userDocRef.SetAsync(bannedUserData);
        }

        // Unban a user
        protected async void unbanUser(string username)
        {
            var bannedUsersCollection = database.Collection("AdminBannedUsers");
            var userDocRef = bannedUsersCollection.Document(username);

            await userDocRef.DeleteAsync();
        }
    }
}