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
    public class TransactionModel
    {
        public int Amount { get; set; }
        public string Phone { get; set; }
        public string Date { get; set; }

    }
}