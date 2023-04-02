using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
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

            getManageUsers("Users");
        }

     
        public async void getManageUsers(string Users)
        {
           DataTable usersGridViewTable = new DataTable();
           usersGridViewTable.Columns.Add("username");
           usersGridViewTable.Columns.Add("userRole");
           usersGridViewTable.Columns.Add("email");
           usersGridViewTable.Columns.Add("address");
           usersGridViewTable.Columns.Add("phoneNumber");

           Query usersQue = database.Collection(Users);
           QuerySnapshot snap = await usersQue.GetSnapshotAsync();

           foreach (DocumentSnapshot docsnap in snap.Documents)
           {
               ManageUsers user = docsnap.ConvertTo<ManageUsers>();

               if (docsnap.Exists)
               {              
                   usersGridViewTable.Rows.Add(user.username, user.userRole, user.email, user.address, user.phoneNumber);                  
               }
           }
           manageUsersGridView.DataSource = usersGridViewTable;
           manageUsersGridView.DataBind();


        }
        protected void manageUsersGridView_SelectedIndexChanged1(object sender, EventArgs e)
        {
            GridViewRow gr = manageUsersGridView.SelectedRow;
                    Response.Redirect("UserDetailsPage.aspx?username="+ gr.Cells[0].Text+"&userRole=" + gr.Cells[1].Text +"&email=" + gr.Cells[2].Text + "&address=" + gr.Cells[3].Text + "&contactNumber=" + gr.Cells[4].Text, false);

        }

        protected void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            search();
        }

        // method for searching username 
        public async void search()
        {
            string searchUsername = searchTextBox.Text;

            if (searchTextBox.Text == "")
            {
                getManageUsers("Users");
            }
            else
            {
                Query query = database.Collection("Users")
                        .WhereGreaterThanOrEqualTo("username", searchUsername)
                        .WhereLessThanOrEqualTo("username", searchUsername + "\uf8ff");

                // Retrieve the search results
                QuerySnapshot snapshot = await query.GetSnapshotAsync();
                List<ManageUsers> results = new List<ManageUsers>();

                if (snapshot.Documents.Count > 0)
                {
                    foreach (DocumentSnapshot document in snapshot.Documents)
                    {
                        ManageUsers model = document.ConvertTo<ManageUsers>();
                        results.Add(model);
                    }
                    // Bind the search results to the GridView control
                    manageUsersGridView.DataSource = results;
                    manageUsersGridView.DataBind();
                }
                else
                {
                    manageUsersGridView.DataSource = null;
                    manageUsersGridView.DataBind();

                    string message = "No records found!";
                    string script = "alert('" + message + "')";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                }

            }
        }
    }
}
