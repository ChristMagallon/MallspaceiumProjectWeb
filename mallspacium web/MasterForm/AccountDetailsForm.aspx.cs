using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.MasterForm
{
    public partial class AdminAccountDetailsForm : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            retrieveData();

            usernameTextbox.Enabled = false;
            idTextbox.Enabled = false;
            dateCreatedTextbox.Enabled = false;
        }

        protected void updateButton_Click(object sender, EventArgs e)
        {
            update();
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            delete();
        }

        public async void retrieveData()
        {
            if (!IsPostBack)
            {
                // Retrieve the document ID from the query string
                string adminUsername = Request.QueryString["adminUsername"];

                // Use the document ID to retrieve the data from Firestore
                DocumentReference docRef = database.Collection("AdminAccount").Document(adminUsername);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                // Check if the document exists
                if (snapshot.Exists)
                {
                    // Retrieve the data from the document
                    string username1 = snapshot.GetValue<string>("adminUsername");
                    string id = snapshot.GetValue<string>("adminId");
                    string email = snapshot.GetValue<string>("adminEmail");
                    string phoneNumber = snapshot.GetValue<string>("adminPhoneNumber");
                    string dateCreated = snapshot.GetValue<string>("adminDateCreated");
                    string password = snapshot.GetValue<string>("adminPassword");
                    string confrimPassword = snapshot.GetValue<string>("adminConfirmPassword");

                    // Display the data
                    usernameTextbox.Text = username1;
                    idTextbox.Text = id;
                    emailTextbox.Text = email;
                    phoneNumberTextbox.Text = phoneNumber;
                    dateCreatedTextbox.Text = dateCreated;
                    passwordTextbox.Text = password;
                    confirmPasswordTextbox.Text = confrimPassword;
                }
            }
        }

        public async void update()
        {
            DocumentReference docRef = database.Collection("AdminAccount").Document(usernameTextbox.Text);

            Dictionary<string, object> data = new Dictionary<string, object>
{
                {"adminUsername", usernameTextbox.Text},
                {"adminId", idTextbox.Text},
                {"adminEmail", emailTextbox.Text},
                {"adminPhoneNumber", phoneNumberTextbox.Text},
                {"adminDateCreated", dateCreatedTextbox.Text},
                {"adminPassword", passwordTextbox.Text},
                {"adminConfirmPassword", confirmPasswordTextbox.Text}
            };

            try
            {
                await docRef.SetAsync(data, SetOptions.MergeAll);

                string message = "Account Successfully Updated";
                string script = "alert('" + message + "')";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

                Response.Redirect("~/MasterForm/AdminAccountForm.aspx", false);
            }
            catch (Exception ex)
            {
                string message = "Error Updating Account";
                string script = "alert('" + message + "')";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

                Response.Redirect("~/MasterForm/AdminAccountForm.aspx", false);
            }
        }

        public async void delete()
        {
            // Specify the collection and document to delete
            CollectionReference collection = database.Collection("AdminAccount");
            DocumentReference document = collection.Document(usernameTextbox.Text);

            // Delete the document
            await document.DeleteAsync();

            string message = "Account Successfully Deleted!";
            string script = "alert('" + message + "')";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

            Response.Redirect("~/MasterForm/AdminAccountForm.aspx", false);
        }
    }
}
