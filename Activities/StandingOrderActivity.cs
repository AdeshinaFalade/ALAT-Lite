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
using AndroidX.AppCompat.Widget;
using ALAT_Lite.Fragments;
using ALAT_Lite.Classes;
using Newtonsoft.Json.Linq;
using Xamarin.Essentials;
using Newtonsoft.Json;

namespace ALAT_Lite.Activities
{
    [Activity(Label = "StandiongOrderActivity")]
    public class StandingOrderActivity : AppCompatActivity
    {
        Toolbar toolbar;
        EditText edtStandingDay;
        EditText edtStandingAmount;
        public static int wardId;
        public static string token, status;
        ToggleButton toggleButton1;
        public ProgressFragment progressDialog;
        AppCompatButton btnSet;
        StandingOrderAlertFragment standingOrderAlertFragment;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.StandingOrderLayout);
            toolbar = FindViewById<Toolbar>(Resource.Id.standingOrderToolbar);
            edtStandingAmount = FindViewById<EditText>(Resource.Id.edtStandingAmount);
            edtStandingDay = FindViewById<EditText>(Resource.Id.edtStandingDay);
            btnSet = FindViewById<AppCompatButton>(Resource.Id.btnSet);
            toggleButton1 = FindViewById<ToggleButton>(Resource.Id.toggleButton1);

            //setup toolbar

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Standing Order";
            ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.black_arrow);
            actionBar.SetDisplayHomeAsUpEnabled(true);

            wardId = Preferences.Get("wardId", 0);
            token = Preferences.Get("token", "");

            GetStandingOrder(wardId);

            btnSet.Click += BtnSet_Click;
            toggleButton1.CheckedChange += ToggleButton1_CheckedChange; 

        }

        private void ToggleButton1_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (toggleButton1.Checked)
            {
                status = "true";
            }
            else
            {
                status = "false";
            }
        }

        private void BtnSet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(edtStandingAmount.Text))
            {
                Toast.MakeText(this, "Amount is required", ToastLength.Short).Show();
                return;
            } 
            else if (string.IsNullOrEmpty(edtStandingDay.Text))
            {
                Toast.MakeText(this, "Day is required", ToastLength.Short).Show();
                return;
            }
            else if (int.Parse(edtStandingAmount.Text) < 100 || int.Parse(edtStandingAmount.Text) > 1000000)
            {
                Toast.MakeText(this, "You can only transfer between N100 and N1,000,000", ToastLength.Short).Show();
                return;
            }
            else if (int.Parse(edtStandingDay.Text) < 1 || int.Parse(edtStandingDay.Text) > 28)
            {
                Toast.MakeText(this, "You can only select between 1-28", ToastLength.Short).Show();
                return;
            }

            SetStandingOrder(double.Parse(edtStandingAmount.Text), wardId, status, int.Parse(edtStandingDay.Text));
        }

        void ShowAlert()
        {
            standingOrderAlertFragment = new StandingOrderAlertFragment();
            var trans = FragmentManager.BeginTransaction();
            standingOrderAlertFragment.Show(trans, "Dialog");

        }

        public async void SetStandingOrder(double amount, int id, string status, int day)
        {
            string result = string.Empty;
            try
            {
                ShowProgressDialog("Setting");
                result = await NetworkUtils.PostData($"Guardian/UpdateStandingInstruction?wardId={id}&amount={amount}&stdInsDate={day}&isActivated={status}", token);
                if (!string.IsNullOrEmpty(result) && result.ToString() == "Updated")
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

        public async void GetStandingOrder(int id)
        {
            string result = string.Empty;
            try
            {
                ShowProgressDialog("Loading");
                result = await NetworkUtils.GetUserData($"Guardian/get-standing-instructions?wardId={id}", token);
                if (!string.IsNullOrEmpty(result) && result != "Unauthorized")
                {
                    var standingDetails = JsonConvert.DeserializeObject<StandingOrderResponse>(result);
                    var status = standingDetails.isActivated;
                    var amount = standingDetails.amount;
                    var day = standingDetails.dayOfTrans;

                    toggleButton1.Checked = status;
                    edtStandingAmount.Text = amount.ToString();
                    edtStandingDay.Text = day.ToString();

                    CloseProgressDialog();
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