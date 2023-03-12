using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.MasterForm
{
    public partial class CreateAdminAccountForm : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            // Set the initial value of the TextBox control to today's date and time
            DateTime currentDate = DateTime.Now;
            dateCreatedTextbox.Text = currentDate.ToString();

            // Make the TextBox control read-only
            dateCreatedTextbox.Enabled = false;
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            AddAccount();
        }

        public async void AddAccount()
        {
            //auto generated unique id
            Guid id = Guid.NewGuid();
            string uniqueId = id.ToString();

            DocumentReference doc = database.Collection("AdminAccount").Document(usernameTextbox.Text);

            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
              { "adminUsername", usernameTextbox.Text},
              {"adminId", uniqueId},
              { "adminEmail", emailTextbox.Text},
              { "adminPhoneNumber", phoneNumberTextbox.Text},
              { "adminDateCreated", dateCreatedTextbox.Text},
              { "adminPassword", passwordTextbox.Text},
              { "adminConfirmPassword", confirmPasswordTextbox.Text}
            };
            await doc.SetAsync(data1); 
            Response.Write("<script>alert('Successfully added a new admin account.');</script>");

            Response.Redirect("~/MasterForm/AdminAccountForm.aspx", false);
        }
    }
}