using ALAT_Lite.Classes;
using ALAT_Lite.Fragments;
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
using ActionBar = AndroidX.AppCompat.App.ActionBar;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace ALAT_Lite.Activities
{
    [Activity(Label = "RoyalListActivity")]
    public class RoyalListActivity : AppCompatActivity
    {
        Toolbar toolbar;
        public ProgressFragment progressDialog;
        public static string AcctNum, KidName, token;
        public static int wardId, userId;
        public static List<ChildClass> listOfChildren = new List<ChildClass>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.RoyalAcctList);
            toolbar = FindViewById<Toolbar>(Resource.Id.wardlistToolbar);

            //setup toolbar

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Royal Kiddies";
            ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.black_arrow);
            actionBar.SetDisplayHomeAsUpEnabled(true);
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