using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using ALAT_Lite.Fragments;
using System.Text;
using ActionBar = AndroidX.AppCompat.App.ActionBar;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;
using Xamarin.Essentials;
using ALAT_Lite.Classes;
using Newtonsoft.Json.Linq;

namespace ALAT_Lite.Activities
{
    [Activity(Label = "RestrictAccountActivity")]
    public class RestrictAccountActivity : AppCompatActivity
    {
        Spinner spinner;
        AppCompatButton btnApply;
        Toolbar toolbar;
        RestrictAlertFrag alertFrag;
        public ProgressFragment progressDialog;
        public static int wardId;
        public static string token;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.RestrictAccountLayout);
            toolbar = FindViewById<Toolbar>(Resource.Id.restrictToolbar);
            spinner = FindViewById<Spinner>(Resource.Id.spinner2);
            btnApply = FindViewById<AppCompatButton>(Resource.Id.btnApply);

            wardId = Preferences.Get("wardId", 0);
            token = Preferences.Get("token", "");

            //setup toolbar

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Restrict Account";
            ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.black_arrow);
            actionBar.SetDisplayHomeAsUpEnabled(true);


            //spinner setup
            ArrayAdapter adapter;
            adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.Choose, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            btnApply.Click += BtnApply_Click;   
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {

            var spinnerOption = spinner.SelectedItem.ToString();
            var status = "";
            if (spinnerOption == "Activate")
            {
                status = "false";
            }
            else
                status = "true";
            Restrict(wardId, status);
        }

        public async void Restrict(int id, string status)
        {
            string result = string.Empty;
            try
            {
                ShowProgressDialog("Updating");
                result = await NetworkUtils.PostData($"Guardian/UpdateRestrictionStatus?wardId={id}&restStatus={status}", token);
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

        void ShowAlert()
        {
            alertFrag = new RestrictAlertFrag();
            var trans = FragmentManager.BeginTransaction();
            alertFrag.Show(trans, "Dialog");
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