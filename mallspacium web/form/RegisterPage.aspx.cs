using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace mallspacium_web.form
{
    public partial class RegisterPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void Registerbutton_Click(object sender, EventArgs e)
        {
            Register();
        }

        public void Register()
        {
            string authSecret = "mEkB17eA9jG99nE8XEGOsV1vXBvNLHjbF0N4pFmV";
            string basePath = "https://mallspaceium-default-rtdb.asia-southeast1.firebasedatabase.app/";
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "mEkB17eA9jG99nE8XEGOsV1vXBvNLHjbF0N4pFmV",
                BasePath = "https://mallspaceium-default-rtdb.asia-southeast1.firebasedatabase.app/"

            };
            IFirebaseClient client;
            client = new FireSharp.FirebaseClient(config);

            if (string.IsNullOrEmpty(basePath) && !string.IsNullOrEmpty(authSecret))
            {
                Response.Write("<script>alert('Please input the necessary information');</script>");
            }
            else 
            { 
                var data = new Data
                {
                    username = usernameTextbox.Text,
                    email = emailTextbox.Text,
                    phoneNumber = phoneNumberTextbox.Text,
                    password = passwordTextbox.Text,
                    confirmPassword = confirmPasswordTextbox.Text
                };
                var response = client.Push("AdminLoginDetails", data);
                Response.Write("<script>alert('Successfully Registered!');</script>");

                usernameTextbox.Text = string.Empty;
                emailTextbox.Text = string.Empty;
                phoneNumberTextbox.Text = string.Empty;
                passwordTextbox.Text = string.Empty;
                confirmPasswordTextbox.Text = string.Empty;
            }

        }
    }
}