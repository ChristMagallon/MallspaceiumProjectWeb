using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.ShopOwner
{
    public partial class AllSaleDiscountDetailsPage : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            retrieveSaleDiscountDetails();
        }

        public async void retrieveSaleDiscountDetails()
        {
            if (!IsPostBack)
            {
                // Retrieve the shop name from the query string
                string saleDiscShopName = Request.QueryString["saleDiscShopName"];
                string saleDiscDesc = Request.QueryString["saleDiscDesc"];

                // Use the shop name to retrieve the data from Firestore
                Query query = database.Collection("Users").WhereEqualTo("shopName", saleDiscShopName);
                QuerySnapshot snapshot = await query.GetSnapshotAsync();

                // Get the first document from the query result (assuming there's only one matching document)
                DocumentSnapshot userDoc = snapshot.Documents.FirstOrDefault();
                if (userDoc != null)
                {
                    // Get the Sale Discount collection from the User document
                    CollectionReference saleDiscRef = userDoc.Reference.Collection("SaleDiscount");

                    // Query the Sale Discount collection to get the sales and discount with the given document ID (which is equal to the selected saleDiscDesc value)
                    Query saleDiscQuery = saleDiscRef.WhereEqualTo("saleDiscDesc", saleDiscDesc);
                    QuerySnapshot saleDiscQuerySnapshot = await saleDiscQuery.GetSnapshotAsync();

                    // Get the first document from the query result (assuming there's only one matching document)
                    //DocumentSnapshot saleDiscDoc = saleDiscQuerySnapshot.Documents.FirstOrDefault();
                    //if (saleDiscDoc != null)
                    //{
                        // Loop through the documents in the query snapshot
                        foreach (DocumentSnapshot documentSnapshot in saleDiscQuerySnapshot.Documents)
                        {
                            // Retrieve the data from the document
                            string shopName = documentSnapshot.GetValue<string>("saleDiscShopName");
                            string image = documentSnapshot.GetValue<string>("saleDiscImage");
                            string description = documentSnapshot.GetValue<string>("saleDiscDesc");
                            string startDate = documentSnapshot.GetValue<string>("saleDiscStartDate");
                            string endDate = documentSnapshot.GetValue<string>("saleDiscEndDate");

                            // Convert the image string to a byte array
                            byte[] imageBytes;
                            imageBytes = Convert.FromBase64String(image);

                            // Set the image in the FileUpload control
                            string imageBase64String = Convert.ToBase64String(imageBytes);
                            string imageSrc = $"data:image/png;base64,{imageBase64String}";
                            Image1.ImageUrl = imageSrc;

                            // Display the data
                            shopNameLabel.Text = shopName;
                            descriptionLabel.Text = description;
                            startDateLabel.Text = startDate;
                            endDateLabel.Text = endDate;
                        }
                    //}
                    //else
                    //{
                        //Response.Write("<script>alert('Error: Sale or Discount not found.');</script>");
                    //}
                }
                else
                {
                    Response.Write("<script>alert('Error: User not found.');</script>");
                }
            }
        }
    }
}