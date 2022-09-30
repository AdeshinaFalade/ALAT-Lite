using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlertDialog = AndroidX.AppCompat.App.AlertDialog;

namespace ALAT_Lite.Activities
{
    [Activity(Label = "GuardianActivity")]
    public class GuardianActivity : AppCompatActivity
    {
        TextView customerName;
        TextView balance;
        ImageView visibility;
        LinearLayout createAcct;
        LinearLayout royalKiddies;
        LinearLayout sendMoney;
        bool Clicked = true;
        string bal = "54,000.34";
        private static Context mContext;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.guardian_Profile);
            customerName = FindViewById<TextView>(Resource.Id.txtCustomerName);
            balance = FindViewById<TextView>(Resource.Id.txtGuardianBalance);
            visibility = FindViewById<ImageView>(Resource.Id.imgVisibility);
            createAcct = FindViewById<LinearLayout>(Resource.Id.linearLayout6);
            royalKiddies = FindViewById<LinearLayout>(Resource.Id.linearLayout8);
            sendMoney = FindViewById<LinearLayout>(Resource.Id.linearLayout5);
            balance.Text = bal;
            visibility.Click += Visibility_Click;
            createAcct.Click += CreateAcct_Click;
            royalKiddies.Click += RoyalKiddies_Click;
            sendMoney.Click += SendMoney_Click;
            mContext = this;
        }

        private void SendMoney_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(mContext, typeof(TransferActivity));
            mContext.StartActivity(intent);
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
                visibility.SetImageResource(Resource.Drawable.baseline_visibility_off_24);
                balance.Text = "********";
            }
            else
            {
                visibility.SetImageResource(Resource.Drawable.baseline_visibility_20);
                balance.Text = bal;
            }
            Clicked = !Clicked;
        }

        public override void OnBackPressed()
        {
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