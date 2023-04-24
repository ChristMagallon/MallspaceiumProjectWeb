using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Cloud.Firestore;

namespace mallspacium_web
{
    public class SubscriptionData
    {
        public string SubscriptionID { get; set; }
        public string SubscriptionType { get; set; }
        public string Price { get; set; }
        public string UserEmail { get; set; }
        public string UserRole { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
    }
}