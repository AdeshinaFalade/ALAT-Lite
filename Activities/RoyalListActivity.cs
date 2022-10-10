using ALAT_Lite.Adapters;
using ALAT_Lite.Classes;
using ALAT_Lite.Fragments;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using Google.Android.Material.Snackbar;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using ActionBar = AndroidX.AppCompat.App.ActionBar;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace ALAT_Lite.Activities
{
    [Activity(Label = "RoyalListActivity")]
    public class RoyalListActivity : AppCompatActivity
    {
        Toolbar toolbar;
        public ProgressFragment progressDialog;
        public static string AcctNum, KidName, token;
        public static int wardId, userId;
        public static List<ChildClass> listOfChildren = new List<ChildClass>();
        RecyclerView recyclerView;
        WardRecyclerAdapter recyclerAdapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.RoyalAcctList);
            toolbar = FindViewById<Toolbar>(Resource.Id.wardlistToolbar);
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView2);

            //setup toolbar

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Royal Kiddies";
            ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.black_arrow);
            actionBar.SetDisplayHomeAsUpEnabled(true);

            userId = Preferences.Get("userId", 0);
            token = Preferences.Get("token", "");
            FetchWards(userId);
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

        void SetupRecyclerView()
        {
            recyclerView.SetLayoutManager(new LinearLayoutManager(this));
            recyclerAdapter = new WardRecyclerAdapter(this,listOfChildren);
            recyclerView.SetAdapter(recyclerAdapter);
            recyclerAdapter.ItemClick += RecyclerAdapter_ItemClick; 
        }

        private void RecyclerAdapter_ItemClick(object sender, WardrecyclerAdapterClickEventArgs e)
        {
            Preferences.Set("child", e.Position);
            Intent intent = new Intent(this, typeof(RoyalKiddiesActivity));
            StartActivity(intent);
        }
         
        public async void FetchWards(int id)
        {
            string result = string.Empty;
            try
            {
                ShowProgressDialog("Loading");
                result = await NetworkUtils.GetUserData($"Guardian/getWardByGuardian?id={id}", token);
                if (!string.IsNullOrEmpty(result) && result != "Unauthorized")
                {
                    var resultObject = JObject.Parse(result);
                    listOfChildren = JsonConvert.DeserializeObject<List<ChildClass>>(resultObject["value"].ToString());
                    CloseProgressDialog();
                    SetupRecyclerView();



                }
                else if (result == "Unauthorized")
                {
                    CloseProgressDialog();
                    Toast.MakeText(this, "Your session has expired, Kindly log in again or refresh in the home page", ToastLength.Short).Show();
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