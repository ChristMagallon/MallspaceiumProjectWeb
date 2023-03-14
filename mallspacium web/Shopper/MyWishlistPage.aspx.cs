using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mallspacium_web.Shopper
{
    public partial class MyWishlistPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void myWishlistGridView_RowRemoving(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void myWishlistGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find the automatically generated Delete button
                Button btnDelete = e.Row.Cells[0].Controls[0] as Button;
                if (btnDelete != null && btnDelete.CommandName == "Delete")
                {
                    // Change the button text to "Remove"
                    btnDelete.Text = "Remove";
                }
            }
        }
    }
}