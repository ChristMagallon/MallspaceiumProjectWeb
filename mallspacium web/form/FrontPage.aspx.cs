using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;

namespace mallspacium_web.form
{
    public partial class FrontPage : System.Web.UI.Page
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "mEkB17eA9jG99nE8XEGOsV1vXBvNLHjbF0N4pFmV",
            BasePath = "https://mallspaceium-default-rtdb.asia-southeast1.firebasedatabase.app/"

        };

        IFirebaseClient client;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
            }
            catch
            {
                Response.Write("<script>alert('No Internet or Connection Problem');</script>");
            }
        }

        public static string usernamepass;
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(usernameTextbox.Text) || string.IsNullOrEmpty(passwordTextbox.Text))
            {
                //check if username and password textbox is empty
                Response.Write("<script>alert('Please input username and password');</script>");
            }
            else 
            {
                //looping to get the match data using foreach
                FirebaseResponse response = client.Get("AdminLoginDetails");
                Dictionary<string, Data> result = response.ResultAs<Dictionary<string, Data>>();

                foreach (var get in result)
                {
                    string userresult = get.Value.username;
                    string passresult = get.Value.password;

                    if (usernameTextbox.Text == userresult)
                    {

                        if (passwordTextbox.Text == passresult)
                        {

                            Response.Write("<script>alert('Login Successfully');</script>");
                            //Declare some public string so you can pass the data to the another Frame.
                            usernamepass = usernameTextbox.Text;
                        }

                    }
                    else 
                    {
                        Response.Write("<script>alert('No record');</script>");
                    }
                }
            }
        } 
    }
}