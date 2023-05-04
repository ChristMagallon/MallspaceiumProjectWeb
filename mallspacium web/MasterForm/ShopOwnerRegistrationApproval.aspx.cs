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
    public partial class ShopOwnerRegistrationApproval : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            getShopOwnerRegistration();
        }

        public void getShopOwnerRegistration()
        {
            CollectionReference usersRef = database.Collection("ShopOwnerRegistrationApproval");
            // Retrieve the documents from the parent collection
            QuerySnapshot querySnapshot = usersRef.GetSnapshotAsync().Result;

            // Create a DataTable to store the retrieved data
            DataTable shopOwnerRegistrationGridViewTable = new DataTable();

            shopOwnerRegistrationGridViewTable.Columns.Add("userID", typeof(string));
            shopOwnerRegistrationGridViewTable.Columns.Add("email", typeof(string));
            shopOwnerRegistrationGridViewTable.Columns.Add("shopName", typeof(string));
            shopOwnerRegistrationGridViewTable.Columns.Add("dateCreated", typeof(string));

            foreach (DocumentSnapshot docsnap in querySnapshot.Documents)
            {
                string userID = docsnap.GetValue<string>("userID");
                string email = docsnap.GetValue<string>("email");
                string shopName = docsnap.GetValue<string>("shopName");
                string date = docsnap.GetValue<string>("dateCreated");

                DataRow dataRow = shopOwnerRegistrationGridViewTable.NewRow();

                dataRow["userID"] = userID;
                dataRow["email"] = email;
                dataRow["shopName"] = shopName;
                dataRow["dateCreated"] = date;

                shopOwnerRegistrationGridViewTable.Rows.Add(dataRow);
            }
            shopOwnerRegistrationGridView.DataSource = shopOwnerRegistrationGridViewTable;
            shopOwnerRegistrationGridView.DataBind();
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
                getShopOwnerRegistration();
            }
            else
            {
                Query query = database.Collection("ShopOwnerRegistrationApproval")
                    .WhereGreaterThanOrEqualTo("email", searchEmail)
                    .WhereLessThanOrEqualTo("email", searchEmail + "\uf8ff");

                // Retrieve the search results
                QuerySnapshot snapshot = await query.GetSnapshotAsync();

                if (snapshot.Documents.Count > 0)
                {
                    // Create a DataTable to store the retrieved data
                    DataTable shopOwnerRegistrationGridViewTable = new DataTable();

                    shopOwnerRegistrationGridViewTable.Columns.Add("userID", typeof(string));
                    shopOwnerRegistrationGridViewTable.Columns.Add("email", typeof(string));
                    shopOwnerRegistrationGridViewTable.Columns.Add("shopName", typeof(string));
                    shopOwnerRegistrationGridViewTable.Columns.Add("dateCreated", typeof(string));

                    foreach (DocumentSnapshot docsnap in snapshot.Documents)
                    {
                        string userID = docsnap.GetValue<string>("userID");
                        string email = docsnap.GetValue<string>("email");
                        string shopName = docsnap.GetValue<string>("shopName");
                        string date = docsnap.GetValue<string>("dateCreated");

                        DataRow dataRow = shopOwnerRegistrationGridViewTable.NewRow();

                        dataRow["userID"] = userID;
                        dataRow["email"] = email;
                        dataRow["shopName"] = shopName;
                        dataRow["dateCreated"] = date;

                        shopOwnerRegistrationGridViewTable.Rows.Add(dataRow);
                    }
                    shopOwnerRegistrationGridView.DataSource = shopOwnerRegistrationGridViewTable;
                    shopOwnerRegistrationGridView.DataBind();
                }
                else
                {
                    // Display an error message if no search results are found
                    errorMessageLabel.Text = "No results found.";
                    errorMessageLabel.Visible = true;
                    shopOwnerRegistrationGridView.Visible = false;
                }
            }
        }

        protected void shopOwnerRegistrationGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the index of the selected row
            int selectedIndex = shopOwnerRegistrationGridView.SelectedIndex;

            // Get the value of the shopName column from the DataKeys collection
            string email = shopOwnerRegistrationGridView.DataKeys[selectedIndex].Values["email"].ToString();

            // Redirect to another page and pass the shopName as a query string parameter
            Response.Redirect("ShopOwnerRegistrationApprovalDetails.aspx?email=" + email, false);
        }

        protected void shopOwnerRegistrationGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(shopOwnerRegistrationGridView, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to view more details.";
            }
        }
    }
}