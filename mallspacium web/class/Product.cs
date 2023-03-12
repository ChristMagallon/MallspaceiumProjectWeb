using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private static byte[] ConvertStringToByteArray(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            return Encoding.UTF8.GetBytes(str);
        }

        [FirestoreProperty]
        // Read-only property that returns ProdImage as a byte array
        public byte[] ProdImageBytes
        {
            get
            {
                if (string.IsNullOrEmpty(prodImage))
                {
                    return null;
                }

                var match = Regex.Match(prodImage, @"^[\da-fA-F]+$");
                if (!match.Success || match.Length % 2 != 0)
                {
                    throw new InvalidOperationException("Invalid string format.");
                }

                return ConvertStringToByteArray(prodImage);
            }
        }
    }
}