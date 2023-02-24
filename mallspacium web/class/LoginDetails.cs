using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Google.Cloud.Firestore;

namespace mallspacium_web
{
    [FirestoreData]
    public class LoginDetails
    {
        [FirestoreProperty]
        public string adminUsername { get; set; }
        [FirestoreProperty]
        public string adminPassword { get; set; }
    }
}