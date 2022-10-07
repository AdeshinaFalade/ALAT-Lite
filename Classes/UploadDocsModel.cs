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
    public class UploadDocsModel
    {
        public string idCardUrl { get; set; }
        public string wardPassportUrl { get; set; }
        public string guardianPassportUrl { get; set; }
        public string birthCertUrl { get; set; }
        public int status { get; } = 0;


    }
}