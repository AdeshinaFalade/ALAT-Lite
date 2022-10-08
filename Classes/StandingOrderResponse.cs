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
    public class StandingOrderResponse
    {
        public int id { get; set; }
        public int wardId { get; set; }
        public double amount { get; set; }
        public int dayOfTrans { get; set; }
        public string guardianAccounNumber { get; set; }
        public string wardAccountNumber { get; set; }
        public string guardianAccount { get; set; }
        public bool isActivated { get; set; }
    }
}