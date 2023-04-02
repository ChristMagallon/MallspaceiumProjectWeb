using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mallspacium_web.ShopOwner
{
    public partial class PopularShopDetailsPage : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            // Retrieve the "shopName" value from the query string
            string shopName = Request.QueryString["shopName"];

            label.Text = shopName + " Details";

            if (!IsPostBack)
            {
                retrieveShopDetails();
                getShopProducts();
                getShopSaleDiscount();
            }
        }

        public async void retrieveShopDetails()
        {
            if (!IsPostBack)
            {
                // Retrieve the shop name from the query string
                string shopName = Request.QueryString["shopName"];

                // Use the shop name to retrieve the data from Firestore
                Query query = database.Collection("Users").WhereEqualTo("shopName", shopName);
                QuerySnapshot snapshot = await query.GetSnapshotAsync();

                // Loop through the documents in the query snapshot
                foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                {
                    // Retrieve the data from the document
                    string image = documentSnapshot.GetValue<string>("shopImage");
                    string name = documentSnapshot.GetValue<string>("shopName");
                    string desc = documentSnapshot.GetValue<string>("shopDescription");
                    string email = documentSnapshot.GetValue<string>("email");
                    string phoneNumber = documentSnapshot.GetValue<string>("phoneNumber");
                    string address = documentSnapshot.GetValue<string>("address");

                    // Convert the image string to a byte array
                    byte[] imageBytes;
                    if (string.IsNullOrEmpty(image))
                    {
                        // If the image string is null or empty, use the default image instead
                        imageBytes = File.ReadAllBytes(Server.MapPath("/Images/no-image.jpg"));
                    }
                    else
                    {
                        imageBytes = Convert.FromBase64String(image);
                    }

                    // Set the image in the FileUpload control
                    string imageBase64String = Convert.ToBase64String(imageBytes);
                    string imageSrc = $"data:image/png;base64,{imageBase64String}";
                    Image1.ImageUrl = imageSrc;

                    // Display the data
                    nameLabel.Text = name;
                    descriptionLabel.Text = desc;
                    emailLabel.Text = email;
                    phoneNumberLabel.Text = phoneNumber;
                    addressLabel.Text = address;
                }
            }
        }

        public async void getShopProducts()
        {
            // Retrieve the shop name from the query string
            string shopName = Request.QueryString["shopName"];

            // Use the shop name to retrieve the data from Firestore
            Query query = database.Collection("Users").WhereEqualTo("shopName", shopName);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // Get the first document from the query result (assuming there's only one matching document)
            DocumentSnapshot userDoc = snapshot.Documents.FirstOrDefault();
            if (userDoc != null)
            {
                // Get the Product collection from the User document
                CollectionReference productRef = userDoc.Reference.Collection("Product");
                QuerySnapshot productSnapshot = await productRef.GetSnapshotAsync();

                // Create a DataTable to store the retrieved data
                DataTable ownProductsGridViewTable = new DataTable();

                ownProductsGridViewTable.Columns.Add("prodName", typeof(string));
                ownProductsGridViewTable.Columns.Add("prodImage", typeof(byte[]));
                ownProductsGridViewTable.Columns.Add("prodDesc", typeof(string));
                ownProductsGridViewTable.Columns.Add("prodPrice", typeof(string));
                ownProductsGridViewTable.Columns.Add("prodTag", typeof(string));
                ownProductsGridViewTable.Columns.Add("prodShopName", typeof(string));

                // Iterate through the documents and populate the DataTable
                foreach (DocumentSnapshot documentSnapshot in productSnapshot.Documents)
                {

                    string productName = documentSnapshot.GetValue<string>("prodName");
                    string base64String = documentSnapshot.GetValue<string>("prodImage");
                    byte[] productImage = Convert.FromBase64String(base64String);
                    string productDescription = documentSnapshot.GetValue<string>("prodDesc");
                    string productPrice = documentSnapshot.GetValue<string>("prodPrice");
                    string productTag = documentSnapshot.GetValue<string>("prodTag");
                    string productShopName = documentSnapshot.GetValue<string>("prodShopName");

                    DataRow dataRow = ownProductsGridViewTable.NewRow();

                    dataRow["prodName"] = productName;
                    dataRow["prodImage"] = productImage;
                    dataRow["prodDesc"] = productDescription;
                    dataRow["prodPrice"] = productPrice;
                    dataRow["prodTag"] = productTag;
                    dataRow["prodShopName"] = productShopName;

                    ownProductsGridViewTable.Rows.Add(dataRow);

                    // Bind the DataTable to the GridView control
                    productGridView.DataSource = ownProductsGridViewTable;
                    productGridView.DataBind();
                }
            } 
            else
            {
                Response.Write("<script>alert('Error: User Not Found.');</script>");
            }
        }

        public async void getShopSaleDiscount()
        {
            // Retrieve the shop name from the query string
            string shopName = Request.QueryString["shopName"];

            // Use the shop name to retrieve the data from Firestore
            Query query = database.Collection("Users").WhereEqualTo("shopName", shopName);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            // Get the first document from the query result (assuming there's only one matching document)
            DocumentSnapshot userDoc = snapshot.Documents.FirstOrDefault();
            if (userDoc != null)
            {
                // Get the Sale Discount collection from the User document
                CollectionReference saleDiscRef = userDoc.Reference.Collection("SaleDiscount");

                // Retrieve the documents from the child collection
                QuerySnapshot querySnapshot = await saleDiscRef.GetSnapshotAsync();

                // Create a DataTable to store the retrieved data
                DataTable saleDiscountGridViewTable = new DataTable();

                //saleDiscountGridViewTable.Columns.Add("shopName", typeof(string));
                saleDiscountGridViewTable.Columns.Add("saleDiscShopName", typeof(string));
                saleDiscountGridViewTable.Columns.Add("saleDiscImage", typeof(byte[]));
                saleDiscountGridViewTable.Columns.Add("saleDiscDesc", typeof(string));

                // Iterate through the documents and populate the DataTable
                foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
                {
                    string saleDiscShopName = documentSnapshot.GetValue<string>("saleDiscShopName");
                    string base64String = documentSnapshot.GetValue<string>("saleDiscImage");
                    byte[] saleDiscImage = Convert.FromBase64String(base64String);
                    string saleDiscDesc = documentSnapshot.GetValue<string>("saleDiscDesc");

                    DataRow dataRow = saleDiscountGridViewTable.NewRow();

                    //dataRow["shopName"] = shopName;
                    dataRow["saleDiscShopName"] = saleDiscShopName;
                    dataRow["saleDiscImage"] = saleDiscImage;
                    dataRow["saleDiscDesc"] = saleDiscDesc;

                    saleDiscountGridViewTable.Rows.Add(dataRow);

                    // Bind the DataTable to the GridView control
                    saleDiscountGridView.DataSource = saleDiscountGridViewTable;
                    saleDiscountGridView.DataBind();
                }
                
            }
            else
            {
                Response.Write("<script>alert('Error: User Not Found.');</script>");
            }
        }

        protected void productGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                byte[] imageBytes = (byte[])DataBinder.Eval(e.Row.DataItem, "prodImage");
                System.Web.UI.WebControls.Image imageControl = (System.Web.UI.WebControls.Image)e.Row.FindControl("Image1");

                if (imageBytes != null && imageBytes.Length > 0)
                {
                    // Convert the byte array to a base64-encoded string and bind it to the Image control
                    imageControl.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(imageBytes);
                    imageControl.Width = 100; // set the width of the image
                    imageControl.Height = 100; // set the height of the image
                }
            }
        }

        protected void saleDiscountGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                byte[] imageBytes = (byte[])DataBinder.Eval(e.Row.DataItem, "saleDiscImage");
                System.Web.UI.WebControls.Image imageControl = (System.Web.UI.WebControls.Image)e.Row.FindControl("Image1");

                if (imageBytes != null && imageBytes.Length > 0)
                {
                    // Convert the byte array to a base64-encoded string and bind it to the Image control
                    imageControl.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(imageBytes);
                    imageControl.Width = 100; // set the width of the image
                    imageControl.Height = 100; // set the height of the image
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(saleDiscountGridView, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to view more details.";
            }
        }

        protected void saleDiscountGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the index of the selected row
            int selectedIndex = saleDiscountGridView.SelectedIndex;

            // Get the value of the shopName column from the DataKeys collection
            string saleDiscShopName = saleDiscountGridView.DataKeys[selectedIndex].Values["saleDiscShopName"].ToString();
            string saleDiscDesc = saleDiscountGridView.DataKeys[selectedIndex].Values["saleDiscDesc"].ToString();

            // Redirect to another page and pass the shopName as a query string parameter
            Response.Redirect("AllSaleDiscountDetailsPage.aspx?saleDiscShopName=" + saleDiscShopName + "&saleDiscDesc=" + saleDiscDesc);
        }

        protected void reportButton_Click(object sender, EventArgs e)
        {
            report();
        }
        public void report() 
        {
            string shopName = nameLabel.Text;

            // Redirect to another page after a delay
            string url = " ReportShopPage.aspx?shopName=" + shopName;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 1000);", true);
        }
    }
}