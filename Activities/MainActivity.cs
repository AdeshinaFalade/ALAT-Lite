using ALAT_Lite.Activities;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using System.Reflection.Emit;
using static Android.Content.Res.Resources;


namespace ALAT_Lite
{
    
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText email;
        AppCompatTextView emailError, passwordError;
        EditText password;
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
            password = FindViewById<EditText>(Resource.Id.edtPassword);
            login = FindViewById<AppCompatButton>(Resource.Id.btnLogin);
            emailError = FindViewById<AppCompatTextView>(Resource.Id.mailError);
            passwordError = FindViewById<AppCompatTextView>(Resource.Id.passwordError);
            login.Click += Login_Click;

        }

        private bool VerifyInput()
        {
            var userEmail = email.Text;
            var userPassword = password.Text;
            if (string.IsNullOrEmpty(userEmail))
            {
                emailError.Visibility = ViewStates.Visible;
                return false;
            }
            if (string.IsNullOrEmpty(userPassword))
            {
                passwordError.Visibility = ViewStates.Visible;
                return false;
            }
            else if (!Android.Util.Patterns.EmailAddress.Matcher(userEmail).Matches())
            {
                emailError.Text = "Invalid Email Address";
                emailError.Visibility = ViewStates.Visible;
                return false;
            }
            emailError.Visibility = ViewStates.Invisible;
            emailError.Visibility = ViewStates.Invisible;
            return true;
        }

        private void Login_Click(object sender, System.EventArgs e)
        {
            if (VerifyInput())
            {
                Intent intent = new Intent(this, typeof(GuardianActivity));
                StartActivity(intent); 
            }
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