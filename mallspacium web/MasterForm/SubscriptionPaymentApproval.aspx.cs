using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.MasterForm
{
    public partial class SubscriptionApproval : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            getSubscription();
        }

        public void getSubscription()
        {
            CollectionReference usersRef = database.Collection("SubscriptionPaymentApproval");
            // Retrieve the documents from the parent collection
            QuerySnapshot querySnapshot = usersRef.GetSnapshotAsync().Result;

            // Create a DataTable to store the retrieved data
            DataTable subscriptionGridViewTable = new DataTable();

            subscriptionGridViewTable.Columns.Add("transactionID", typeof(string));
            subscriptionGridViewTable.Columns.Add("userEmail", typeof(string));
            subscriptionGridViewTable.Columns.Add("subscriptionType", typeof(string));
            subscriptionGridViewTable.Columns.Add("price", typeof(string));
            subscriptionGridViewTable.Columns.Add("userRole", typeof(string));

            foreach (DocumentSnapshot docsnap in querySnapshot.Documents)
            {
                string id = docsnap.GetValue<string>("transactionID");
                string email = docsnap.GetValue<string>("userEmail");
                string type = docsnap.GetValue<string>("subscriptionType");
                string price = docsnap.GetValue<string>("price");
                string role = docsnap.GetValue<string>("userRole");

                DataRow dataRow = subscriptionGridViewTable.NewRow();

                dataRow["transactionID"] = id;
                dataRow["userEmail"] = email;
                dataRow["subscriptionType"] = type;
                dataRow["price"] = price;
                dataRow["userRole"] = role;

                subscriptionGridViewTable.Rows.Add(dataRow);
            }
            subscriptionGridView.DataSource = subscriptionGridViewTable;
            subscriptionGridView.DataBind();
        }

        protected void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            search();
        }

        public async void search()
        {
            string searchEmail = searchTextBox.Text;

            if (searchEmail == "")
            {
                getSubscription();
            }
            else
            {
                Query query = database.Collection("SubscriptionPaymentApproval")
                    .WhereGreaterThanOrEqualTo("userEmail", searchEmail)
                    .WhereLessThanOrEqualTo("userEmail", searchEmail + "\uf8ff");

                // Retrieve the search results
                QuerySnapshot snapshot = await query.GetSnapshotAsync();

                if (snapshot.Documents.Count > 0)
                {
                    // Create a DataTable to store the retrieved data
                    DataTable subscriptionGridViewTable = new DataTable();

                    subscriptionGridViewTable.Columns.Add("transactionID", typeof(string));
                    subscriptionGridViewTable.Columns.Add("userEmail", typeof(string));
                    subscriptionGridViewTable.Columns.Add("subscriptionType", typeof(string));
                    subscriptionGridViewTable.Columns.Add("price", typeof(string));
                    subscriptionGridViewTable.Columns.Add("userRole", typeof(string));

                    // Iterate through the documents and populate the DataTable
                    foreach (DocumentSnapshot docsnap in snapshot.Documents)
                    {
                        string id = docsnap.GetValue<string>("transactionID");
                        string email = docsnap.GetValue<string>("userEmail");
                        string type = docsnap.GetValue<string>("subscriptionType");
                        string price = docsnap.GetValue<string>("price");
                        string role = docsnap.GetValue<string>("userRole");

                        DataRow dataRow = subscriptionGridViewTable.NewRow();

                        dataRow["transactionID"] = id;
                        dataRow["userEmail"] = email;
                        dataRow["subscriptionType"] = type;
                        dataRow["price"] = price;
                        dataRow["userRole"] = role;

                        subscriptionGridViewTable.Rows.Add(dataRow);
                    }
                    subscriptionGridView.DataSource = subscriptionGridViewTable;
                    subscriptionGridView.DataBind();
                }
                else
                {
                    // Display an error message if no search results are found
                    errorMessageLabel.Text = "No results found.";
                    errorMessageLabel.Visible = true;
                    subscriptionGridView.Visible = false;
                }
            }
        }

        protected void subscriptionGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the index of the selected row
            int selectedIndex = subscriptionGridView.SelectedIndex;

            // Get the value of the shopName column from the DataKeys collection
            string userEmail = subscriptionGridView.DataKeys[selectedIndex].Values["userEmail"].ToString();

            // Redirect to another page and pass the shopName as a query string parameter
            Response.Redirect("SubscriptionPaymentApprovalDetails.aspx?userEmail=" + userEmail);
        }

        protected void subscriptionGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(subscriptionGridView, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to view more details.";
            }
        }
    }
}