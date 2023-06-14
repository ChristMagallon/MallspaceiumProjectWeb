using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace mallspacium_web
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web;

    public class FcmNotification
    {
        public void SendNotification(string deviceToken, string title, string message)
        {
            // Replace with your FCM server key
            string serverKey = "AAAA_ML7o9A:APA91bEzyQ93NhjjxzRz_U1M_Em1YI_BZhFjSj21GYZHvhF2Rh1ybZAOlwCHB4yiGyioC7nL4KWyM7GKTpd6snRuV31COCpBLAKniQ4CY3wEePoEtAe-u1jBf9FSbsPWdsQvHcmM-Ilq";

            string firebaseUrl = "https://fcm.googleapis.com/fcm/send";

            var request = (HttpWebRequest)WebRequest.Create(firebaseUrl);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add(string.Format("Authorization: key={0}", serverKey));

            var payload = new
            {
                to = deviceToken,
                notification = new
                {
                    title = title,
                    body = message,
                    sound = "default",
                    click_action = "OPEN_LOGIN_ACTIVITY" // Modify to fit your app's behavior
                }
            };

            string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

            byte[] byteArray = Encoding.UTF8.GetBytes(jsonPayload);
            request.ContentLength = byteArray.Length;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream dataStreamResponse = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(dataStreamResponse))
                        {
                            string responseFromServer = reader.ReadToEnd();
                            // Handle the response if needed
                        }
                    }
                }
            }
        }
    }

}