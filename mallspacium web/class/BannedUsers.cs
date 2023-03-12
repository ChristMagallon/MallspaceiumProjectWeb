using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Cloud.Firestore;

namespace mallspacium_web
{
    [FirestoreData]
    public class BannedUsers
    {
        [FirestoreProperty]
        public string email { get; set; }
    }
}