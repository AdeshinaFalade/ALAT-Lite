using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.Button;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALAT_Lite.Activities
{
    [Activity(Label = "ChildForgotPassword")]
    public class ChildForgotPassword : AppCompatActivity
    {
        EditText emailEdit;
        TextView resetPwd;
        MaterialButton btnResetPwd;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.forgot_password);
            // Create your application here
            emailEdit = FindViewById<EditText>(Resource.Id.emailEdt);
            resetPwd = FindViewById<TextView>(Resource.Id.resetPwdText);
            resetPwd.Click += ResetPwd_Click;
            btnResetPwd = FindViewById<MaterialButton>(Resource.Id.resetPwdBtn);
            btnResetPwd.Click += BtnResetPwd_Click;


        }

        private void BtnResetPwd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(emailEdit.Text) || !emailEdit.Text.Contains('@'))
            {
                Toast.MakeText(this,"Enter a valid email",ToastLength.Long).Show();
            }
        }

        private void ResetPwd_Click(object sender, EventArgs e)
        {
            var myIntent = new Intent(this, typeof(ChildLoginActivity));
            StartActivityForResult(myIntent, 6000);
            SetResult(Result.Ok, myIntent);
            Finish();
        }
    }
}