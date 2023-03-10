
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Google.Cloud.Firestore;
using Google.Type;

namespace mallspacium_web
{
    [FirestoreData]
    public class ManageUsers
    {
        [FirestoreProperty]
        public string username { get; set; }
        [FirestoreProperty]
        public string id { get; set; }
        [FirestoreProperty]
        public string accountType { get; set; }
        [FirestoreProperty]
        public Timestamp dateCreated { get; set; }
        [FirestoreProperty]
        public string email { get; set; }
        [FirestoreProperty]
        public string address { get; set; }
        [FirestoreProperty]
        public Int64 contactNumber { get; set; }
    }
}