using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.form
{
    public partial class LoginPage : System.Web.UI.Page
    {
        FirestoreDb db;

        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("mallspaceium");
        }   

        protected void SignupButton_Click(object sender, EventArgs e)
        {
            getUserStatus();
        }

        public async void getLoginDetails()
        {
            // Query the Firestore collection for a user with a specific email address
            CollectionReference usersRef = db.Collection("Users");
            Query query = usersRef.WhereEqualTo("email", EmailTextBox.Text).WhereEqualTo("password", PasswordTextBox.Text);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // Iterate over the results to find the user
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                if (document.Exists)
                {
                    // Do something with the user document
                    Application.Set("usernameget",EmailTextBox.Text);
                    Response.Redirect("~/ShopOwner/PopularShopsPage.aspx", false);
                }
                else
                {
                    // Do something with the user document
                    Response.Write("<script>alert('No user records!');</script>");
                }
            }
        }
        public async void getUserStatus()
        {
            Boolean choice = false;
            // Query the Firestore collection for a user with a specific email address
            CollectionReference usersRef = db.Collection("AdminBannedUsers");
            Query query = usersRef.WhereEqualTo("email", EmailTextBox.Text);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // Iterate over the results to find the user
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                if (document.Exists)
                {
                    // Do something with the user document
                    Response.Write("<script>alert('Your account is banned! Please contact administrator');</script>");
                    choice = true;
                }
            }
            if (choice == false)
            {
                getLoginDetails();
            }
        }
    }
}