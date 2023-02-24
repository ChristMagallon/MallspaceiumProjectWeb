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
    }
}