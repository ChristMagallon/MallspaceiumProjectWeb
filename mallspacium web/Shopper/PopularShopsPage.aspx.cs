using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Google.Cloud.Firestore;

namespace mallspacium_web.MasterForm3
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            if (!IsPostBack)
            {
                getShops();
                showAdvertisementPopup();
            }
        }

        public async void showAdvertisementPopup()
        {
            // Retrieve subscription type from AdminManageSubscriptions collection
            DocumentReference docRef = database.Collection("AdminManageSubscription").Document((string)Application.Get("usernameget"));
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            string subscriptionType = snapshot.GetValue<string>("subscriptionType");

            // Check if subscription type is Free Subscription or Basic Subscription
            if (subscriptionType == "Free Subscription" || subscriptionType == "Basic Subscription" || subscriptionType == "Advanced Subscription")
            {
                // Query AllAdvertisement collection for documents
                QuerySnapshot adSnapshot = await database.Collection("AllAdvertisement").GetSnapshotAsync();

                // If documents exist
                if (adSnapshot.Documents.Count > 0)
                {
                    // Get a random document
                    DocumentSnapshot randomDoc = adSnapshot.Documents[new Random().Next(adSnapshot.Documents.Count)];

                    // Retrieve adProdImage string from selected document
                    string adsProdImage = randomDoc.GetValue<string>("adsProdImage");

                    // Convert the image string to a byte array
                    byte[] imageBytes = Convert.FromBase64String(adsProdImage);

                    // Get the image height and width
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        using (System.Drawing.Image img = System.Drawing.Image.FromStream(ms))
                        {
                            // Set the height and width of the popup window
                            int height = img.Height;
                            int width = img.Width;

                            // Create a Base64-encoded data URL for the image
                            string imageBase64String = Convert.ToBase64String(imageBytes);
                            string imageSrc = $"data:image/jpeg;base64,{imageBase64String}";

                            // Register the script to open the popup window using ScriptManager
                            string script = $"var w = window.open('', '', 'height={height},width={width},status=yes,toolbar=no,menubar=no,location=no,resizable=no'); w.document.write('<html><body><img src=\"{imageSrc}\"/></body></html>');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupWindow", script, true);
                        }
                    }
                }
            }
        }

        public void getShops()
        {
            string role = "ShopOwner";
            // Query shops with the specified userRole
            Query query = database.Collection("Users").WhereEqualTo("userRole", role);

            // Retrieve all documents from the query
            List<DocumentSnapshot> documentSnapshots = query.GetSnapshotAsync().Result.Documents.ToList();

            // Sort the documents by counterPopularity in descending order
            documentSnapshots = documentSnapshots.OrderByDescending(snapshot => snapshot.GetValue<int>("counterPopularity")).ToList();

            // Create a DataTable to store the retrieved data
            DataTable shopsGridViewTable = new DataTable();

            shopsGridViewTable.Columns.Add("shopName", typeof(string));
            shopsGridViewTable.Columns.Add("shopImage", typeof(byte[]));
            shopsGridViewTable.Columns.Add("shopDescription", typeof(string));

            // Iterate through the sorted documents and populate the DataTable
            foreach (DocumentSnapshot documentSnapshot in documentSnapshots)
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

        protected async void shopsGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the index of the selected row
            int selectedIndex = shopsGridView.SelectedIndex;

            // Get the value of the shopName column from the DataKeys collection
            string shopName = shopsGridView.DataKeys[selectedIndex].Values["shopName"].ToString();

            // Get current date time of the account created
            DateTime currentDate = DateTime.Now;
            string dateVisited = currentDate.ToString("yyyy-MM-dd HH:mm:ss");

            // Use the shop name to retrieve the data from Firestore
            Query query = database.Collection("Users").WhereEqualTo("shopName", shopName);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // Get the first document from the query result (assuming there's only one matching document)
            DocumentSnapshot userDoc = snapshot.Documents.FirstOrDefault();
            if (userDoc != null)
            {
                // Increment the counterPopularity field by 1
                int counterPopularity = userDoc.GetValue<int>("counterPopularity");
                counterPopularity++;

                // Update the counterPopularity field in the database
                DocumentReference docRef = database.Collection("Users").Document(userDoc.Id);
                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    { "counterPopularity", counterPopularity }
                };
                await docRef.UpdateAsync(data);

                // Iterate through the documents and populate the DataTable
                foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                {
                    string shopDescription = documentSnapshot.GetValue<string>("shopDescription");
                    string shopImage = documentSnapshot.GetValue<string>("shopImage");
                    string email = documentSnapshot.GetValue<string>("email");
                    string phoneNumber = documentSnapshot.GetValue<string>("phoneNumber");
                    string address = documentSnapshot.GetValue<string>("address");

                    DocumentReference visitedShopRef = database.Collection("Users").Document((string)Application.Get("usernameget")).Collection("VisitedShop").Document(shopName);
                    Dictionary<string, object> visitedShopData = new Dictionary<string, object>()
                    {
                        { "shopName", shopName},
                        { "shopDescription", shopDescription},
                        { "shopImage", shopImage},
                        { "email", email},
                        { "phoneNumber", phoneNumber },
                        { "address", address},
                        { "dateVisited", dateVisited}
                    };
                    await visitedShopRef.SetAsync(visitedShopData);
                }
            }
            Response.Redirect("PopularShopDetailsPage.aspx?shopName=" + shopName, false);
        }

        protected void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            search();
        }

        public async void search()
        {
            string searchShopName = searchTextBox.Text.ToLower(); // convert search term to lowercase

            if (searchShopName == "")
            {
                getShops();
            }
            else
            {
                Query query = database.Collection("Users")
                    .WhereGreaterThanOrEqualTo("shopName", searchTextBox.Text)
                    .WhereLessThanOrEqualTo("shopName", searchTextBox.Text + "\uf8ff");

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
                        string shopNameLowercase = shopName.ToLower(); // convert field value to lowercase
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