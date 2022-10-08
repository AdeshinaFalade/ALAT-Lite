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
        public double amount { get; set; }
        public string phone_Number { get; set; }
        public string trx_Date { get; set; }
        public string trx_Network { get; set; }
        public int id { get; set; }
        public string trx_Description { get; set; }

    }
}