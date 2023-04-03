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
    public partial class WebForm4 : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            getAdminReport();
        }

        public void getAdminReport()
        {
            CollectionReference usersRef = database.Collection("AdminReport");
            // Retrieve the documents from the parent collection
            QuerySnapshot querySnapshot = usersRef.GetSnapshotAsync().Result;

            // Create a DataTable to store the retrieved data
            DataTable reportGridViewTable = new DataTable();

            reportGridViewTable.Columns.Add("id", typeof(string));
            reportGridViewTable.Columns.Add("shopName", typeof(string));
            reportGridViewTable.Columns.Add("reason", typeof(string));
            reportGridViewTable.Columns.Add("reportedBy", typeof(string));
            reportGridViewTable.Columns.Add("status", typeof(string));

            foreach (DocumentSnapshot docsnap in querySnapshot.Documents)
            {
                string id = docsnap.GetValue<string>("id");
                string shopName = docsnap.GetValue<string>("shopName");
                string reason = docsnap.GetValue<string>("reason");
                string reportedBy = docsnap.GetValue<string>("reportedBy");
                string status= docsnap.GetValue<string>("status");

                DataRow dataRow = reportGridViewTable.NewRow();

                dataRow["id"] = id;
                dataRow["shopName"] = shopName;
                dataRow["reason"] = reason;
                dataRow["reportedBy"] = reportedBy;
                dataRow["status"] = status;

                reportGridViewTable.Rows.Add(dataRow);
            }
            reportGridView.DataSource = reportGridViewTable;
            reportGridView.DataBind();
        }

        protected void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            search();
        }

        // method for searching username 
        public async void search()
        {
            string searchShopName = searchTextBox.Text;

            if (searchTextBox.Text == "")
            {
                getAdminReport();
            }
            else
            {
                Query query = database.Collection("Users")
                    .WhereGreaterThanOrEqualTo("shopName", searchShopName)
                    .WhereLessThanOrEqualTo("shopName", searchShopName + "\uf8ff");

                // Retrieve the search results
                QuerySnapshot snapshot = await query.GetSnapshotAsync();
                List<Report> results = new List<Report>();

                if (snapshot.Documents.Count > 0)
                {
                    foreach (DocumentSnapshot document in snapshot.Documents)
                    {
                        Report model = document.ConvertTo<Report>();
                        results.Add(model);
                    }
                    // Bind the search results to the GridView control
                    reportGridView.DataSource = results;
                    reportGridView.DataBind();
                }
                else
                {
                    reportGridView.DataSource = null;
                    reportGridView.DataBind();

                    string message = "No records found!";
                    string script = "alert('" + message + "')";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                }

            }
        }

        protected void reportGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the index of the selected row
            int selectedIndex = reportGridView.SelectedIndex;

            // Get the value of the shopName column from the DataKeys collection
            string id = reportGridView.DataKeys[selectedIndex].Values["id"].ToString();

            // Redirect to another page and pass the shopName as a query string parameter
            Response.Redirect("ReportDetailsForm.aspx?id=" + id);
        }

        protected void reportGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(reportGridView, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to view proof.";
            }
        }
    }
}