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
    [Activity(Label = "MaintenanceActivity")]
    public class MaintenanceActivity : AppCompatActivity
    {
        Toolbar toolbar;
        RelativeLayout btnStandingOrder;
        RelativeLayout btnSetLimits;
        RelativeLayout btnAcctStatement;
        RelativeLayout btnRestrictAcct, btnDocumentation;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Maintenance_layout);
            toolbar = FindViewById<Toolbar>(Resource.Id.maintenanceToolbar);
            btnStandingOrder = FindViewById<RelativeLayout>(Resource.Id.btnStandingOrder);
            btnAcctStatement = FindViewById<RelativeLayout>(Resource.Id.btnAcctStatement);
            btnRestrictAcct = FindViewById<RelativeLayout>(Resource.Id.btnRestrictAcct);
            btnSetLimits = FindViewById<RelativeLayout>(Resource.Id.btnSetLimits);
            btnDocumentation = FindViewById<RelativeLayout>(Resource.Id.btnDocumentation);

            //setup toolbar

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Maintenance";
            ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.black_arrow);
            actionBar.SetDisplayHomeAsUpEnabled(true);

            //connect
            btnStandingOrder.Click += BtnStandingOrder_Click;
            btnAcctStatement.Click += BtnAcctStatement_Click;
            btnRestrictAcct.Click += BtnRestrictAcct_Click;
            btnSetLimits.Click += BtnSetLimits_Click;
            btnDocumentation.Click += BtnDocumentation_Click;

        }

        private void BtnDocumentation_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(DocumentationActivity));
            StartActivity(intent);
        }

        private void BtnSetLimits_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SetLimitActivity));
            StartActivity(intent);
        }

        private void BtnRestrictAcct_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(RestrictAccountActivity));
            StartActivity(intent);
        }

        private void BtnAcctStatement_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(GenerateStatementActivity));
            StartActivity(intent);
        }

        private void BtnStandingOrder_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(StandingOrderActivity));
            StartActivity(intent);
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