using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using Google.Android.Material.Button;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Xamarin.Essentials.Platform;
using Intent = Android.Content.Intent;

namespace ALAT_Lite.Activities
{
    [Activity(Label = "ChildLoginActivity")]
    public class ChildLoginActivity : AppCompatActivity
    {
        TextView fgtPassword;
        AppCompatTextView emailErr,pwdErr;
        EditText emailEdt, pwdEdt;
        MaterialButton btnSignIn;

        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.child_login);          
            fgtPassword = FindViewById<TextView>(Resource.Id.forgotPassword);
            fgtPassword.Click += FgtPassword_Click;
            emailEdt = FindViewById<EditText>(Resource.Id.edtEmail);
            pwdEdt = FindViewById<EditText>(Resource.Id.edtPwd);
            btnSignIn = FindViewById<MaterialButton>(Resource.Id.btnSignIn);
            btnSignIn.Click += BtnSignIn_Click;
            emailErr = FindViewById<AppCompatTextView>(Resource.Id.emailErr);
            pwdErr = FindViewById<AppCompatTextView>(Resource.Id.pwdErr);


            // Create your application here
        }

        private bool VerifyInput()
        {
            var userEmail = emailEdt.Text;
            var userPassword = pwdEdt.Text;
            if (userEmail == string.Empty)
            {
                emailErr.Visibility = ViewStates.Visible;
                return false;
            }
            else if (userPassword == string.Empty)
            {
                pwdErr.Visibility = ViewStates.Visible;
                return false ;
            }
            else if (!Android.Util.Patterns.EmailAddress.Matcher(userEmail).Matches())
            {
                emailErr.Text = "Invalid Email Address";
                emailErr.Visibility = ViewStates.Visible;
                return false;
            }
            return true;
        }

        private void BtnSignIn_Click(object sender, EventArgs e)
       {
               var verifyInput =  VerifyInput();
            if (verifyInput)
            {
                var myIntent = new Intent(this, typeof(ChildDashboardActivity));
                StartActivityForResult(myIntent, 8);
                SetResult(Result.Ok, myIntent);
                Finish();
            }
                
            
        }

        private void FgtPassword_Click(object sender, EventArgs e)
        {
            var myIntent = new Intent(this, typeof(ChildForgotPassword));
            StartActivityForResult(myIntent, 7000);
            SetResult(Result.Ok, myIntent);
            Finish();
        }

        public override void OnBackPressed()
        {
            var myIntent = new Intent(this, typeof(MainActivity));
            StartActivityForResult(myIntent, 2000);
            SetResult(Result.Ok, myIntent);
            Finish();
        }
    }
}