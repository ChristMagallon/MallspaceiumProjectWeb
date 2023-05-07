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
        protected async void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("mallspaceium");

            checkNotificationSetting();
        }

        public async void checkNotificationSetting()
        {
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
                if (data.TryGetValue("userNotif", out object fieldValue) && fieldValue is bool userNotif)
                {
                    if (userNotif)
                    {
                        NotifButton.Text = "On";
                    }
                    else
                    {
                        NotifButton.Text = "Off";
                    }
                }
                else
                {
                    // Field does not exist or is not a boolean
                }
            }
            else
            {
                // Document does not exist
            }
        }

        public async void notificationsetting()
        {
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
                if (data.TryGetValue("userNotif", out object fieldValue) && fieldValue is bool userNotif)
                {
                    if (userNotif)
                    {
                        // Update the userNotif field to false
                        Dictionary<string, object> updates = new Dictionary<string, object>
                        {
                            { "userNotif", false }
                        };
                        await docRef.UpdateAsync(updates);

                        // Update the button text
                        NotifButton.Text = "Off";
                    }
                    else
                    {
                        // Update the userNotif field to true
                        Dictionary<string, object> updates = new Dictionary<string, object>
                        {
                            { "userNotif", true }
                        };
                        await docRef.UpdateAsync(updates);

                        // Update the button text
                        NotifButton.Text = "On";
                    }
                }
                else
                {
                    // Field does not exist or is not a boolean
                }
            }
            else
            {
                // Document does not exist
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            // Redirect to another page after a delay
            string url = "AboutUsPage.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirectScript", "setTimeout(function(){ window.location.href = '" + url + "'; }, 50);", true);

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

        protected void NotifButton_Click(object sender, EventArgs e)
        {
            notificationsetting();
        }
    }
}