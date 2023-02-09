using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace mallspacium_web
{
    public partial class ManageUsersPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FireBaseClient(ifc);
            }
            catch
            {
                MessageBox.Show("there was a problem in you internet");
            }
        }
    }
}