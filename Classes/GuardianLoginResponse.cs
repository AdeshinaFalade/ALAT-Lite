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
    public class GuardianLoginResponse
    {
        public int userId { get; set; }
        public string token { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string accountBalance { get; set; }
        public bool accountstatus { get; set; }
        public string accountNumber { get; set; }
        public string accountType { get; set; }
        public string bvn { get; set; }
        public string role { get; set; }
    }
}