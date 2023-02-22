using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.AdditionalForm
{
    public partial class UserDetailsPage : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            usernameLabel.Text = Request.QueryString["username"].ToString();
            idLabel.Text = Request.QueryString["id"].ToString();
            accountTypeLabel.Text = Request.QueryString["accountType"].ToString();
            dateCreatedLabel.Text = Request.QueryString["dateCreated"].ToString();
            emailLabel.Text = Request.QueryString["email"].ToString();
            addressLabel.Text = Request.QueryString["address"].ToString();
            contactNumberLabel.Text = Request.QueryString["contactNumber"].ToString();
        }

    }
}