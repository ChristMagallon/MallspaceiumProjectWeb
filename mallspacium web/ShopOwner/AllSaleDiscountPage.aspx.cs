using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.MasterForm2
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
                showAdvertisementPopup();
            }

            getAllSalesAndDiscounts();
        }

        public async void showAdvertisementPopup()
        {
            // Retrieve subscription type from AdminManageSubscriptions collection
            DocumentReference docRef = database.Collection("AdminManageSubscription").Document((string)Application.Get("usernameget"));
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            string subscriptionType = snapshot.GetValue<string>("subscriptionType");

            // Check if subscription type is Free Subscription or Basic Subscription
            if (subscriptionType == "Free Subscription")
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

        public void getAllSalesAndDiscounts()
        {
            CollectionReference usersRef = database.Collection("Users");
            // Retrieve the documents from the parent collection
            QuerySnapshot querySnapshot = usersRef.GetSnapshotAsync().Result;

            // Create a DataTable to store the retrieved data
            DataTable allSaleDiscountGridViewTable = new DataTable();

            allSaleDiscountGridViewTable.Columns.Add("saleDiscShopName", typeof(string));
            allSaleDiscountGridViewTable.Columns.Add("saleDiscImage", typeof(byte[]));
            allSaleDiscountGridViewTable.Columns.Add("saleDiscDesc", typeof(string));

            // Iterate through the documents and populate the DataTable
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                // Create a reference to the child collection inside the parent document
                CollectionReference productsRef = documentSnapshot.Reference.Collection("SaleDiscount");

                // Retrieve the documents from the child collection
                QuerySnapshot productsSnapshot = productsRef.GetSnapshotAsync().Result;

                foreach (DocumentSnapshot productDoc in productsSnapshot.Documents)
                {
                    string saleDiscShopName = productDoc.GetValue<string>("saleDiscShopName");
                    string base64String = productDoc.GetValue<string>("saleDiscImage");
                    byte[] saleDiscImage = Convert.FromBase64String(base64String);
                    string saleDiscDescription = productDoc.GetValue<string>("saleDiscDesc");

                    DataRow dataRow = allSaleDiscountGridViewTable.NewRow();

                    dataRow["saleDiscShopName"] = saleDiscShopName;
                    dataRow["saleDiscImage"] = saleDiscImage;
                    dataRow["saleDiscDesc"] = saleDiscDescription;

                    allSaleDiscountGridViewTable.Rows.Add(dataRow);
                }
            }
            // Bind the DataTable to the GridView control
            allSaleDiscountGridView.DataSource = allSaleDiscountGridViewTable;
            allSaleDiscountGridView.DataBind();
        }

        protected void allSaleDiscountGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                byte[] imageBytes = (byte[])DataBinder.Eval(e.Row.DataItem, "saleDiscImage");
                System.Web.UI.WebControls.Image imageControl = (System.Web.UI.WebControls.Image)e.Row.FindControl("Image1");

                if (imageBytes != null && imageBytes.Length > 0)
                {
                    // Convert the byte array to a base64-encoded string and bind it to the Image control
                    imageControl.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(imageBytes);
                    imageControl.Width = 200; // set the width of the image
                    imageControl.Height = 200; // set the height of the image
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(allSaleDiscountGridView, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to view more details.";
            }
        }

        protected void allSaleDiscountGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the index of the selected row
            int selectedIndex = allSaleDiscountGridView.SelectedIndex;

            // Get the value of the shopName column from the DataKeys collection
            string saleDiscShopName = allSaleDiscountGridView.DataKeys[selectedIndex].Values["saleDiscShopName"].ToString();
            string saleDiscDesc = allSaleDiscountGridView.DataKeys[selectedIndex].Values["saleDiscDesc"].ToString();

            // Redirect to another page and pass the shopName as a query string parameter
            Response.Redirect("AllSaleDiscountDetailsPage.aspx?saleDiscShopName=" + saleDiscShopName + "&saleDiscDesc=" + saleDiscDesc);
        }
    }
}