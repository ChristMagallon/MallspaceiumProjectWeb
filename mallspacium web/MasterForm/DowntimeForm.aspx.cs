using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Cloud.Firestore;

namespace mallspacium_web
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        FirestoreDb database;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("mallspaceium");

        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            save();
        }

        public async void save()
        {
            DocumentReference downtimeRef = database.Collection("Users").Document();

            Dictionary<string, object> downtimeData = new Dictionary<string, object>
{
            {"startTime", DateTime.UtcNow},
            {"endTime", DateTime.UtcNow.AddDays(1)},
            {"message", messageTextbox.Text}
};
            await downtimeRef.SetAsync(downtimeData);
            Response.Write("<script>alert('Successfully saved setting downtime');</script>");

            DocumentSnapshot snapshot = await downtimeRef.GetSnapshotAsync();

            /*if (snapshot.Exists)
            {
                DateTime startTime = snapshot.GetValue<DateTime>("startTime");
                DateTime endTime = snapshot.GetValue<DateTime>("endTime");
                string message = snapshot.GetValue<string>("message");

                if (DateTime.UtcNow >= startTime && DateTime.UtcNow <= endTime)
                {
                    // Show downtime message to users.
                    // Display the message in a modal or overlay, or redirect the user to a dedicated downtime page.
                }
            }*/
        }
    }
}