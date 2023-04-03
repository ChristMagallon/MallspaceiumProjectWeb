using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web.MasterForm
{
    public partial class ReportDetailsForm : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

            if (!IsPostBack)
            {
                retrieveReportDetails();
            }
        }

        public async void retrieveReportDetails()
        {
            if (!IsPostBack)
            {
                // Retrieve the shop name from the query string
                string id = Request.QueryString["id"];

                // Use the shop name to retrieve the data from Firestore
                Query query = database.Collection("AdminReport").WhereEqualTo("id", id);
                QuerySnapshot snapshot = await query.GetSnapshotAsync();

                // Loop through the documents in the query snapshot
                foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                {
                    // Retrieve the data from the document
                    //string shopName = documentSnapshot.GetValue<string>("shopImage");
                    string shopName = documentSnapshot.GetValue<string>("shopName");
                    string reason = documentSnapshot.GetValue<string>("reason");
                    string detailedReason = documentSnapshot.GetValue<string>("detailedReason");
                    string reportedBy = documentSnapshot.GetValue<string>("reportedBy");
                    string date = documentSnapshot.GetValue<string>("date");

                    // Display the data
                    idLabel.Text = id;
                    shopNameLabel.Text = shopName;
                    reasonLabel.Text = reason;
                    detailedReasonLabel.Text = detailedReason;
                    reportedByLabel.Text = reportedBy;
                    dateLabel.Text = date;
                }
            }
        }

        protected void viewSupportingImageButton_Click(object sender, EventArgs e)
        {
            viewSupportingImage();
        }

        public async void viewSupportingImage()
        {
            // Retrieve the shop name from the query string
            string id = Request.QueryString["id"];

            // Retrieve the data from Firestore database
            DocumentReference docRef = database.Collection("AdminReport").Document(id);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            string imageString = snapshot.GetValue<string>("supportingImage");

            // Show the modal with the image
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "showModal", "showModal('" + imageString + "');", true);
        }

        protected void addProofNoteButton_Click(object sender, EventArgs e)
        {
            addProofNote();
        }

        public async void addProofNote()
        {
            //auto generated unique id
            Random random = new Random();
            int randomIDNumber = random.Next(100000, 999999);
            string noteID = "NOTE" + randomIDNumber.ToString();

            //Get current date time and the expected expiration date
            DateTime currentDate = DateTime.Now;
            string date = currentDate.ToString("yyyy-MM-dd HH:mm:ss");

            //Create an instance of Bitmap from the uploaded file using the FileUpload control
            Bitmap image = new Bitmap(proofFileUpload.PostedFile.InputStream);
            MemoryStream stream = new MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] bytes = stream.ToArray();

            //Convert the Bitmap image to a Base64 string
            string base64String = Convert.ToBase64String(bytes);

            DocumentReference userRef = database.Collection("AdminReport").Document(idLabel.Text).Collection("ReportStatus").Document(noteID);
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                { "id", noteID },
                { "message", noteTextBox.Text },
                { "date", date },
                { "proofImage", base64String}
            };

            await userRef.SetAsync(data1);

            // Display a message
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('Successfully Added Proof and Note!');", true);

            // Redirect to another page after a delay
            string url = "ReportsForm.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 500);", true);
        }
    }
}