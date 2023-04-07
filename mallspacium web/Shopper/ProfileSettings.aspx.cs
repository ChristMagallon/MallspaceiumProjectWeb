using FireSharp.Extensions;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Google.Cloud.Language.V1.PartOfSpeech.Types;

namespace mallspacium_web.Shopper
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        FirestoreDb db;
        static string notif = "";
        protected async void Page_Load(object sender, EventArgs e)
        {
            bool choice = false;
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("mallspaceium");

            object field = "";

            // Query the Firestore collection for a user with a specific email address
            CollectionReference usersRef = db.Collection("Users");
            DocumentReference docRef = usersRef.Document((string)Application.Get("usernameget"));

            // Retrieve the document data asynchronously
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            // Check if the document exists
            if (snapshot.Exists)
            {
                // Get the data as a Dictionary
                Dictionary<string, object> data = snapshot.ToDictionary();
                // Access the specific field you want
                try
                {
                    field = data["userNotif"];
                }
                catch (Exception)
                {
                    choice = true;
                    createNotif();
                    notif = "On";
                }
                // Do something with the field value
            }
            else
            {
                // Document does not exist
            }

            if (choice == false)
            {
                notif = field.ToString();
                Button1.Text = notif;

            }

            if (!IsPostBack)
            {
                // Check if the cookie exists
                if (Request.Cookies["Language"] != null)
                {
                    // Set the selected value of the drop-down list to the cookie value
                    ddlLanguage.SelectedValue = Request.Cookies["Language"].Value;
                }
                else
                {
                    // Set the default language to English
                    ddlLanguage.SelectedValue = "en-US";
                }
            }
        }

        public async void createNotif()
        {

            DocumentReference usersRef2 = db.Collection("Users").Document((string)Application.Get("usernameget"));
            Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {"userNotif","On" }
                };
            DocumentSnapshot snap = await usersRef2.GetSnapshotAsync();
            if (snap.Exists)
            {
                await usersRef2.UpdateAsync(data);
            }
            Button1.Text = "On";

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            notificationsetting();
        }
        public async void notificationsetting()
        {
            if (notif == "On")
            {
                DocumentReference usersRef2 = db.Collection("Users").Document((string)Application.Get("usernameget"));
                Dictionary<string, object> data = new Dictionary<string, object>()
                    {
                        {"userNotif","Off" }
                    };
                DocumentSnapshot snap = await usersRef2.GetSnapshotAsync();
                if (snap.Exists)
                {
                    await usersRef2.UpdateAsync(data);
                }

                Button1.Text = "Off";
            }

            if (notif == "Off")
            {
                DocumentReference usersRef2 = db.Collection("Users").Document((string)Application.Get("usernameget"));
                Dictionary<string, object> data = new Dictionary<string, object>()
                    {
                        {"userNotif","On" }
                    };
                DocumentSnapshot snap = await usersRef2.GetSnapshotAsync();
                if (snap.Exists)
                {
                    await usersRef2.UpdateAsync(data);
                }

                Button1.Text = "On";
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Write("<script>alert('Mallspaceium is an application that will help shoppers find and locate their desired stores from their current location. It also allows admins, including store owners, to have independent access to the application to have business promotions and notify users. The application would improve the shopping experience by making it quicker, faster, and more convenient for customers to locate stores and products of interest, and improve safety management by allowing consumers to evacuate a mall more swiftly in the event of an emergency or fire.');</script>");

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            downloadData();
        }

        public async void downloadData()
        {
            // Define collection and document reference
            CollectionReference usersCollectionRef = db.Collection("Users");
            DocumentReference userDocRef = usersCollectionRef.Document((string)Application.Get("usernameget"));

            // Get user data as dictionary
            DocumentSnapshot userSnapshot = await userDocRef.GetSnapshotAsync();
            Dictionary<string, object> userData = userSnapshot.ToDictionary();

            // Create file stream and writer
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string downloadsPath = Path.Combine(folderPath, "Downloads");
            string fileName = "My-User-Details.txt";
            string filePath = Path.Combine(downloadsPath, fileName);
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            StreamWriter writer = new StreamWriter(fileStream);

            // Write user data to file
            foreach (KeyValuePair<string, object> entry in userData)
            {
                string line = $"{entry.Key}: {entry.Value}";
                writer.WriteLine(line);
            }

            // Close writer and stream
            writer.Close();
            fileStream.Close();
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set the language cookie to the selected value
            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = ddlLanguage.SelectedValue;
            Response.Cookies.Add(cookie);

            // Redirect to the same page to apply the language change
            Response.Redirect(Request.RawUrl, false);
        }
    }
}