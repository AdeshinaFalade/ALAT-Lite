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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


            //spinner setup
            ArrayAdapter adapter;
            adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.Banks, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
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
            else if (int.Parse(amt) < 100 | int.Parse(amt) > 1000000)
            {
                Toast.MakeText(this, "You can only transfer between N100 and N1,000,000", ToastLength.Short).Show();
                return;
            }
            else if (string.IsNullOrEmpty(pin))
            {
                Toast.MakeText(this, "Enter a valid pin", ToastLength.Short).Show();
                return;
            }
        

            alertDialogFragment = new AlertDialogFragment();
            var trans = FragmentManager.BeginTransaction();
            alertDialogFragment.Show(trans, "Dialog");
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