using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using ActionBar = AndroidX.AppCompat.App.ActionBar;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ALAT_Lite.Adapters;
using ALAT_Lite.Classes;
using Android.Support.V7.Widget;
using ALAT_Lite.Fragments;
using Xamarin.Essentials;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ALAT_Lite.Activities
{
    [Activity(Label = "WardTrans")]
    public class WardTransActivity : AppCompatActivity
    {
        public ProgressFragment progressDialog;
        public static string token;
        public static int wardId, userId;
        Toolbar toolbar;
        RecyclerView recyclerView;
        RecyclerAdapter recyclerAdapter;
        List<TransactionModel> listOfTrans = new List<TransactionModel>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.WardTransactionHistory);
            toolbar = FindViewById<Toolbar>(Resource.Id.wardTransToolbar);
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView1);

            userId = Preferences.Get("userId", 0);
            wardId = Preferences.Get("wardId", 0);
            token = Preferences.Get("token", "");

            //setup toolbar

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Transaction History";
            ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.black_arrow);
            actionBar.SetDisplayHomeAsUpEnabled(true);

            FetchWardHistory(wardId);

        }

        void SetupRecyclerView()
        {
            recyclerView.SetLayoutManager(new LinearLayoutManager(this));
            recyclerAdapter = new RecyclerAdapter(listOfTrans);
            recyclerView.SetAdapter(recyclerAdapter);
        }

        public async void FetchWardHistory(int id)
        {
            string result = string.Empty;
            try
            {
                ShowProgressDialog("Loading");
                result = await NetworkUtils.GetUserData($"Guardian/WardTransactions?id={id}", token);
                if (!string.IsNullOrEmpty(result) && result != "Unauthorized")
                {
                    listOfTrans = JsonConvert.DeserializeObject<List<TransactionModel>>(result);
                    CloseProgressDialog();

                    SetupRecyclerView();
                }
                else if (result == "Unauthorized")
                {
                    CloseProgressDialog();
                    Toast.MakeText(this, "Your session has expired, Kindly log in again or refresh in the home page.", ToastLength.Short).Show();
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