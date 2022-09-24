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
using ALAT_Lite.Fragments;
using System.Text;
using ActionBar = AndroidX.AppCompat.App.ActionBar;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace ALAT_Lite.Activities
{
    [Activity(Label = "RestrictAccountActivity")]
    public class RestrictAccountActivity : AppCompatActivity
    {
        Spinner spinner;
        AppCompatButton btnApply;
        Toolbar toolbar;
        RestrictAlertFrag alertFrag;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.RestrictAccountLayout);
            toolbar = FindViewById<Toolbar>(Resource.Id.restrictToolbar);
            spinner = FindViewById<Spinner>(Resource.Id.spinner2);
            btnApply = FindViewById<AppCompatButton>(Resource.Id.btnApply);

            //setup toolbar

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Restrict Account";
            ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.black_arrow);
            actionBar.SetDisplayHomeAsUpEnabled(true);


            //spinner setup
            ArrayAdapter adapter;
            adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.Choose, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            btnApply.Click += BtnApply_Click;   
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            ShowAlert();
        }
        void ShowAlert()
        {
            alertFrag = new RestrictAlertFrag();
            var trans = FragmentManager.BeginTransaction();
            alertFrag.Show(trans, "Dialog");
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