using ALAT_Lite.Activities;
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
using System.Reflection.Emit;
using static Android.Content.Res.Resources;

namespace ALAT_Lite
{
    
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText email;
        TextInputEditText password;
        AppCompatButton login;
        ImageView childImage;

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

        private void Login_Click(object sender, System.EventArgs e)
        {
            var userEmail = email.Text;
            var userPassword = password.Text;
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


            Intent intent = new Intent(this, typeof(GuardianActivity));
            StartActivity(intent);
            password.Text = "" ;
            
        }
        private void ChildImage_Click(object sender, System.EventArgs e)
        {
            var myIntent = new Intent(this,typeof(ChildLoginActivity));
            StartActivityForResult(myIntent,1000);
            SetResult(Result.Ok, myIntent);
            Finish();
        }
       

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}