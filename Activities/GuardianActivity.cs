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
using Xamarin.Essentials;
using static AndroidX.ViewPager.Widget.ViewPager;
using static AndroidX.ViewPager2.Widget.ViewPager2;
using System.Text;
using AlertDialog = AndroidX.AppCompat.App.AlertDialog;
using System.Globalization;
using ALAT_Lite.Fragments;
using Android.Support.V4.Widget;
using Newtonsoft.Json;

namespace ALAT_Lite.Activities
{
    [Activity(Label = "GuardianActivity")]
    public class GuardianActivity : AppCompatActivity
    {
        TextView customerName, txtAcctNumber;
        TextView balance;
        SwipeRefreshLayout swipeRefreshLayout1;
        ImageView visibility;
        public ProgressFragment progressDialog;
        LinearLayout createAcct;
        LinearLayout royalKiddies;
        NumberFormatInfo myNumberFormatInfo = new CultureInfo("yo-NG", false).NumberFormat;
        LinearLayout sendMoney;
        bool Clicked = true;
        public static double bal;
        public static string accountBalance, loginEmail, loginPassword, firstName, lastName, formattedBal, accountNumber;
        public static int userId;

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
            swipeRefreshLayout1 = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout1);

            LoadDashboard();

            //to do show and hide balance
            visibility.Click += Visibility_Click;

            createAcct.Click += CreateAcct_Click;
            royalKiddies.Click += RoyalKiddies_Click;
            sendMoney.Click += SendMoney_Click;
            swipeRefreshLayout1.Refresh += SwipeRefreshLayout1_Refresh;

        }

        public void LoadDashboard()
        {
            userId = Preferences.Get("userId", 0);
            firstName = Preferences.Get("firstName", "");
            lastName = Preferences.Get("lastName", "");
            accountBalance = Preferences.Get("accountBalance", "");
            accountNumber = Preferences.Get("accountNumber", "");
            loginEmail = Preferences.Get("loginEmail", "");
            loginPassword = Preferences.Get("loginPassword", "");

            //format string to currency
            bal = double.Parse(accountBalance);
            formattedBal = bal.ToString("C", myNumberFormatInfo);
            balance.Text = formattedBal;
            customerName.Text = "Hi, " + firstName;
            txtAcctNumber.Text = accountNumber;
        }

        private void SwipeRefreshLayout1_Refresh(object sender, EventArgs e)
        {
            //to refresh 
            RefreshData(loginEmail, loginPassword);
            swipeRefreshLayout1.Refreshing = false;
        }

        public async void RefreshData(string mail, string passwrd)
        {
            //this method will automatically login with the provided password and mail to get fresh token and balance
            string result = string.Empty;
            try
            {
                GuardianLogin model = new GuardianLogin() { username = mail, password = passwrd };
                var rawString = JsonConvert.SerializeObject(model);
                result = await NetworkUtils.PostUserData("Authentication//GuardianLogin", rawString);
                if (!string.IsNullOrEmpty(result))
                {
                    // Toast.MakeText(this, "Logged in successfully", ToastLength.Short).Show();

                    var loginDetails = JsonConvert.DeserializeObject<GuardianLoginResponse>(result);
                    var userId = loginDetails.userId;
                    var firstName = loginDetails.firstName.ToString();
                    var lastName = loginDetails.lastName.ToString();
                    var gender = loginDetails.gender.ToString();
                    var phoneNumber = loginDetails.phoneNumber.ToString();
                    var email = loginDetails.email.ToString();
                    var accountBalance = loginDetails.accountBalance.ToString();
                    var accountNumber = loginDetails.accountNumber.ToString();
                    var accountStatus = loginDetails.accountstatus.ToString();
                    var accountType = loginDetails.accountType.ToString();
                    var token = loginDetails.token.ToString();

                    Preferences.Set("userId", userId);
                    Preferences.Set("firstName", firstName);
                    Preferences.Set("lastName", lastName);
                    Preferences.Set("gender", gender);
                    Preferences.Set("phoneNumber", phoneNumber);
                    Preferences.Set("email", email);
                    Preferences.Set("accountBalance", accountBalance);
                    Preferences.Set("accountNumber", accountNumber);
                    Preferences.Set("accountStatus", accountStatus);
                    Preferences.Set("accountType", accountType);
                    Preferences.Set("token", token);


                    LoadDashboard();

                }
                else
                {
                    Toast.MakeText(this, "Oops! an error occured, Kindly try again.", ToastLength.Short).Show();
                }
            }
            catch (Exception e)
            {
                    Toast.MakeText(this, "Oops! an error occured, Kindly try again. ", ToastLength.Short).Show();
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
