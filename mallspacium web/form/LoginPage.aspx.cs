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
            getLoginDetails();
        }

        public async void getLoginDetails()
        {
            Query qRef = db.Collection("Users")
                .WhereEqualTo("shopper_email", EmailTextBox.Text)
                .WhereEqualTo("shopper_password", PasswordTextBox.Text);
            QuerySnapshot snap = await qRef.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap)
            {
                LoginDetails login = docsnap.ConvertTo<LoginDetails>();

                if (!docsnap.Exists)
                    Response.Write("<script>alert('No record');</script>");
                else
                    Response.Write("<script>alert('Login successfully!');</script>");
            }
        }
    }
}