using ALAT_Lite.Fragments;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActionBar = AndroidX.AppCompat.App.ActionBar;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace ALAT_Lite.Activities
{
    [Activity(Label = "SetLimitActivity")]
    public class SetLimitActivity : AppCompatActivity
    {
        Toolbar toolbar;
        EditText edtLimitAmount;
        SetLimitAlertFrag setLimitAlertFrag;
        AppCompatButton btnSetLimit;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SetLimitLayout);
            toolbar = FindViewById<Toolbar>(Resource.Id.limitToolbar);
            btnSetLimit = FindViewById<AppCompatButton>(Resource.Id.btnSetLimit);
            edtLimitAmount = FindViewById<EditText>(Resource.Id.edtLimitAmount);

            //setup toolbar

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Set Limit";
            ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.black_arrow);
            actionBar.SetDisplayHomeAsUpEnabled(true);

            //connect
            btnSetLimit.Click += BtnSetLimit_Click; 
        }

        private void BtnSetLimit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(edtLimitAmount.Text))
            {
                Toast.MakeText(this, "Amount is required", ToastLength.Short).Show();
                return;
            }
            else if (int.Parse(edtLimitAmount.Text) < 1000 | int.Parse(edtLimitAmount.Text) > 50000)
            {
                Toast.MakeText(this, "Amount can only be between N1,000 and N50,000", ToastLength.Short).Show();
                return;
            }
            setLimitAlertFrag = new SetLimitAlertFrag();
            var trans = FragmentManager.BeginTransaction();
            setLimitAlertFrag.Show(trans,"Dialog");
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    OnBackPressed();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }

        }
    }
}