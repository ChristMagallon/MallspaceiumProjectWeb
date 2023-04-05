using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mallspacium_web
{
    [FirestoreData]
    public class AdminAccount
    {
        [FirestoreProperty]
        public string adminId { get; set; }
        [FirestoreProperty]
        public string adminEmail { get; set; }
        [FirestoreProperty]
        public string adminDateCreated { get; set; }
    }
}