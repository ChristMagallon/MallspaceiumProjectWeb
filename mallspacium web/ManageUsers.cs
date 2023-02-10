
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
        public string manageUser { get; set; }
        [FirestoreProperty]
        public string manageId { get; set; }
        [FirestoreProperty]
        public string manageRole { get; set; }
        [FirestoreProperty]
        public Timestamp manageDate { get; set; }
        [FirestoreProperty]
        public string manageStatus { get; set; }
    }
}