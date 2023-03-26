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
            string datetime2 = DateTime.UtcNow.ToString("MM-dd-yyyy-HH-mm-ss");
            DocumentReference downtimeRef = database.Collection("AdminSystemDowntime").Document("DT: " + datetime2);

            Dictionary<string, object> downtimeData = new Dictionary<string, object>
{
            {"startTime", startDateTextbox.Text},
            {"endTime", endDateTextbox.Text},
            {"message", messageTextbox.Text}
};
            await downtimeRef.SetAsync(downtimeData);
           
            DocumentSnapshot snapshot = await downtimeRef.GetSnapshotAsync();



            Query usersQue = database.Collection("Users");
            QuerySnapshot snap = await usersQue.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap.Documents)
            {
                ManageUsers user = docsnap.ConvertTo<ManageUsers>();

                if (docsnap.Exists)
                {
                    // Specify the name of the document using a variable or a string literal
                    string documentName = "server down: " + datetime2;
                    DocumentReference downtimeRef1 = database.Collection("Users").Document(docsnap.Id).Collection("Notification").Document(documentName);

                    Dictionary<string, object> downtimeData1 = new Dictionary<string, object>
                        {
                            {"startTime", startDateTextbox.Text},
                            {"endTime", endDateTextbox.Text},
                            {"message", messageTextbox.Text}
                        };
                    await downtimeRef1.SetAsync(downtimeData1);

                    DocumentSnapshot snapshot1 = await downtimeRef1.GetSnapshotAsync();
                }
            }

            Response.Write("<script>alert('Successfully saved setting downtime');</script>");
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