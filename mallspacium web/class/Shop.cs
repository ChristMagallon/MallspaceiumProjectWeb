using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mallspacium_web
{
    [FirestoreData]
    public class Shop
    {
        [FirestoreProperty]
        public string shopName { get; set; }
        [FirestoreProperty]
        public string shopImage { get; set; }
        [FirestoreProperty]
        public string shopDescription { get; set; }
    }
}