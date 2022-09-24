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
    [Activity(Label = "TransferActivity")]
    public class TransferActivity : AppCompatActivity
    {
        Spinner spinner;
        Toolbar toolbar;
        AppCompatButton btnTransfer;
        AlertDialogFragment alertDialogFragment;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Transfer_layout);
            toolbar = FindViewById<Toolbar>(Resource.Id.transferToolbar);
            spinner = FindViewById<Spinner>(Resource.Id.spinner1);
            btnTransfer = FindViewById<AppCompatButton>(Resource.Id.btnTransfer);

            //setup toolbar

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Transfer Funds";
            ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.black_arrow);
            actionBar.SetDisplayHomeAsUpEnabled(true);


            //spinner setup
            ArrayAdapter adapter;
            adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.Banks, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            // click event handlers
            btnTransfer.Click += BtnTransfer_Click; 
        }

        private void BtnTransfer_Click(object sender, EventArgs e)
        {
            alertDialogFragment = new AlertDialogFragment();
            var trans = FragmentManager.BeginTransaction();
            alertDialogFragment.Show(trans, "Dialog");
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