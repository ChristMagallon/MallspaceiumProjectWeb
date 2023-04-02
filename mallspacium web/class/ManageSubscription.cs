using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mallspacium_web
{
    [FirestoreData]

    public class ManageSubscription
    {
        [FirestoreProperty]
        public string subscriptionID { get; set; }
        [FirestoreProperty]
        public string userEmail { get; set; }
        [FirestoreProperty]
        public string userRole { get; set; }
        [FirestoreProperty]
        public string subscriptionType { get; set; }
        [FirestoreProperty]
        public string price { get; set; }
        [FirestoreProperty]
        public string startDate { get; set; }
        [FirestoreProperty]
        public string endDate { get; set; }
        [FirestoreProperty]
        public string status { get; set; }
    }
}