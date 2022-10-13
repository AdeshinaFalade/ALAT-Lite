using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALAT_Lite.Classes
{
    public class RegisterWardModel
    {
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string dateOfBirth { get; set; }
        public string emailAddress { get; set; }
        public string phoneNumber { get; set; }
        public string gender { get; set; }
        public int guardianId { get; set; }
        public string address { get; set; }
        public string idUrl { get; set; }
        public string wardPassportUrl { get; set; }
        public string guardianPassportUrl { get; set; }
        public string birthCertUrl { get; set; }
        public bool isDownload { get; set; }

    }
}