using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mallspacium_web
{
    [FirestoreData]
    public class Report
    {
        [FirestoreProperty]
        public string id { get; set; }
        [FirestoreProperty]
        public string shopName { get; set; }
        [FirestoreProperty]
        public string reason { get; set; }
        [FirestoreProperty]
        public string detailedReason { get; set; }
        [FirestoreProperty]
        public string supportingImage { get; set; }
        [FirestoreProperty]
        public string reportedBy { get; set; }
        [FirestoreProperty]
        public string date { get; set; }
        [FirestoreProperty]
        public string status { get; set; }
    }
}