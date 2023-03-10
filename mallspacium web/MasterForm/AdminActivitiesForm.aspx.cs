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
    public partial class WebForm3 : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            getAdminActivity("AdminActivity");
        }

        public async void getAdminActivity(string AdminActivity)
        {
            DataTable activityGridViewTable = new DataTable();

            activityGridViewTable.Columns.Add("activityId");
            activityGridViewTable.Columns.Add("activity");
            activityGridViewTable.Columns.Add("username");
            activityGridViewTable.Columns.Add("date");

            Query subQue = database.Collection(AdminActivity);
            QuerySnapshot snap = await subQue.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap.Documents)
            {
                Activity act = docsnap.ConvertTo<Activity>();

                if (docsnap.Exists)
                {
                    activityGridViewTable.Rows.Add(act.activityId, act.activity, act.username, act.date);
                }
            }
            activityGridView.DataSource = activityGridViewTable;
            activityGridView.DataBind();

        }

        protected void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            search();
        }

        // method for searching username 
        public async void search()
        {
            string searchUsername = searchTextBox.Text;
            Query query = database.Collection("AdminActivity")
                          .WhereEqualTo("username", searchUsername);

            // Retrieve the search results
            QuerySnapshot snapshot = await query.GetSnapshotAsync();
            List<Activity> results = new List<Activity>();

            if (snapshot.Documents.Count > 0)
            {
                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    Activity model = document.ConvertTo<Activity>();
                    results.Add(model);
                }
                // Bind the search results to the GridView control
                activityGridView.DataSource = results;
                activityGridView.DataBind();
            }
            else
            {
                activityGridView.DataSource = null;
                activityGridView.DataBind();
                string message = "No records found!";
                string script = "alert('" + message + "')";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }

        }
    }
}