using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mallspacium_web
{
    [FirestoreData]
    public class Product
    {
        [FirestoreProperty]
        public string prodName { get; set; }
        [FirestoreProperty]
        public string prodImage { get; set; }
        [FirestoreProperty]
        public string prodDesc { get; set; }
        [FirestoreProperty]
        public string prodPrice { get; set; }
        [FirestoreProperty]
        public string prodTag{ get; set; }
        [FirestoreProperty]
        public string prodShopName { get; set; }
    }
}