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
using Google.Android.Material.Snackbar;
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
    [Activity(Label = "TransferActivity")]
    public class TransferActivity : AppCompatActivity
    {
        Spinner spinner;
        EditText edtAcctNum, edtAcctName, edtTransAmount, edtTransferPIN;
        Toolbar toolbar;
        AppCompatButton btnTransfer;
        AlertDialogFragment alertDialogFragment;
        public ProgressFragment progressDialog;
        public static int userId;
        public static string token, accountNumber, accountBalance;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Transfer_layout);
            toolbar = FindViewById<Toolbar>(Resource.Id.transferToolbar);
            spinner = FindViewById<Spinner>(Resource.Id.spinner1);
            btnTransfer = FindViewById<AppCompatButton>(Resource.Id.btnTransfer);
            edtAcctName = FindViewById<EditText>(Resource.Id.edtAcctName);
            edtTransAmount = FindViewById<EditText>(Resource.Id.edtTransAmount);
            edtAcctNum = FindViewById<EditText>(Resource.Id.edtAcctNum);
            edtTransferPIN = FindViewById<EditText>(Resource.Id.edtTransferPIN);


            //setup toolbar

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Transfer Funds";
            ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.black_arrow);
            actionBar.SetDisplayHomeAsUpEnabled(true);

            userId = Preferences.Get("userId", 0);
            token = Preferences.Get("token", "");
            accountNumber = Preferences.Get("accountNumber", "");
            accountBalance = Preferences.Get("accountBalance", "");


            //spinner setup
            ArrayAdapter adapter;
            adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.Banks, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            //get info from the viewpager in royalkiddies activity
            var acct = Intent.GetStringExtra("acct");
            var kidName = Intent.GetStringExtra("name");

            edtAcctNum.Text = acct;
            edtAcctName.Text = kidName;

            // click event handlers
            btnTransfer.Click += BtnTransfer_Click; 
        }

        private void BtnTransfer_Click(object sender, EventArgs e)
        {
            var acctnum = edtAcctNum.Text;
            var amt = edtTransAmount.Text;
            var pin = edtTransferPIN.Text;

            // input validations

            if (string.IsNullOrEmpty(acctnum))
            {
                Toast.MakeText(this, "Enter a valid account number", ToastLength.Short).Show();
                return;
            }
            else if (acctnum.Length != 10)
            {
                Toast.MakeText(this, "Invalid account number", ToastLength.Short).Show();
                return;
            }
            else if (string.IsNullOrEmpty(amt))
            {
                Toast.MakeText(this, "Amount is required", ToastLength.Short).Show();
                return;
            }
            else if (int.Parse(amt) < 100 || int.Parse(amt) > 1000000)
            {
                Toast.MakeText(this, "You can only transfer between N100 and N1,000,000", ToastLength.Short).Show();
                return;
            }
            else if (string.IsNullOrEmpty(pin) || pin.Length < 6)
            {
                Toast.MakeText(this, "Enter a valid password", ToastLength.Short).Show();
                return;
            }
            else if (double.Parse(amt) >= double.Parse(accountBalance))
            {
                Toast.MakeText(this, "Insufficient funds", ToastLength.Short).Show();
                return;
            }
            Transfer(userId, accountNumber, acctnum, double.Parse(amt), pin);


        }

        void ShowAlert()
        {
            //dialog fragment
            alertDialogFragment = new AlertDialogFragment();
            var trans = FragmentManager.BeginTransaction();
            alertDialogFragment.Show(trans, "Dialog");
        }

        public async void Transfer(int id, string sendersAcct, string recipientAcct, double amount, string password)
        {
            string result = string.Empty;
            try
            {
                ShowProgressDialog("Processing");
                result = await NetworkUtils.PostData($"Guardian/transfer_to_ward?userId={id}&fromAccount={sendersAcct}&toAccount={recipientAcct}&amount={amount}&password={password}", token);
                var resultObject = JObject.Parse(result);
                if (!string.IsNullOrEmpty(result) && resultObject["responseMessage"].ToString() == "Transaction Successful")
                {
                    CloseProgressDialog();
                    ShowAlert();
                }
                else if (!string.IsNullOrEmpty(result) && resultObject["responseMessage"].ToString().ToLower() == "enter valid password")
                {
                    CloseProgressDialog();
                    Toast.MakeText(this, "Wrong password", ToastLength.Short).Show();
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

        //for back button on top
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