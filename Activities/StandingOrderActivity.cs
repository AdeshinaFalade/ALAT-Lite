using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using ActionBar = AndroidX.AppCompat.App.ActionBar;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroidX.AppCompat.Widget;
using ALAT_Lite.Fragments;

namespace ALAT_Lite.Activities
{
    [Activity(Label = "StandiongOrderActivity")]
    public class StandingOrderActivity : AppCompatActivity
    {
        Toolbar toolbar;
        EditText edtStandingDay;
        EditText edtStandingAmount;
        AppCompatButton btnSet;
        StandingOrderAlertFragment standingOrderAlertFragment;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.StandingOrderLayout);
            toolbar = FindViewById<Toolbar>(Resource.Id.standingOrderToolbar);
            edtStandingAmount = FindViewById<EditText>(Resource.Id.edtStandingAmount);
            edtStandingDay = FindViewById<EditText>(Resource.Id.edtStandingDay);
            btnSet = FindViewById<AppCompatButton>(Resource.Id.btnSet);

            //setup toolbar

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Standing Order";
            ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.black_arrow);
            actionBar.SetDisplayHomeAsUpEnabled(true);

            btnSet.Click += BtnSet_Click;   

        }

        private void BtnSet_Click(object sender, EventArgs e)
        {
            standingOrderAlertFragment = new StandingOrderAlertFragment();
            var trans = FragmentManager.BeginTransaction();
            standingOrderAlertFragment.Show(trans, "Dialog");
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