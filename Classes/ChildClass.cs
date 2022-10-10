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
    public class ChildClass
    {
        public double account_Balance { get; set; }
        public string account_Number { get; set; }
        public string account_Name { get; set; }
        public int ward_Id { get; set; }
        public int guardianId { get; set; }
        public string activity { get; set; }
    }
}