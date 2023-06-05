using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mallspacium_web
{
    [FirestoreData]
    public class SuperAdminAccountClass
    {
        [FirestoreProperty]
        public string superAdminId { get; set; }
        [FirestoreProperty]
        public string superAdminEmail { get; set; }
        [FirestoreProperty]
        public string superAdminDateCreated { get; set; }
    }
}