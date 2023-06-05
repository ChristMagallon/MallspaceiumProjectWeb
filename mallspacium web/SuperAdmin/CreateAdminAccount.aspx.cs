using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mallspacium_web.SuperAdmin
{
    public partial class CreateAdminAccount : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            validateInput();
        }


        public async void validateInput()
        {
            Boolean checker = true;

            // Query the Firestore collection for a specific username
            CollectionReference usersRef2 = database.Collection("AdminAccount");
            Query query2 = usersRef2.WhereEqualTo("adminUsername", usernameTextbox.Text);
            QuerySnapshot snapshot2 = await query2.GetSnapshotAsync();

            // Iterate over the results to find the if the username is already taken
            foreach (DocumentSnapshot document2 in snapshot2.Documents)
            {
                if (document2.Exists)
                {
                    // Do something with the registered document
                    errorUsernameLabel.Text = "Username is already taken!";
                    checker = false;
                }
                else
                {
                    errorUsernameLabel.Text = "";
                }
            }

            // Query the Firestore collection for a admin account with a specific email address
            CollectionReference usersRef = database.Collection("AdminAccount");
            Query query = usersRef.WhereEqualTo("adminEmail", emailTextbox.Text);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // Iterate over the results to find the admin account
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                if (document.Exists)
                {
                    // Do something with the user document
                    errorEmailLabel.Text = "Email is already registered!";
                    checker = false;
                }
                else
                {
                    errorUsernameLabel.Text = "";
                }
            }

            // If the input values are valid proceed to complete registration
            if (checker == true)
            {
                addAccount();
                /*addAccountActivity();*/
            }
        }

        public async void addAccount()
        {
            //auto generated unique id
            Random random = new Random();
            int randomIDNumber = random.Next(100000, 999999);
            string adminID = "ADM" + randomIDNumber.ToString();

            //Get current date time and the expected expiration date
            DateTime currentDate = DateTime.Now;
            string date = currentDate.ToString("yyyy-MM-dd HH:mm:ss");

            DocumentReference doc = database.Collection("AdminAccount").Document(emailTextbox.Text);

            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
              { "adminUsername", usernameTextbox.Text},
              { "adminId", adminID},
              { "adminEmail", emailTextbox.Text},
              { "adminDateCreated", date},
              { "adminPassword", passwordTextbox.Text},
              { "adminConfirmPassword", confirmPasswordTextbox.Text}
            };
            await doc.SetAsync(data1);

            // Display a message
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('Successfully added a new admin account!');", true);

            // Redirect to another page after a delay
            string url = "ManageAdmin.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 500);", true);
        }


        /*public async void addAccountActivity()
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
                { "activity", (string)Application.Get("usernameget") + " created a new admin account " + emailTextbox.Text },
                { "userRole", "Admin"},
                { "email", emailTextbox.Text },
                { "date", date }
            };
            await userRef.SetAsync(data1);
        }*/
    }
}