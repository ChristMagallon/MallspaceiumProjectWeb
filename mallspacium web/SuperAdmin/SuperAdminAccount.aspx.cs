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
    public partial class SuperAdminAccount : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            getAdminAccount("SuperAdminAccount");
        }

        public async void getAdminAccount(string SuperAdminAccount)
        {
            DataTable accountGridViewTable = new DataTable();

            accountGridViewTable.Columns.Add("superAdminId");
            accountGridViewTable.Columns.Add("superAdminEmail");
            accountGridViewTable.Columns.Add("superAdminDateCreated");

            Query subQue = database.Collection(SuperAdminAccount);
            QuerySnapshot snap = await subQue.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap.Documents)
            {
                SuperAdminAccountClass acc = docsnap.ConvertTo<SuperAdminAccountClass>();

                if (docsnap.Exists)
                {
                    accountGridViewTable.Rows.Add(acc.superAdminId, acc.superAdminEmail, acc.superAdminDateCreated);
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
                getAdminAccount("SuperAdminAccount");
            }
            else
            {
                Query query = database.Collection("SuperAdminAccount")
                    .WhereGreaterThanOrEqualTo("superAdminEmail", searchEmail)
                    .WhereLessThanOrEqualTo("superAdminEmail", searchEmail + "\uf8ff");

                // Retrieve the search results
                QuerySnapshot snapshot = await query.GetSnapshotAsync();
                List<SuperAdminAccountClass> results = new List<SuperAdminAccountClass>();

                if (snapshot.Documents.Count > 0)
                {
                    foreach (DocumentSnapshot document in snapshot.Documents)
                    {
                        SuperAdminAccountClass model = document.ConvertTo<SuperAdminAccountClass>();
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
            Response.Redirect("~/SuperAdmin/CreateSuperAdminAccount.aspx", false);
        }

        protected void accountGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Get the selected row
            GridViewRow row = accountGridView.Rows[e.NewEditIndex];

            // Retrieve the document ID from the DataKeys collection
            string superAdminEmail = accountGridView.DataKeys[e.NewEditIndex].Value.ToString();

            // Redirect to the edit page, passing the document ID as a query string parameter
            Response.Redirect("EditSuperAdminAccount.aspx?superAdminEmail=" + superAdminEmail, false);
        }
    }
}