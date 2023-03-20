using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
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
                field = data["userNotif"];
                // Do something with the field value
            }
            else
            {
                // Document does not exist
            }

            notif = field.ToString();
            Button1.Text = notif;
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
            Response.Write("<script>alert('Mallspaceium is an application that will help shoppers find and locate their desired stores from their current location. It also allows admins, including store owners, to have independent access to the application to have business promotions and notify users. The application would improve the shopping experience by making it quicker, faster, and more convenient for customers to locate stores and products of interest, and improve safety management by allowing consumers to evacuate a mall more swiftly in the event of an emergency or fire.');</script>");

        }
    }
}
