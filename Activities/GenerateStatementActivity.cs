using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActionBar = AndroidX.AppCompat.App.ActionBar;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;
using AndroidX.AppCompat.Widget;
using ALAT_Lite.Fragments;
using AndroidX.AppCompat.App;

namespace ALAT_Lite.Activities
{
    [Activity(Label = "GenerateStatementActivity")]
    public class GenerateStatementActivity : AppCompatActivity
    {
        Toolbar toolbar;
        AppCompatButton btnStartDate;
        AppCompatButton btnEndDate;
        AppCompatButton btnGenerate;
        AcctStatementAlertFrag alertFrag;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.GenerateStatementLayout);
            toolbar = FindViewById<Toolbar>(Resource.Id.GenerateStatementToolbar);
            btnStartDate = FindViewById<AppCompatButton>(Resource.Id.btnStartDate);
            btnEndDate = FindViewById<AppCompatButton>(Resource.Id.btnEndDate);
            btnGenerate = FindViewById<AppCompatButton>(Resource.Id.btnGenerate);

            //setup toolbar

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Generate Statement";
            ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.black_arrow);
            actionBar.SetDisplayHomeAsUpEnabled(true);

            //connect
            btnStartDate.Click += BtnStartDate_Click;
            btnEndDate.Click += BtnEndDate_Click;
            btnGenerate.Click += BtnGenerate_Click; 

        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            alertFrag = new AcctStatementAlertFrag();
            var trans = FragmentManager.BeginTransaction();
            alertFrag.Show(trans, "Dialog");
        }

        private void BtnEndDate_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                btnEndDate.Text = time.ToShortDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void BtnStartDate_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                btnStartDate.Text = time.ToShortDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
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