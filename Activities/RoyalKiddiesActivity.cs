using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using ActionBar = AndroidX.AppCompat.App.ActionBar;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;
using System.Text;
using AndroidX.AppCompat.Widget;

namespace ALAT_Lite.Activities
{
    [Activity(Label = "RoyalKiddiesActivity")]
    public class RoyalKiddiesActivity : AppCompatActivity
    {
        Toolbar toolbar;
        AppCompatButton btnFundWard;
        AppCompatButton btnTransactionHistory;
        AppCompatButton btnMaintenance;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ward_dashboard);
            toolbar = FindViewById<Toolbar>(Resource.Id.royakkiddiesToolbar);
            btnFundWard = FindViewById<AppCompatButton>(Resource.Id.btnFundWard);
            btnTransactionHistory = FindViewById<AppCompatButton>(Resource.Id.btnWardTransactionHistory);
            btnMaintenance = FindViewById<AppCompatButton>(Resource.Id.btnMaintenance);

            //setup toolbar

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Royal Kiddies Account";
            ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.black_arrow);
            actionBar.SetDisplayHomeAsUpEnabled(true);

            btnFundWard.Click += BtnFundWard_Click;
            btnMaintenance.Click += BtnMaintenance_Click;
        }

        private void BtnMaintenance_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MaintenanceActivity));
            StartActivity(intent);
        }

        private void BtnFundWard_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(TransferActivity));
            StartActivity(intent);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    OnBackPressed();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }

        }
    }
}