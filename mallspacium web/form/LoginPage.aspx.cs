using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
            getServerStatus();
        }

        public async void getLoginDetails()
        {
            try
            {
                // Query the Firestore collection for a user with a specific email address and password
                CollectionReference usersRef = db.Collection("Users");
                Query query = usersRef.WhereEqualTo("email", EmailTextBox.Text).WhereEqualTo("password", PasswordTextBox.Text);
                QuerySnapshot snapshot = await query.GetSnapshotAsync();

                // Check if the snapshot is empty
                if (snapshot.Count == 0)
                {
                    ErrorEmailAddressLabel.Text = "It seems like the password you entered is incorrect or email you entered doesn't match our records.";
                }

                // Iterate over the results to find the user
                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        // Define the document reference and field name
                        DocumentReference docRef = db.Collection("Users").Document(EmailTextBox.Text);
                        string userRole = "userRole";
                        // Get the field value from Firestore
                        DocumentSnapshot docSnapshot = await docRef.GetSnapshotAsync();
                        string userFieldValue = docSnapshot.GetValue<string>(userRole);
                        bool verifiedFieldValue = docSnapshot.GetValue<bool>("verified");                     

                        // Check if the user has been verified
                        if (!verifiedFieldValue)
                        {
                            Response.Write("<script>alert('We noticed your account has not been verified! Please verify your account to be able to login.');</script>");
                        }

                        // Identify the user role and redirect to their respective page
                        if (userFieldValue == "Shopper")
                        {
                            Application.Set("usernameget", EmailTextBox.Text);
                            Response.Redirect("~/Shopper/PopularShopsPage.aspx", false);
                        }
                        else if (userFieldValue == "ShopOwner")
                        {
                            bool certifiedShopOwnerFieldValue = docSnapshot.GetValue<bool>("certifiedShopOwner");

                            // Check if the user has been certified as a shop owner
                            if (!certifiedShopOwnerFieldValue)
                            {
                                Response.Write("<script>alert('We noticed your account has not been fully verified! Please wait while the admin review your account.');</script>");
                            }
                            else
                            {
                                Application.Set("usernameget", EmailTextBox.Text);
                                Response.Redirect("~/ShopOwner/PopularShopsPage.aspx", false);
                            }                      
                        }
                        else
                        {
                            throw new Exception("Invalid user role.");
                        }
                    }
                    else
                    {
                        throw new Exception("Invalid email or password.");
                    }
                }
            }
            catch (Exception ex)
            {
                string message = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('{message}');", true);
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
                ErrorEmailAddressLabel.Text = "It seems like the password you entered is incorrect or email you entered doesn't match our records.";
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

        // Check the server status incase of maintenance
        public async void getServerStatus()
        {
            DateTime currentDate = DateTime.Now;
            bool isMaintenanceInProgress = false;
            DateTime latestEndTime = DateTime.MinValue;
            CollectionReference downtimeRef = db.Collection("AdminSystemDowntime");

            QuerySnapshot downtimeSnapshot = await downtimeRef.GetSnapshotAsync();

            foreach (DocumentSnapshot documentSnapshot in downtimeSnapshot.Documents)
            {
                DateTime startTime;
                DateTime endTime;

                if (DateTime.TryParseExact(documentSnapshot.GetValue<string>("startTime"), "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out startTime) &&
                    DateTime.TryParseExact(documentSnapshot.GetValue<string>("endTime"), "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out endTime))
                {
                    if (currentDate >= startTime && currentDate <= endTime.AddSeconds(30))
                    {
                        // Display the maintenance message to the user
                        Response.Write("<script>alert('Server is under maintenance. Try logging back again later.');</script>");
                        isMaintenanceInProgress = true;
                        break;
                    }

                    if (endTime > latestEndTime)
                    {
                        latestEndTime = endTime;
                    }
                }
            }

            if (!isMaintenanceInProgress && currentDate > latestEndTime.AddSeconds(30))
            {
                // If there is no maintenance in progress and the current time is after the latest end time plus 30 seconds, proceed with login
                getAdmin();
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

        protected void VerifyAccountLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/form/ConfirmationEmailPage.aspx");
        }
    }
}