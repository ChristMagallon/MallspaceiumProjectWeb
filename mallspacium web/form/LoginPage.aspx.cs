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
            getAdmin();
        }

        public async void getLoginDetails()
        {
            // Query the Firestore collection for a user with a specific email address
            CollectionReference usersRef = db.Collection("Users");
            Query query = usersRef.WhereEqualTo("email", EmailTextBox.Text).WhereEqualTo("password", PasswordTextBox.Text);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // Check if the snapshot is empty
            if (snapshot.Count == 0)
            {
                // Handle the case where the snapshot is empty
                Response.Write("<script>alert('It seems like the email you entered doesn't match our records.');</script>");
            }
            else
            {
                // Iterate over the results to find the user
                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        // Handle each document in the snapshot

                        // Define the document reference and field name
                        DocumentReference docRef = db.Collection("Users").Document(EmailTextBox.Text);
                        string userRole = "userRole";
                        // Get the field value from Firestore
                        DocumentSnapshot docSnapshot = await docRef.GetSnapshotAsync();
                        string fieldValue = docSnapshot.GetValue<string>(userRole);
                        // Store the field value in a local variable
                        string localUserRole = fieldValue;

                        // Identify the user role and from the user and redirect it to their respective page
                        if (localUserRole == "Shopper")
                        {
                            Application.Set("usernameget", EmailTextBox.Text);
                            Response.Redirect("~/Shopper/PopularShopsPage.aspx", false);
                        }
                        else if (localUserRole == "ShopOwner")
                        {
                            Application.Set("usernameget", EmailTextBox.Text);
                            Response.Redirect("~/ShopOwner/PopularShopsPage.aspx", false);
                        }                
                    }
                }
            }
        }

        public async void getAdmin()
        {
            Boolean choice = false;
            bool userExists = false;

            // Query the Firestore collection for a user with a specific email address
            CollectionReference usersRef = db.Collection("AdminAccount");
            Query query = usersRef.WhereEqualTo("adminEmail", EmailTextBox.Text).WhereEqualTo("adminPassword", PasswordTextBox.Text);

            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // Iterate over the results to find the user
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                if (document.Exists)
                {
                    // Do something with the user document
                    Application.Set("usernameget", EmailTextBox.Text);
                    Response.Redirect("~/MasterForm/ManageUserForm.aspx", false);
                    choice = true;
                    userExists = true;
                }
            }

            if (!userExists)
            {
                ErrorEmailAddressLabel.Text = "It seems like the email you entered doesn't match our records.";
            }

            if (choice == false)
            {
                getUserStatus();
            }
        }

        // Get the user status wether the account is banned or not
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

        protected void ShopperRegisterLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/form/ShopperRegisterPage.aspx");
        }

        protected void ShopOwnerRegisterLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/form/ShopOwnerRegisterPage.aspx");
        }

        protected void ForgotPasswordLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/form/ForgotPasswordPage.aspx");
        }
    }
}