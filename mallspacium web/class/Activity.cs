using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mallspacium_web
{
    [FirestoreData]
    public class Activity
    {
        [FirestoreProperty]
        public string id { get; set; }
        [FirestoreProperty]
        public string activity { get; set; }
        [FirestoreProperty]
        public string email { get; set; }
        [FirestoreProperty]
        public string userRole { get; set; }
        [FirestoreProperty]
        public string date { get; set; }
    }
}