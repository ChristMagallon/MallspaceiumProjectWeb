using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        FirestoreDb database;

        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");
            getManageUsers("AdminManageUsers");
        }
        public async void getManageUsers(string AdminManageUsers)
        {
            Query usersQue = database.Collection(AdminManageUsers);
            QuerySnapshot snap = await usersQue.GetSnapshotAsync();

            

            foreach (DocumentSnapshot docsnap in snap.Documents)
            {
                ManageUsers user = docsnap.ConvertTo<ManageUsers>();

                if (docsnap.Exists)
                {              
                    usersGridViewTable.Rows.Add(user.username, user.id, user.accountType, user.dateCreated, user.email, user.address, user.contactNumber);                  
                }
            }
            manageUsersGridView.DataSource = usersGridViewTable;
            manageUsersGridView.DataBind();

        }

        protected void manageUsersGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gr = manageUsersGridView.SelectedRow;
            Response.Redirect("UserDetailsPage.aspx?username=" + gr.Cells[0].Text + "id" + gr.Cells[1].Text + "accountType" + gr.Cells[2].Text +
                "dateCreated" + gr.Cells[3].Text + "email" + gr.Cells[4].Text + "address" + gr.Cells[5].Text + "contactNumber" + gr.Cells[6].Text, false);
        }
    }
}