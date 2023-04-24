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
                        {"message", "Shopper " + (string)Application.Get("usernameget") + " added your product " + productNameLabel.Text + " to wishlist." }
                    };
                await notifRef.SetAsync(data);
            }   
        }
    }
}