using FireSharp.Extensions;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Google.Cloud.Language.V1.PartOfSpeech.Types;
namespace mallspacium_web.ShopOwner
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
                catch(Exception)
                {
                    choice = true;
                    createNotif();
                    notif = "true";
                }
             
                // Do something with the field value
            }
            else
            {
                // Document does not exist
            }

            if(choice == false)
            {
                notif = field.ToString();
                Button1.Text = notif;

            }
         
        }

        public async void createNotif(){

            DocumentReference usersRef2 = db.Collection("Users").Document((string)Application.Get("usernameget"));
            Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {"userNotif","true" }
                };
            DocumentSnapshot snap = await usersRef2.GetSnapshotAsync();
            if (snap.Exists)
            {
                await usersRef2.UpdateAsync(data);
            }
            Button1.Text = "true";

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            notificationsetting();
        }

        public async void notificationsetting()
        {
            if (notif == "true")
            {
                DocumentReference usersRef2 = db.Collection("Users").Document((string)Application.Get("usernameget"));
                Dictionary<string, object> data = new Dictionary<string, object>()
                    {
                        {"userNotif","false" }
                    };
                DocumentSnapshot snap = await usersRef2.GetSnapshotAsync();
                if (snap.Exists)
                {
                    await usersRef2.UpdateAsync(data);
                }
                Button1.Text = "false";
            }

            if (notif == "false")
            {
                DocumentReference usersRef2 = db.Collection("Users").Document((string)Application.Get("usernameget"));
                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {"userNotif","true" }
                };
                DocumentSnapshot snap = await usersRef2.GetSnapshotAsync();
                if (snap.Exists)
                {
                    await usersRef2.UpdateAsync(data);
                }
                Button1.Text = "true";
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            // Redirect to another page after a delay
            string url = "AboutUsPage.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 50);", true);

        }

        protected void Button2_Click1(object sender, EventArgs e)
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
    }
}
