using ALAT_Lite.Fragments;
using Android;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActionBar = AndroidX.AppCompat.App.ActionBar;
using AlertDialog = AndroidX.AppCompat.App.AlertDialog;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace ALAT_Lite.Activities
{
    [Activity(Label = "WardDetailsActivity")]
    public class WardDetailsActivity : AppCompatActivity
    {
        Toolbar toolbar;
        FrameLayout frameLayout;
        //Spinner spinner;
        //ImageView imgAttachBirthCert, imgAttachPassport;
        //AppCompatButton btnDOB;
        //readonly string[] permissionGroup =
        //{
        //    Manifest.Permission.ReadExternalStorage,
        //    Manifest.Permission.WriteExternalStorage,
        //    Manifest.Permission.Camera
        //};
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.WardDetailsLayout);
            toolbar = FindViewById<Toolbar>(Resource.Id.wardDetToolbar);
            //frameLayout = FindViewById<FrameLayout>(Resource.Id.frameLayout1);
            //spinner = FindViewById<Spinner>(Resource.Id.spinner3);
            //btnDOB = FindViewById<AppCompatButton>(Resource.Id.btnDOB);
            //imgAttachPassport = FindViewById<ImageView>(Resource.Id.imgAttachPassport);
            //imgAttachBirthCert = FindViewById<ImageView>(Resource.Id.imgAttachBirthCert);

            //setup toolbar

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Create Account";
            ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.black_arrow);
            actionBar.SetDisplayHomeAsUpEnabled(true);

            var trans = FragmentManager.BeginTransaction();
            trans.Add(Resource.Id.frameLayout1, new WardDetailFragment(), "Fragment");
            //trans.AddToBackStack(null);
            trans.Commit();



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

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}