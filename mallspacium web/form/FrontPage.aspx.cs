using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.form
{
    public partial class FrontPage : System.Web.UI.Page
    {
        FirestoreDb database;

        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            getLoginDetails();
        }

        public async void getLoginDetails()
        {
            Query qRef = database.Collection("AdminLoginDetails").WhereEqualTo("adminUsername", usernameTextbox.Text)
                                                                 .WhereEqualTo("adminPassword", passwordTextbox.Text);
            QuerySnapshot snap = await qRef.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap)
            {
                LoginDetails login = docsnap.ConvertTo<LoginDetails>(); 
                
                if (docsnap.Exists)
                    Response.Redirect("~/MasterForm/AdminActivitiesForm.aspx");
                else
                    Response.Write("<script>alert('No record');</script>");
            }
        }
    }
}