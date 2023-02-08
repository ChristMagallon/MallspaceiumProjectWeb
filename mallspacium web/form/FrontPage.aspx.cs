using System;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

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
            client = new FireSharp.FirebaseClient(config);

            if (client != null)
            {
                Response.Write("<script>alert('Connected');</script>");
            }
        }
    }
}