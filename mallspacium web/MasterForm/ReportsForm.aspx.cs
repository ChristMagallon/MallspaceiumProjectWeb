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

            getAdminReport("AdminReport");
        }

        public async void getAdminReport(string AdminReport)
        {
            DataTable reportGridViewTable = new DataTable();

            reportGridViewTable.Columns.Add("reportedUsername");
            reportGridViewTable.Columns.Add("reportId");
            reportGridViewTable.Columns.Add("reportMessage");
            reportGridViewTable.Columns.Add("reportedBy");
            reportGridViewTable.Columns.Add("reportDate");

            Query subQue = database.Collection(AdminReport);
            QuerySnapshot snap = await subQue.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap.Documents)
            {
                Report rep = docsnap.ConvertTo<Report>();

                if (docsnap.Exists)
                {
                    reportGridViewTable.Rows.Add(rep.reportedUsername, rep.reportId, rep.reportMessage, rep.reportedBy,  rep.reportDate);
                }
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
            string searchUsername = searchTextBox.Text;
            Query query = database.Collection("AdminReport")
                          .WhereEqualTo("reportedUsername", searchUsername);

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

        protected void reportGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gr = reportGridView.SelectedRow;
            Response.Redirect("ResolveReportForm.aspx?reportedUsername=" + gr.Cells[0].Text + "&reportId=" + gr.Cells[1].Text + "&reportMessage=" + gr.Cells[2].Text + "&reportedBy=" + gr.Cells[3].Text + "&reportDate=" + gr.Cells[4].Text, false);

        }
    }
}