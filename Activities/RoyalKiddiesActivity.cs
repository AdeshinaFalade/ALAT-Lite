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
using ALAT_Lite.Fragments;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ALAT_Lite.Activities
{
    [Activity(Label = "RoyalKiddiesActivity")]
    public class RoyalKiddiesActivity : AppCompatActivity
    {
        public ProgressFragment progressDialog;
        Toolbar toolbar;
        AppCompatButton btnFundWard;
        public static string AcctNum, KidName, token;
        ViewPager2 viewPager2;
        public static List<ChildClass> listOfChildren = new List<ChildClass>();
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

            token = Preferences.Get("token", "");
            btnFundWard.Click += BtnFundWard_Click;
            btnMaintenance.Click += BtnMaintenance_Click;
            btnTransactionHistory.Click += BtnTransactionHistory_Click;

            FetchWards();

            
            viewPager2.RegisterOnPageChangeCallback(new MyOnPageCangeListener(parent_view));

            

        }
        public static List<ChildClass> CreateData()
        {
            List<ChildClass> children = new List<ChildClass>();
            children.Add(new ChildClass() { account_Balance = 5000, account_Number = "2323243422", guardianId = 2 ,account_Name = "Thor Odinson", ward_Id = 3});
            children.Add(new ChildClass() { account_Balance = 3000, account_Number = "2323243455", guardianId = 2, account_Name = "James Bond", ward_Id = 1 });
            children.Add(new ChildClass() { account_Balance = 8000, account_Number = "2323232323", guardianId = 2, account_Name = "John Jones", ward_Id = 2 });
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


        public async void FetchWards()
        {
            string result = string.Empty;
            try
            {
                ShowProgressDialog("Loading");
                result = await NetworkUtils.GetUserData("Guardian/getWardByGuardian?id=2", token);
                if (!string.IsNullOrEmpty(result))
                {
                    var resultObject = JObject.Parse(result);
                    listOfChildren = JsonConvert.DeserializeObject<List<ChildClass>>(resultObject["value"].ToString());
                    CloseProgressDialog();

                    //setup view pager
                    var myAdapter = new ViewPagerAdapter(this, listOfChildren);
                    viewPager2.Adapter = myAdapter;
                }
                else
                {
                    CloseProgressDialog();
                    Toast.MakeText(this, "Oops! an error occured, Kindly try again.", ToastLength.Short).Show();
                }
            }
            catch (Exception e)
            {
                CloseProgressDialog();
                Toast.MakeText(this, "Oops! an error occured, Kindly try again.", ToastLength.Short).Show();
            }


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
                AcctNum = listOfChildren[position].account_Number;
                KidName = listOfChildren[position].account_Name;

               // Preferences.Set("acct", AcctNum);
            }
        }

        public void ShowProgressDialog(string status)
        {
            progressDialog = new ProgressFragment(status);
            var trans = FragmentManager.BeginTransaction();
            progressDialog.Cancelable = false;
            progressDialog.Show(trans, "progress");

        }

        public void CloseProgressDialog()
        {
            if (progressDialog != null)
            {
                progressDialog.Dismiss();
                progressDialog = null;
            }
        }
    }
}