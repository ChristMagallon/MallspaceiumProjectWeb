using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mallspacium_web.SuperAdmin
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            profileName.Text = (string)Application.Get("usernameget");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/form/LoginPage.aspx", false);
        }
    }
}