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
        public string subscriptionId { get; set; }
        [FirestoreProperty]
        public string subscriptionType { get; set; }
        [FirestoreProperty]
        public string username { get; set; }
        [FirestoreProperty]
        public float price { get; set; }
        [FirestoreProperty]
        public Timestamp startDate { get; set; }
        [FirestoreProperty]
        public Timestamp endDate { get; set; }
        [FirestoreProperty]
        public string status { get; set; }
    }
}