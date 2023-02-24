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
        public string activityId { get; set; }
        [FirestoreProperty]
        public string activity { get; set; }
        [FirestoreProperty]
        public string username { get; set; }
        [FirestoreProperty]
        public Timestamp date { get; set; }
    }
}