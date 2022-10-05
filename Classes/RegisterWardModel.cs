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
        public string guardianId { get; set; }
        public string address { get; set; }

    }
}