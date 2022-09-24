using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using Google.Android.Material.Button;
using Google.Android.Material.TextField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALAT_Lite.Activities
{
    [Activity(Label = "ChildRechargeActivity")]
    public class ChildRechargeActivity : AppCompatActivity
    {
        AppCompatImageView backArrow;
        Spinner network;
        ImageView btnHome;
        MaterialButton recharge;
        TextInputEditText phoneNo,amount, pin;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.child_airtime_recharge);
            backArrow = FindViewById<AppCompatImageView>(Resource.Id.back_arrow);
            backArrow.Click += BackArrow_Click;
            btnHome = FindViewById<ImageView>(Resource.Id.btnHome);
            btnHome.Click += BtnHome_Click;
            // Create your application here
            network = FindViewById<Spinner>(Resource.Id.network);
            network.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Network_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.network_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            network.Adapter = adapter;
            phoneNo = FindViewById<TextInputEditText>(Resource.Id.phone_no);
            amount = FindViewById<TextInputEditText>(Resource.Id.amount);
            pin = FindViewById<TextInputEditText>(Resource.Id.pin);
            recharge = FindViewById<MaterialButton>(Resource.Id.recharge);
            recharge.Click += Recharge_Click;
        }

        private bool ValidateEntry()
        {
            if(string.IsNullOrEmpty(phoneNo.Text)|| string.IsNullOrEmpty(amount.Text) || string.IsNullOrEmpty(pin.Text))
            {
                Toast.MakeText(this, "Fields cannot be empty",ToastLength.Short).Show();
                return false;
            }
            return true;
        }

        private void Recharge_Click(object sender, EventArgs e)
        {
            var validateEntry = ValidateEntry();
            if(validateEntry == true)
            {
               Dialog popupDialog = new Dialog(this);
                popupDialog.SetContentView(Resource.Layout.child_recharge_success);
                popupDialog.Window.SetSoftInputMode(SoftInput.AdjustResize);
                popupDialog.Show();
            }
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            ReturnToDashboard();
        }

        private void Network_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            network = (Spinner)sender;
            network.GetItemAtPosition(e.Position);
        }

        private void BackArrow_Click(object sender, EventArgs e)
        {
            ReturnToDashboard();
        }

        public override void OnBackPressed()
        {
            ReturnToDashboard();
        }
        public void ReturnToDashboard()
        {
            var myIntent = new Intent(this, typeof(ChildDashboardActivity));
            StartActivityForResult(myIntent, 2);
            SetResult(Result.Ok, myIntent);
            Finish();
        }
    }
}