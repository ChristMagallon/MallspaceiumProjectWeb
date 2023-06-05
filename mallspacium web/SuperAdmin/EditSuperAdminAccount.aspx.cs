using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mallspacium_web.SuperAdmin
{
    public partial class EditSuperAdminAccount : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            if (!IsPostBack)
            {
                retrieveData();

                emailTextbox.Enabled = false;
                idTextbox.Enabled = false;
                usernameTextbox.Enabled = false;
                dateCreatedTextbox.Enabled = false;
            }
        }

        protected void updateButton_Click(object sender, EventArgs e)
        {
            getAdminPassword();
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            delete();
            /*deleteAccountActivity();*/
        }

        public async void retrieveData()
        {
            if (!IsPostBack)
            {
                // Retrieve the document ID from the query string
                string superAdminEmail = Request.QueryString["superAdminEmail"];

                // Use the document ID to retrieve the data from Firestore
                DocumentReference docRef = database.Collection("SuperAdminAccount").Document(superAdminEmail);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                // Check if the document exists
                if (snapshot.Exists)
                {
                    // Retrieve the data from the document
                    string username = snapshot.GetValue<string>("superAdminUsername");
                    string id = snapshot.GetValue<string>("superAdminId");
                    string email = snapshot.GetValue<string>("superAdminEmail");
                    string dateCreated = snapshot.GetValue<string>("superAdminDateCreated");

                    // Display the data
                    usernameTextbox.Text = username;
                    idTextbox.Text = id;
                    emailTextbox.Text = email;
                    dateCreatedTextbox.Text = dateCreated;
                }
            }
        }

        private async void getAdminPassword()
        {
            // Check if email address exists in Firestore
            string adminPassword = currentPasswordTextbox.Text;
            CollectionReference usersRef = database.Collection("SuperAdminAccount");
            QuerySnapshot usersQuery = await usersRef.WhereEqualTo("superAdminPassword", adminPassword).GetSnapshotAsync();
            if (usersQuery.Documents.Count == 0)
            {
                // current password not found
                errorCurrentPasswordLabel.Text = "It seems like the password you entered doesn't match our records.";
            }
            else
            {
                update();
                /*updateAccountActivity();*/
            }
        }

        public async void update()
        {
            DocumentReference docRef = database.Collection("SuperAdminAccount").Document(emailTextbox.Text);

            Dictionary<string, object> data = new Dictionary<string, object>
{
                {"superAdminUsername", usernameTextbox.Text},
                {"superAdminId", idTextbox.Text},
                {"superAdminEmail", emailTextbox.Text},
                {"superAdminDateCreated", dateCreatedTextbox.Text},
                {"superAdminPassword", newPasswordTextbox.Text},
                {"superAdminConfirmPassword", confirmNewPasswordTextbox.Text}
            };

            try
            {
                await docRef.SetAsync(data, SetOptions.MergeAll);

                string message = "Account Successfully Updated";
                string script = "alert('" + message + "')";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

                Response.Redirect("~/SuperAdmin/SuperAdminAccount.aspx", false);
            }
            catch (Exception)
            {
                string message = "Error Updating Account";
                string script = "alert('" + message + "')";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

                Response.Redirect("SuperAdminAccount.aspx", false);
            }
        }

        /*public async void updateAccountActivity()
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
                { "activity", (string)Application.Get("usernameget") + " updated the admin account " + emailTextbox.Text },
                { "userRole", "Admin"},
                { "email", emailTextbox.Text },
                { "date", date }
            };
            await userRef.SetAsync(data1);
        }*/


        public async void delete()
        {
            // Specify the collection and document to delete
            CollectionReference collection = database.Collection("SuperAdminAccount");
            DocumentReference document = collection.Document(emailTextbox.Text);

            // Delete the document
            await document.DeleteAsync();

            string message = "Account Successfully Deleted!";
            string script = "alert('" + message + "')";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

            Response.Redirect("SuperAdminAccount.aspx", false);
        }


        /*public async void deleteAccountActivity()
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
                { "activity", (string)Application.Get("usernameget") + " deleted the admin account " + emailTextbox.Text },
                { "userRole", "Admin"},
                { "email", emailTextbox.Text },
                { "date", date }
            };
            await userRef.SetAsync(data1);
        }*/
    }
}