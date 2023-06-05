using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mallspacium_web.SuperAdmin
{
    public partial class ManageAdmin : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            getAdminAccount("AdminAccount");
        }
        public async void getAdminAccount(string AdminAccount)
        {
            DataTable accountGridViewTable = new DataTable();

            accountGridViewTable.Columns.Add("adminId");
            accountGridViewTable.Columns.Add("adminEmail");
            accountGridViewTable.Columns.Add("adminDateCreated");

            Query subQue = database.Collection(AdminAccount);
            QuerySnapshot snap = await subQue.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap.Documents)
            {
                AdminAccount acc = docsnap.ConvertTo<AdminAccount>();

                if (docsnap.Exists)
                {
                    accountGridViewTable.Rows.Add(acc.adminId, acc.adminEmail, acc.adminDateCreated);
                }
            }
            accountGridView.DataSource = accountGridViewTable;
            accountGridView.DataBind();
        }

        protected void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            search();
        }

        // method for searching admin email 
        public async void search()
        {
            string searchEmail = searchTextBox.Text;

            if (searchTextBox.Text == "")
            {
                getAdminAccount("AdminAccount");
            }
            else
            {
                Query query = database.Collection("AdminAccount")
                    .WhereGreaterThanOrEqualTo("adminEmail", searchEmail)
                    .WhereLessThanOrEqualTo("adminEmail", searchEmail + "\uf8ff");

                // Retrieve the search results
                QuerySnapshot snapshot = await query.GetSnapshotAsync();
                List<AdminAccount> results = new List<AdminAccount>();

                if (snapshot.Documents.Count > 0)
                {
                    foreach (DocumentSnapshot document in snapshot.Documents)
                    {
                        AdminAccount model = document.ConvertTo<AdminAccount>();
                        results.Add(model);
                    }
                    // Bind the search results to the GridView control
                    accountGridView.DataSource = results;
                    accountGridView.DataBind();
                }
                else
                {
                    accountGridView.DataSource = null;
                    accountGridView.DataBind();

                    string message = "No records found!";
                    string script = "alert('" + message + "')";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                }

            }
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SuperAdmin/CreateAdminAccount.aspx", false);
        }

        protected void accountGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Get the selected row
            GridViewRow row = accountGridView.Rows[e.NewEditIndex];

            // Retrieve the document ID from the DataKeys collection
            string adminEmail = accountGridView.DataKeys[e.NewEditIndex].Value.ToString();

            // Redirect to the edit page, passing the document ID as a query string parameter
            Response.Redirect("EditAdminAccount.aspx?adminEmail=" + adminEmail, false);
        }

    }
}