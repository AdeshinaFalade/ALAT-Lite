using ALAT_Lite.Adapters;
using ALAT_Lite.Classes;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.ViewPager2.Widget;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;

using static AndroidX.ViewPager.Widget.ViewPager;
using static AndroidX.ViewPager2.Widget.ViewPager2;
using System.Text;
using AlertDialog = AndroidX.AppCompat.App.AlertDialog;
using System.Globalization;
using ALAT_Lite.Fragments;

namespace ALAT_Lite.Activities
{
    [Activity(Label = "GuardianActivity")]
    public class GuardianActivity : AppCompatActivity
    {
        TextView customerName, txtAcctNumber;
        TextView balance;
        ImageView visibility;
        LinearLayout createAcct;
        LinearLayout royalKiddies;
        NumberFormatInfo myNumberFormatInfo = new CultureInfo("yo-NG", false).NumberFormat;
        LinearLayout sendMoney;
        bool Clicked = true;
        public double bal;
        public static string accountBalance;
        public static string formattedBal;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.guardian_Profile);
            customerName = FindViewById<TextView>(Resource.Id.txtCustomerName);
            txtAcctNumber = FindViewById<TextView>(Resource.Id.txtAcctNumber);
            balance = FindViewById<TextView>(Resource.Id.txtGuardianBalance);
            visibility = FindViewById<ImageView>(Resource.Id.imgVisibility);
            createAcct = FindViewById<LinearLayout>(Resource.Id.linearLayout6);
            royalKiddies = FindViewById<LinearLayout>(Resource.Id.linearLayout8);
            sendMoney = FindViewById<LinearLayout>(Resource.Id.linearLayout5);


            /**
            intent.PutExtra("userId", userId);
            intent.PutExtra("firstName", firstName);
            intent.PutExtra("lastName", lastName);
            intent.PutExtra("gender", gender);
            intent.PutExtra("phoneNumber", phoneNumber);
            intent.PutExtra("email", email);
            intent.PutExtra("accountBalance", accountBalance);
            intent.PutExtra("accountNumber", accountNumber);
            intent.PutExtra("accountStatus", accountStatus);
            intent.PutExtra("accountType", accountType);
            intent.PutExtra("token", token);
            **/


            var userId = Intent.GetStringExtra("userId");
            var firstName = Intent.GetStringExtra("firstName");
            var lastName = Intent.GetStringExtra("lastName");
            accountBalance = Intent.GetStringExtra("accountBalance");
            var accountNumber = Intent.GetStringExtra("accountNumber");
            var bal = double.Parse(accountBalance);
            formattedBal = bal.ToString("C", myNumberFormatInfo);
            balance.Text = formattedBal;
            customerName.Text = "Hi, " + firstName;
            txtAcctNumber.Text = accountNumber;

            visibility.Click += Visibility_Click;
            createAcct.Click += CreateAcct_Click;
            royalKiddies.Click += RoyalKiddies_Click;
            sendMoney.Click += SendMoney_Click;

        }

        private void SendMoney_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(TransferActivity));
            StartActivity(intent);
        }

        private void RoyalKiddies_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(RoyalKiddiesActivity));
            StartActivity(intent);
        }

        private void CreateAcct_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(CreateAccount));
            StartActivity(intent);
        }

        private void Visibility_Click(object sender, EventArgs e)
        {

            if (Clicked)
            {
                visibility.SetImageResource(Resource.Drawable.baseline_visibility_20);
                balance.Text = "******.**";
            }
            else
            {
                visibility.SetImageResource(Resource.Drawable.baseline_visibility_off_24);
                balance.Text = formattedBal;
            }
            Clicked = !Clicked;
        }

        

        public override void OnBackPressed()
        {
            //to confirm logout
            AlertDialog.Builder Alert = new AlertDialog.Builder(this);
            Alert.SetTitle("Log Out");
            Alert.SetCancelable(false);
            Alert.SetMessage("Are you sure?");
            Alert.SetNegativeButton("No", (sender, e) =>
            {
                return;

            });

            Alert.SetPositiveButton("Yes", (sender, e) =>
            {
                base.OnBackPressed();
            });
            Alert.Show();
        }

    }
}
