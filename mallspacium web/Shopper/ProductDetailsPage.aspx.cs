using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.Shopper
{
    public partial class ProductDetailsPage : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            if (!IsPostBack)
            {
                getProductDetails();
            }
        }

        public async void getProductDetails()
        {
            if (!IsPostBack)
            {
                // Retrieve the shop name from the query string
                string prodShopName = Request.QueryString["prodShopName"];
                string prodName = Request.QueryString["prodName"];

                // Use the shop name to retrieve the data from Firestore
                Query query = database.Collection("Users").WhereEqualTo("shopName", prodShopName);
                QuerySnapshot snapshot = await query.GetSnapshotAsync();

                // Get the first document from the query result (assuming there's only one matching document)
                DocumentSnapshot userDoc = snapshot.Documents.FirstOrDefault();
                if (userDoc != null)
                {
                    // Get the Product collection from the User document
                    CollectionReference productsRef = userDoc.Reference.Collection("Product");

                    // Query the Product collection to get the product with the given document ID (which is equal to the selected prodName value)
                    Query productQuery = productsRef.WhereEqualTo("prodName", prodName);
                    QuerySnapshot productQuerySnapshot = await productQuery.GetSnapshotAsync();

                    // Get the first document from the query result (assuming there's only one matching document)
                    DocumentSnapshot productDoc = productQuerySnapshot.Documents.FirstOrDefault();
                    if (productDoc != null)
                    {
                        // Retrieve the data from the document
                        string desc = productDoc.GetValue<string>("prodDesc");
                        string color = productDoc.GetValue<string>("prodColor");
                        string size = productDoc.GetValue<string>("prodSize");
                        string price = productDoc.GetValue<string>("prodPrice");
                        string tag = productDoc.GetValue<string>("prodTag");
                        string availability = productDoc.GetValue<string>("prodAvailability");
                        string image = productDoc.GetValue<string>("prodImage");

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
                        productNameLabel.Text = prodName;
                        descriptionLabel.Text = desc;
                        colorLabel.Text = color;
                        sizeLabel.Text = size;
                        priceLabel.Text = price;
                        tagLabel.Text = tag;
                        availabilityLabel.Text = availability;
                        shopNameLabel.Text = prodShopName;
                        imageHiddenField.Value = image;
                    }
                    else
                    {
                        Response.Write("<script>alert('Error: Product Not Found.');</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Error: User Not Found.');</script>");
                }
            }
        }

        protected void addWishlistButton_Click(object sender, EventArgs e)
        {
            AddWishlist();
        }

        public async void AddWishlist()
        {
            DocumentReference doc = database.Collection("Users").Document((string)Application.Get("usernameget")).Collection("Wishlist").Document(productNameLabel.Text);
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                { "prodName", productNameLabel.Text},
                { "prodImage", imageHiddenField.Value},
                { "prodDesc", descriptionLabel.Text},
                { "prodColor", colorLabel.Text},
                { "prodSize", sizeLabel.Text },
                { "prodPrice", priceLabel.Text},
                { "prodTag", tagLabel.Text },
                { "prodAvailability", availabilityLabel.Text },
                { "prodShopName", shopNameLabel.Text}
            };
            await doc.SetAsync(data1);
            Response.Write("<script>alert('Successfully Added Shop to the Wishlist.');</script>");


            Query query = database.Collection("Users").WhereEqualTo("shopName", shopNameLabel.Text);
            QuerySnapshot snap = await query.GetSnapshotAsync();

            // Get the first document from the query result (assuming there's only one matching document)
            DocumentSnapshot userDoc = snap.Documents.FirstOrDefault();
            if (userDoc != null)
            {
                // Retrieve the data from the document
                string email = userDoc.GetValue<string>("email");

                // Specify the name of the document using a variable or a string literal
                string documentName = (string)Application.Get("usernameget") + " added your product " + productNameLabel.Text + " to Wishlist.";

                DocumentReference notifRef = database.Collection("Users").Document(email).Collection("Notification").Document(documentName);

                Dictionary<string, object> data = new Dictionary<string, object>
                    {
                        {"notifDetail", "Shopper " + (string)Application.Get("usernameget") + " added your product " + productNameLabel.Text + " to wishlist." },
                        {"notifImage", "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"}
                    };
                await notifRef.SetAsync(data);
            }   
        }
    }
}