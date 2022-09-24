using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.Button;
using Google.Android.Material.TextField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALAT_Lite.Activities
{
    [Activity(Label = "AccountStatementActivity")]
    public class AccountStatementActivity : AppCompatActivity
    {
        MaterialButton btnRequest;
        TextInputEditText acctNo, startDate, endDate, pin;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.child_account_statement);
            acctNo = FindViewById<TextInputEditText>(Resource.Id.acct_no);
            startDate = FindViewById<TextInputEditText>(Resource.Id.start_date);
            endDate = FindViewById<TextInputEditText>(Resource.Id.end_date);
            pin = FindViewById<TextInputEditText>(Resource.Id.pin);
            btnRequest = FindViewById<MaterialButton>(Resource.Id.btn_request);
            btnRequest.Click += BtnRequest_Click;
            // Create your application here
        }
        private bool ValidateEntry()
        {
            if (string.IsNullOrEmpty(startDate.Text) || string.IsNullOrEmpty(endDate.Text) || string.IsNullOrEmpty(pin.Text))
            {
                Toast.MakeText(this, "Fields cannot be empty", ToastLength.Short).Show();
                return false;
            }
            return true;
        }
        private void BtnRequest_Click(object sender, EventArgs e)
        {
            var validateEntry = ValidateEntry();
            if (validateEntry == true)
            {
                Dialog popupDialog = new Dialog(this);
                popupDialog.SetContentView(Resource.Layout.child_recharge_success);
                popupDialog.Window.SetSoftInputMode(SoftInput.AdjustResize);
                popupDialog.SetCanceledOnTouchOutside(false);
                popupDialog.Show();
            }
        }
    }
}