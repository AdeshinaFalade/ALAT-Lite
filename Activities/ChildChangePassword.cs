using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.Chip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALAT_Lite.Activities
{
    [Activity(Label = "ChildChangePassword")]
    public class ChildChangePassword : AppCompatActivity
    {
        ImageView backArrow;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.change_password);
            // Create your application here
            backArrow = FindViewById<ImageView>(Resource.Id.back_arrow);
            backArrow.Click += BackArrow_Click;
        }
        private void BackArrow_Click(object sender, EventArgs e)
        {
            var myIntent = new Intent(this, typeof(ChildLoginActivity));
            StartActivityForResult(myIntent, 4000);
            SetResult(Result.Ok, myIntent);
            Finish();
        }
        public override void OnBackPressed()
        {
            return;
        }
    }
}