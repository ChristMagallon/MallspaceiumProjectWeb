using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.Shopper
{
    public partial class ReportShopPage : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            if (!IsPostBack)
            {
                string shopName = Request.QueryString["shopName"];
                shopNameLabel.Text = shopName;
            }
        }

        protected void reportButton_Click(object sender, EventArgs e)
        {
            report();
        }

        public async void report()
        {
            string shopName = Request.QueryString["shopName"];
            string email = Request.QueryString["email"];

            string defaultStatus = "Pending";

            //auto generated unique id
            Random random = new Random();
            int randomIDNumber = random.Next(100000, 999999);
            string reportID = "REP" + randomIDNumber.ToString();

            //Get current date time and the expected expiration date
            DateTime currentDate = DateTime.Now;
            string date = currentDate.ToString("yyyy-MM-dd HH:mm:ss");

            //Create an instance of Bitmap from the uploaded file using the FileUpload control
            Bitmap image = new Bitmap(imageFileUpload.PostedFile.InputStream);
            MemoryStream stream = new MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);

            byte[] bytes = stream.ToArray();

            //Convert the Bitmap image to a Base64 string
            string base64String = Convert.ToBase64String(bytes);

            // Update the file in Firestore
            DocumentReference userRef = database.Collection("AdminReport").Document(reportID);
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                { "id", reportID },
                { "shopName", shopName },
                { "reason", reasonDropDownList.SelectedItem.Text},
                { "detailedReason", reasonTextbox.Text },
                { "supportingImage", base64String },
                { "reportedBy", (string)Application.Get("usernameget")},
                { "date", date },
                { "status", defaultStatus},
                { "email", email }
            };
            await userRef.SetAsync(data1);

            // Display a message
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('Successfully Report Shop!');", true);

            // Redirect to another page after a delay
            string url = "PopularShopsPage.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 500);", true);
        }
    }
}