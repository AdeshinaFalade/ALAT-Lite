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
using ActionBar = AndroidX.AppCompat.App.ActionBar;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;
using AndroidX.AppCompat.Widget;
using ALAT_Lite.Fragments;
using AndroidX.AppCompat.App;
using Xamarin.Essentials;
using ALAT_Lite.Classes;

namespace ALAT_Lite.Activities
{
    [Activity(Label = "GenerateStatementActivity")]
    public class GenerateStatementActivity : AppCompatActivity
    {
        Toolbar toolbar;
        AppCompatButton btnStartDate;
        AppCompatButton btnEndDate;
        public static int wardId, userId;
        public static string token;
        public ProgressFragment progressDialog;
        AppCompatButton btnGenerate;
        AcctStatementAlertFrag alertFrag;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.GenerateStatementLayout);
            toolbar = FindViewById<Toolbar>(Resource.Id.GenerateStatementToolbar);
            btnStartDate = FindViewById<AppCompatButton>(Resource.Id.btnStartDate);
            btnEndDate = FindViewById<AppCompatButton>(Resource.Id.btnEndDate);
            btnGenerate = FindViewById<AppCompatButton>(Resource.Id.btnGenerate);


            wardId = Preferences.Get("wardId", 0);
            token = Preferences.Get("token", "");
            userId = Preferences.Get("userId", 0);
            //setup toolbar
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Generate Statement";
            ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.black_arrow);
            actionBar.SetDisplayHomeAsUpEnabled(true);

            //connect
            btnStartDate.Click += BtnStartDate_Click;
            btnEndDate.Click += BtnEndDate_Click;
            btnGenerate.Click += BtnGenerate_Click; 

        }


        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(btnStartDate.Text))
            {
                Toast.MakeText(this, "Start Date is required", ToastLength.Short).Show();
                return;
            }
            else if (string.IsNullOrEmpty(btnEndDate.Text))
            {
                Toast.MakeText(this, "End Date is required", ToastLength.Short).Show();
                return;
            }
            else if (DateTime.Parse(btnStartDate.Text) >= DateTime.Now)
            {
                Toast.MakeText(this, "Pick a date before today", ToastLength.Short).Show();
                return;
            }
            else if (DateTime.Parse(btnEndDate.Text) >= DateTime.Now)
            {
                Toast.MakeText(this, "Pick a date before today", ToastLength.Short).Show();
                return;
            }
            else if (DateTime.Parse(btnEndDate.Text) < DateTime.Parse(btnStartDate.Text))
            {
                Toast.MakeText(this, "End date can't be less than start date", ToastLength.Short).Show();
                return;
            }
            var startDate = DateTime.Parse(btnStartDate.Text).ToString(@"MM-dd-yyyy");
            var endDate = DateTime.Parse(btnEndDate.Text).ToString(@"MM-dd-yyyy");
            GenerateStatement(startDate, endDate);
        }
        public async void GenerateStatement(string start, string stop)
        {
            string result = string.Empty;
            try
            {
                ShowProgressDialog("Processing");
                result = await NetworkUtils.PostData($"Guardian/GenerateAccountStatementWard?WardId={wardId}&GuardianId={userId}&startDate={start}&endDate={stop}", token);
                if (!string.IsNullOrEmpty(result) && result == "Sent")
                {
                    CloseProgressDialog();
                    ShowAlert();
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

        void ShowAlert()
        {
            alertFrag = new AcctStatementAlertFrag();
            var trans = FragmentManager.BeginTransaction();
            alertFrag.Show(trans, "Dialog");
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
        private void BtnEndDate_Click(object sender, EventArgs e)
        {
            
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                btnEndDate.Text = time.ToShortDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void BtnStartDate_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                btnStartDate.Text = time.ToShortDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
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