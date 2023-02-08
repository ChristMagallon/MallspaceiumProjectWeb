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
            Response.Write("<script>alert('Connected');</script>");
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            login();
        }

        public void login()
        {
            

            
            

        }

       
    }
}