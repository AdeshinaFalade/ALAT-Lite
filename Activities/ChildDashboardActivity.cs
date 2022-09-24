using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.CardView.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALAT_Lite.Activities
{
    [Activity(Label = "ChildDashboardActivity")]
    public class ChildDashboardActivity : AppCompatActivity
    {
        CardView airtime, accountStatement, history;
        ImageView btnHome;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
          
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.child_dashboard);
            // Create your application here
            airtime = FindViewById<CardView>(Resource.Id.airtimePurchase);
            airtime.Click += Airtime_Click;
            accountStatement = FindViewById<CardView>(Resource.Id.accountStatement);
            accountStatement.Click += AccountStatement_Click;
            history = FindViewById<CardView>(Resource.Id.transactionHistory);
            btnHome = FindViewById<ImageView>(Resource.Id.homeButton);
            btnHome.Click += BtnHome_Click;
        }

        private void AccountStatement_Click(object sender, EventArgs e)
        {
            var myIntent = new Intent(this, typeof(AccountStatementActivity));
            StartActivityForResult(myIntent, 2);
            SetResult(Result.Ok, myIntent);
            Finish();
        }

        private void Airtime_Click(object sender, EventArgs e)
        {
            var myIntent = new Intent(this, typeof(ChildRechargeActivity));
            StartActivityForResult(myIntent, 1);
            SetResult(Result.Ok, myIntent);
            Finish();
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            //
        }
    }
}