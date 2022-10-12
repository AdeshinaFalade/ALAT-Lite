using ALAT_Lite.Classes;
using ALAT_Lite.Fragments;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
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
    [Activity(Label = "SetLimitActivity")]
    public class SetLimitActivity : AppCompatActivity
    {
        Toolbar toolbar;
        public static int wardId;
        public static string token;
        EditText edtLimitAmount;
        SetLimitAlertFrag setLimitAlertFrag;
        public ProgressFragment progressDialog;
        AppCompatButton btnSetLimit;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SetLimitLayout);
            toolbar = FindViewById<Toolbar>(Resource.Id.limitToolbar);
            btnSetLimit = FindViewById<AppCompatButton>(Resource.Id.btnSetLimit);
            edtLimitAmount = FindViewById<EditText>(Resource.Id.edtLimitAmount);

            wardId = Preferences.Get("wardId", 0);
            token = Preferences.Get("token", "");

            //setup toolbar

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Set Limit";
            ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.black_arrow);
            actionBar.SetDisplayHomeAsUpEnabled(true);

            //connect
            btnSetLimit.Click += BtnSetLimit_Click; 
        }

        private void BtnSetLimit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(edtLimitAmount.Text))
            {
                Toast.MakeText(this, "Amount is required", ToastLength.Short).Show();
                return;
            }
            else if (int.Parse(edtLimitAmount.Text) < 1000 | int.Parse(edtLimitAmount.Text) > 50000)
            {
                Toast.MakeText(this, "Amount can only be between N1,000 and N50,000", ToastLength.Short).Show();
                return;
            }
            var amt = double.Parse(edtLimitAmount.Text);
            SetLimit(amt, wardId);
        }

        void ShowAlert()
        {

            setLimitAlertFrag = new SetLimitAlertFrag();
            var trans = FragmentManager.BeginTransaction();
            setLimitAlertFrag.Show(trans, "Dialog");
        }

        public async void SetLimit(double amount, int id)
        {
            string result = string.Empty;
            try
            {
                ShowProgressDialog("Updating");
                result = await NetworkUtils.PostData($"Guardian/UpdateLimit?wardId={id}&amount={amount}", token);
                var resultObject = JObject.Parse(result);
                if (!string.IsNullOrEmpty(result) && resultObject["statusCode"].ToString() == "200")
                {
                    CloseProgressDialog();
                    ShowAlert();
                }
                else if (result == "Unauthorized")
                {
                    CloseProgressDialog();
                    Toast.MakeText(this, "Your session has expired", ToastLength.Short).Show();
                    Intent intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                    Finish();
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