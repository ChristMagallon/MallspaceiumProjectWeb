using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.ShopOwner
{
    public partial class AddSalesAndDiscountPage : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");
            shopNameTextbox.Enabled = false;

            getShopName();
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            AddSaleDiscount();
        }
        public async void AddSaleDiscount()
        {
            //auto generated unique id
            Guid id = Guid.NewGuid();
            string uniqueId = id.ToString();

            //Create an instance of Bitmap from the uploaded file using the FileUpload control
            Bitmap image = new Bitmap(imageFileUpload.PostedFile.InputStream);
            MemoryStream stream = new MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] bytes = stream.ToArray();

            //Convert the Bitmap image to a Base64 string
            string base64String = Convert.ToBase64String(bytes);

            DocumentReference doc = database.Collection("Users").Document((string)Application.Get("usernameget")).Collection("SaleDiscount").Document(uniqueId);
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                { "saleDiscId", uniqueId},
                { "saleDiscImage", base64String},
                { "saleDiscDesc", descriptionTextbox.Text},
                { "saleDiscStartDate", startDateTextBox.Text},
                { "saleDiscEndDate", endDateTextBox.Text },
                { "saleDiscShopName", shopNameTextbox.Text }
            };

            if (imageFileUploadValidator.IsValid && descriptionTextboxValidator.IsValid && startDateTextboxValidator.IsValid && endDateTextboxValidator.IsValid)
            {
                await doc.SetAsync(data1);
                Response.Write("<script>alert('Successfully Added a New Sale or Discount.');</script>");
            }
            else
            {
                Response.Write("<script>alert('Error!');</script>");
            }

            Response.Redirect("~/ShopOwner/MySalesAndDiscountsPage.aspx", false);
        }

        public async void getShopName()
        {
            if (!IsPostBack)
            {
                Query query = database.Collection("Users").WhereEqualTo("email", (string)Application.Get("usernameget"));
                QuerySnapshot snap = await query.GetSnapshotAsync();

                // Loop through the documents in the query snapshot
                foreach (DocumentSnapshot documentSnapshot in snap.Documents)
                {
                    // Retrieve the data from the document
                    string shopName = documentSnapshot.GetValue<string>("shopName");

                    shopNameTextbox.Text = shopName;
                }
            }
        }
    }
}