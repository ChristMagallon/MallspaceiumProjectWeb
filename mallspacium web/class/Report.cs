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
        public string reportedUsername { get; set; }
        [FirestoreProperty]
        public string reportId { get; set; }
        [FirestoreProperty]
        public string reportedBy { get; set; }
        [FirestoreProperty]
        public string reportMessage { get; set; }
        [FirestoreProperty]
        public Timestamp reportDate { get; set; }
    }
}