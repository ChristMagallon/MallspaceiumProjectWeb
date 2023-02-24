using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.form
{
    public partial class RegisterPage : System.Web.UI.Page
    {
        FirestoreDb database;

        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");
        }
     
      protected async void Registerbutton_Click(object sender, EventArgs e)
      {
          Add_Document_with_AutoID();
      }
    
      public void Add_Document_with_AutoID()
      {
          CollectionReference doc = database.Collection("AdminLoginDetails");
          Dictionary<string, object> data1 = new Dictionary<string, object>()
          {
              { "adminUsername", usernameTextbox.Text},
              { "adminEmail", emailTextbox.Text},
              { "adminPhoneNumber", phoneNumberTextbox.Text},
              { "adminPassword", passwordTextbox.Text},
              { "adminConfirmPassword", confirmPasswordTextbox.Text}
          };
          doc.AddAsync(data1);
          Response.Write("<script>alert('Added');</script>");
      }
    }
}