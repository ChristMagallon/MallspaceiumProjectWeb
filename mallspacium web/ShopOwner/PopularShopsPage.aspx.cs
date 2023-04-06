using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mallspacium_web.MasterForm2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            getShops();
            /*getCurrentSubDetails();*/
        }

        public void getShops()
        {
            string role = "ShopOwner";
            Query query = database.Collection("Users").WhereEqualTo("userRole", role);
            // Retrieve the documents from the parent collection
            QuerySnapshot querySnapshot = query.GetSnapshotAsync().Result;

            // Create a DataTable to store the retrieved data
            DataTable shopsGridViewTable = new DataTable();

            shopsGridViewTable.Columns.Add("shopName", typeof(string));
            shopsGridViewTable.Columns.Add("shopImage", typeof(byte[]));
            shopsGridViewTable.Columns.Add("shopDescription", typeof(string));

            // Iterate through the documents and populate the DataTable
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                string shopName = documentSnapshot.GetValue<string>("shopName");
                string base64String = documentSnapshot.GetValue<string>("shopImage");
                byte[] shopImage = Convert.FromBase64String(base64String);
                string shopDescription = documentSnapshot.GetValue<string>("shopDescription");

                DataRow dataRow = shopsGridViewTable.NewRow();

                dataRow["shopName"] = shopName;
                dataRow["shopImage"] = shopImage;
                dataRow["shopDescription"] = shopDescription;

                shopsGridViewTable.Rows.Add(dataRow);
            }
            // Bind the DataTable to the GridView control
            shopsGridView.DataSource = shopsGridViewTable;
            shopsGridView.DataBind();
        }

        protected void shopsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                byte[] imageBytes = (byte[])DataBinder.Eval(e.Row.DataItem, "shopImage");
                System.Web.UI.WebControls.Image imageControl = (System.Web.UI.WebControls.Image)e.Row.FindControl("Image1");

                if (imageBytes != null && imageBytes.Length > 0)
                {
                    // Convert the byte array to a base64-encoded string and bind it to the Image control
                    imageControl.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(imageBytes);
                    imageControl.Width = 100; // set the width of the image
                    imageControl.Height = 100; // set the height of the image
                }
                else
                {
                    // If no image is available, show a default image instead
                    imageControl.ImageUrl = "/Images/no-image.jpg";
                    imageControl.Width = 100; // set the width of the image
                    imageControl.Height = 100; // set the height of the image
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(shopsGridView, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to view more details.";
            }
        }

        protected void shopsGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the index of the selected row
            int selectedIndex = shopsGridView.SelectedIndex;

            // Get the value of the shopName column from the DataKeys collection
            string shopName = shopsGridView.DataKeys[selectedIndex].Values["shopName"].ToString();

            // Redirect to another page and pass the shopName as a query string parameter
            Response.Redirect("PopularShopDetailsPage.aspx?shopName=" + shopName);
        }

        /*public async void getCurrentSubDetails()
        {
            DateTime currentDate = DateTime.UtcNow;
            CollectionReference usersRef = database.Collection("AdminManageSubscription");
            DocumentReference docRef = usersRef.Document((string)Application.Get("usernameget"));
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                Dictionary<string, object> data = snapshot.ToDictionary();
                DateTime endDate = DateTime.Parse(data["endDate"].ToString());

                if (currentDate > endDate)
                {
                    await expiredSubscription(data["email"].ToString(), data["userRole"].ToString());
                }
            }
        }

        public async Task expiredSubscription(string userEmail, string userRole)
        {
            String subscriptionType = "Free";
            String subscriptionPrice = "0.00";
            String status = "Expired";

            Random random = new Random();
            int randomIDNumber = random.Next(100000, 999999);
            string subscriptionID = "SUB" + randomIDNumber.ToString();

            DateTime currentDate = DateTime.Now;
            DateTime expirationDate = currentDate.AddMonths(3);
            string startDate = currentDate.ToString("yyyy-MM-dd HH:mm:ss");
            string endDate = expirationDate.ToString("yyyy-MM-dd HH:mm:ss");

            DocumentReference documentRef = database.Collection("AdminManageSubscription").Document(userEmail);
            DocumentSnapshot documentSnapshot = await documentRef.GetSnapshotAsync();

            Dictionary<string, object> dataInsert = new Dictionary<string, object>
            {
                {"subscriptionID", subscriptionID},
                {"subscriptionType", subscriptionType},
                {"price", subscriptionPrice},
                {"userEmail", userEmail},
                {"userRole", userRole},
                {"startDate", startDate},
                {"endDate", endDate},
                {"status", status}
            };

            if (documentSnapshot.Exists)
            {
                Dictionary<string, object> dataUpdate = new Dictionary<string, object>
                {
                    {"subscriptionType", subscriptionType},
                    {"price", subscriptionPrice},
                    {"subscriptionID", subscriptionID},
                    {"startDate", FieldValue.Delete},
                    {"endDate", FieldValue.Delete},
                    {"status", status}
                };

                await documentRef.UpdateAsync(dataUpdate);
            }
            else
            {
                await documentRef.SetAsync(dataInsert);
            }
            Response.Redirect("~/Shopper/PopularShopsPage.aspx", false);
        }*/
        protected void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            search();
        }
        public async void search()
        {
            string searchShopName = searchTextBox.Text;

            if (searchTextBox.Text == "")
            {
                getShops();
            }
            else
            {
                Query query = database.Collection("Users")
                    .WhereGreaterThanOrEqualTo("shopName", searchShopName)
                    .WhereLessThanOrEqualTo("shopName", searchShopName + "\uf8ff");

                // Retrieve the search results
                QuerySnapshot snapshot = await query.GetSnapshotAsync();

                if (snapshot.Documents.Count > 0)
                {
                    // Create a DataTable to store the retrieved data
                    DataTable shopsGridViewTable = new DataTable();

                    shopsGridViewTable.Columns.Add("shopName", typeof(string));
                    shopsGridViewTable.Columns.Add("shopImage", typeof(byte[]));
                    shopsGridViewTable.Columns.Add("shopDescription", typeof(string));

                    // Iterate through the documents and populate the DataTable
                    foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                    {
                        string shopName = documentSnapshot.GetValue<string>("shopName");
                        string base64String = documentSnapshot.GetValue<string>("shopImage");
                        byte[] shopImage = Convert.FromBase64String(base64String);
                        string shopDescription = documentSnapshot.GetValue<string>("shopDescription");

                        DataRow dataRow = shopsGridViewTable.NewRow();

                        dataRow["shopName"] = shopName;
                        dataRow["shopImage"] = shopImage;
                        dataRow["shopDescription"] = shopDescription;

                        shopsGridViewTable.Rows.Add(dataRow);
                    }
                    // Bind the DataTable to the GridView control
                    shopsGridView.DataSource = shopsGridViewTable;
                    shopsGridView.DataBind();
                }
                else
                {
                    // Display an error message if no search results are found
                    errorMessageLabel.Text = "No results found.";
                    errorMessageLabel.Visible = true;
                    shopsGridView.Visible = false;
                }
            }
        }
    }
}