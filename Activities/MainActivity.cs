using ALAT_Lite.Activities;
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
using Google.Android.Material.TextField;
using Plugin.Connectivity;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Text;
using static Android.Content.Res.Resources;
using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;

namespace ALAT_Lite
{
    
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText email;
        TextInputEditText password;
        AppCompatButton login;
        public ProgressFragment progressDialog;
        ImageView childImage;
        public static string baseUrl = "https://galacticos-alat-apim.azure-api.net/api/Authentication/Login/";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
             childImage = FindViewById<ImageView>(Resource.Id.childImage);
            childImage.Click += ChildImage_Click;
            email = FindViewById<EditText>(Resource.Id.edtEmail);
            password = FindViewById<TextInputEditText>(Resource.Id.edtPassword);
            login = FindViewById<AppCompatButton>(Resource.Id.btnLogin);
            login.Click += Login_Click;

        }

        private void Login_Click(object sender, EventArgs e)
        {
            var userEmail = email.Text;
            var userPassword = password.Text;
            if (!CrossConnectivity.Current.IsConnected)
            {
                Toast.MakeText(this, "No internet connection", ToastLength.Short).Show();
                return;
            }
            if (string.IsNullOrEmpty(userEmail))
            {
                Toast.MakeText(this, "Email is required", ToastLength.Short).Show();
                return;
            }
            else if (!Android.Util.Patterns.EmailAddress.Matcher(userEmail).Matches())
            {
                Toast.MakeText(this, "Invalid Email Address", ToastLength.Short).Show();
                return;
            }
            else if (string.IsNullOrEmpty(userPassword))
            {
                Toast.MakeText(this, "Password is required", ToastLength.Short).Show();
                return;
            }
            else if (userPassword.Length < 6)
            {
                Toast.MakeText(this, "Invalid Password", ToastLength.Short).Show();
                return;
            }

            /**
            Intent intent = new Intent(this, typeof(GuardianActivity));
            StartActivity(intent);
            password.Text = "" ;
            **/

            //SendData();
            LoginUserAccount(userEmail, userPassword);
        }
        public async void LoginUserAccount(string mail, string passwrd)
        {
            string result = string.Empty;
            try
            {
                GuardianLogin model = new GuardianLogin() { username = mail, password = passwrd };
                var rawString = JsonConvert.SerializeObject(model);
                ShowProgressDialog("Authenticating");
                result = await NetworkUtils.PostUserData("Authentication/Login", rawString);
                if (!string.IsNullOrEmpty(result))
                {
                   // Toast.MakeText(this, "Logged in successfully", ToastLength.Short).Show();

                    var loginDetails = JsonConvert.DeserializeObject<GuardianLoginResponse>(result);
                    var userId = loginDetails.userId.ToString();
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

                    Intent intent = new Intent(this, typeof(GuardianActivity));
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

                    CloseProgressDialog();
                    StartActivity(intent);
                    password.Text = "";
                    // var desiioo = JsonConvert.DeserializeObject<LoginModel>(result);
                }
                else
                {
                    CloseProgressDialog();
                    Toast.MakeText(this, "Oops!, Kindly try again.", ToastLength.Long).Show();
                }
            }
            catch (Exception e)
            {
                CloseProgressDialog();
                Toast.MakeText(this, "Oops!, Kindly try again. " + e.Message, ToastLength.Long).Show();
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

        private void ChildImage_Click(object sender, System.EventArgs e)
        {
            var myIntent = new Intent(this,typeof(ChildLoginActivity));
            StartActivityForResult(myIntent,1000);
            SetResult(Result.Ok, myIntent);
            Finish();
        }


       
    }
}