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
    public partial class WebForm2 : System.Web.UI.Page
    {
        FirestoreDb database;

        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            getManageSubscription("AdminManageSubscription");
        }

        public async void getManageSubscription(string AdminManageSubscription)
        {
            DataTable subscriptionGridViewTable = new DataTable();

            subscriptionGridViewTable.Columns.Add("subscriptionId");
            subscriptionGridViewTable.Columns.Add("subscriptionType");
            subscriptionGridViewTable.Columns.Add("username");
            subscriptionGridViewTable.Columns.Add("price");
            subscriptionGridViewTable.Columns.Add("startDate");
            subscriptionGridViewTable.Columns.Add("endDate");
            subscriptionGridViewTable.Columns.Add("status");

            Query subQue = database.Collection(AdminManageSubscription);
            QuerySnapshot snap = await subQue.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap.Documents)
            {
                ManageSubscription sub = docsnap.ConvertTo<ManageSubscription>();

                if (docsnap.Exists)
                {
                    subscriptionGridViewTable.Rows.Add(sub.subscriptionId, sub.subscriptionType, sub.username, sub.price, sub.startDate, sub.endDate, 
                        sub.status);
                }
            }
            manageSubscriptionGridView.DataSource = subscriptionGridViewTable;
            manageSubscriptionGridView.DataBind();

        }

        protected void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            search();
        }

        // method for searching username 
        public async void search()
        {
            string searchUsername = searchTextBox.Text;
            Query query = database.Collection("AdminManageSubscription")
                          .WhereEqualTo("username", searchUsername);

            // Retrieve the search results
            QuerySnapshot snapshot = await query.GetSnapshotAsync();
            List<ManageSubscription> results = new List<ManageSubscription>();

            if (snapshot.Documents.Count > 0)
            {
                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    ManageSubscription model = document.ConvertTo<ManageSubscription>();
                    results.Add(model);
                }
                // Bind the search results to the GridView control
                manageSubscriptionGridView.DataSource = results;
                manageSubscriptionGridView.DataBind();
            }
            else
            {
                manageSubscriptionGridView.DataSource = null;
                manageSubscriptionGridView.DataBind();
                string message = "No records found!";
                string script = "alert('" + message + "')";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }

        }
    }
}