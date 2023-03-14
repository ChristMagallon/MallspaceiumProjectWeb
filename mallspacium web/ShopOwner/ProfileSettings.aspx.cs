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
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"mallspaceium.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("mallspaceium");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            notificationsetting();
        }
        public async void notificationsetting()
        {
            DocumentReference usersRef = db.Collection("Users").Document((string)Application.Get("usernameget"));
            Dictionary<string, object> data = new Dictionary<string, object>()
                    {
                        {"userNotif","false" }
                    };
            DocumentSnapshot snap = await usersRef.GetSnapshotAsync();
            if(snap.Exists)
            {
                await usersRef.UpdateAsync(data);
            }
 
            Button1.Text = "false";
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Write("<script>alert('Mallspaceium is an application that will help shoppers find and locate their desired stores from their current location. It also allows admins, including store owners, to have independent access to the application to have business promotions and notify users. The application would improve the shopping experience by making it quicker, faster, and more convenient for customers to locate stores and products of interest, and improve safety management by allowing consumers to evacuate a mall more swiftly in the event of an emergency or fire.');</script>");
        }
    }
}