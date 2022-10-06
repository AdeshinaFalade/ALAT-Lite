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
using AndroidX.ViewPager2.Widget;

using static AndroidX.ViewPager2.Widget.ViewPager2;
using ALAT_Lite.Classes;
using ALAT_Lite.Adapters;
using Xamarin.Essentials;

namespace ALAT_Lite.Activities
{
    [Activity(Label = "RoyalKiddiesActivity")]
    public class RoyalKiddiesActivity : AppCompatActivity
    {
        Toolbar toolbar;
        AppCompatButton btnFundWard;
        public static string AcctNum, KidName;
        ViewPager2 viewPager2;
        LinearLayout parent_view;
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
            viewPager2 = FindViewById<ViewPager2>(Resource.Id.viewPager);
            parent_view = FindViewById<LinearLayout>(Resource.Id.parent_view);

            //setup toolbar

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Royal Kiddies Account";
            ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.black_arrow);
            actionBar.SetDisplayHomeAsUpEnabled(true);

            btnFundWard.Click += BtnFundWard_Click;
            btnMaintenance.Click += BtnMaintenance_Click;
            btnTransactionHistory.Click += BtnTransactionHistory_Click;

            //setup view pager
            var myAdapter = new ViewPagerAdapter(this,CreateData());
            viewPager2.Adapter = myAdapter;
            viewPager2.RegisterOnPageChangeCallback(new MyOnPageCangeListener(parent_view));

           

        }
        

        public static List<ChildClass> CreateData()
        {
            List<ChildClass> children = new List<ChildClass>();
            children.Add(new ChildClass() { Balance = 5000, AccountNumber = "2323243422", Active = true ,Name = "Thor Odinson"});
            children.Add(new ChildClass() { Balance = 4000, AccountNumber = "2343648635", Active = true , Name = "Peter Parker"});
            children.Add(new ChildClass() { Balance = 400000, AccountNumber = "2343565896", Active = true, Name = "Bruce Banner" });
            return children;

        }





        private void BtnTransactionHistory_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(WardTransActivity));
            StartActivity(intent);
        }

        private void BtnMaintenance_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MaintenanceActivity));
            StartActivity(intent);
        }


        //transfer info about the selected account in the viewpager
        private void BtnFundWard_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(TransferActivity));
            intent.PutExtra("acct", AcctNum);
            intent.PutExtra("name", KidName);
            StartActivity(intent);
        }

        //back button
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

        private class MyOnPageCangeListener : OnPageChangeCallback
        {
            private LinearLayout parent_view;

            public MyOnPageCangeListener(LinearLayout parent_view)
            {
                this.parent_view = parent_view;
            }

            public override void OnPageSelected(int position)
            {
                //to note the item being displayed in the viewpager
                
                base.OnPageSelected(position);
                AcctNum = CreateData()[position].AccountNumber;
                KidName = CreateData()[position].Name;

               // Preferences.Set("acct", AcctNum);
            }
        }
    }
}