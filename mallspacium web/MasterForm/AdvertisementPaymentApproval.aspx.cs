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
    public partial class AdvertisementPaymentApproval : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            getAdvertisement();
        }

        public void getAdvertisement()
        {
            CollectionReference usersRef = database.Collection("AdvertisementPaymentApproval");
            // Retrieve the documents from the parent collection
            QuerySnapshot querySnapshot = usersRef.GetSnapshotAsync().Result;

            // Create a DataTable to store the retrieved data
            DataTable advertisementGridViewTable = new DataTable();

            advertisementGridViewTable.Columns.Add("transactionID", typeof(string));
            advertisementGridViewTable.Columns.Add("userEmail", typeof(string));
            advertisementGridViewTable.Columns.Add("advertisementID", typeof(string));
            advertisementGridViewTable.Columns.Add("price", typeof(string));

            foreach (DocumentSnapshot docsnap in querySnapshot.Documents)
            {
                string transId = docsnap.GetValue<string>("transactionID");
                string email = docsnap.GetValue<string>("userEmail");
                string adsID = docsnap.GetValue<string>("advertisementID");
                string price = docsnap.GetValue<string>("price");

                DataRow dataRow = advertisementGridViewTable.NewRow();

                dataRow["transactionID"] = transId;
                dataRow["userEmail"] = email;
                dataRow["advertisementID"] = adsID;
                dataRow["price"] = price;

                advertisementGridViewTable.Rows.Add(dataRow);
            }
            advertisementGridView.DataSource = advertisementGridViewTable;
            advertisementGridView.DataBind();
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
                getAdvertisement();
            }
            else
            {
                Query query = database.Collection("AdvertisementPaymentApproval")
                    .WhereGreaterThanOrEqualTo("userEmail", searchEmail)
                    .WhereLessThanOrEqualTo("userEmail", searchEmail + "\uf8ff");

                // Retrieve the search results
                QuerySnapshot snapshot = await query.GetSnapshotAsync();

                if (snapshot.Documents.Count > 0)
                {
                    // Create a DataTable to store the retrieved data
                    DataTable advertisementGridViewTable = new DataTable();

                    advertisementGridViewTable.Columns.Add("transactionID", typeof(string));
                    advertisementGridViewTable.Columns.Add("userEmail", typeof(string));
                    advertisementGridViewTable.Columns.Add("advertisementID", typeof(string));
                    advertisementGridViewTable.Columns.Add("price", typeof(string));

                    // Iterate through the documents and populate the DataTable
                    foreach (DocumentSnapshot docsnap in snapshot.Documents)
                    {
                        string id = docsnap.GetValue<string>("transactionID");
                        string email = docsnap.GetValue<string>("userEmail");
                        string type = docsnap.GetValue<string>("advertisementID");
                        string price = docsnap.GetValue<string>("price");

                        DataRow dataRow = advertisementGridViewTable.NewRow();

                        dataRow["transactionID"] = id;
                        dataRow["userEmail"] = email;
                        dataRow["advertisementID"] = type;
                        dataRow["price"] = price;

                        advertisementGridViewTable.Rows.Add(dataRow);
                    }
                    advertisementGridView.DataSource = advertisementGridViewTable;
                    advertisementGridView.DataBind();
                }
                else
                {
                    // Display an error message if no search results are found
                    errorMessageLabel.Text = "No results found.";
                    errorMessageLabel.Visible = true;
                    advertisementGridView.Visible = false;
                }
            }
        }

        protected void advertisementGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the index of the selected row
            int selectedIndex = advertisementGridView.SelectedIndex;

            // Get the value of the shopName column from the DataKeys collection
            string userEmail = advertisementGridView.DataKeys[selectedIndex].Values["userEmail"].ToString();

            // Redirect to another page and pass the shopName as a query string parameter
            Response.Redirect("AdvertisementPaymentApprovalDetails.aspx?userEmail=" + userEmail, false);
        }

        protected void advertisementGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(advertisementGridView, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to view more details.";
            }
        }
    }
}