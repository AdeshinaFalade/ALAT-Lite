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
using ALAT_Lite.Adapters;
using ALAT_Lite.Classes;
using Android.Support.V7.Widget;

namespace ALAT_Lite.Activities
{
    [Activity(Label = "WardTrans")]
    public class WardTransActivity : AppCompatActivity
    {
        Toolbar toolbar;
        RecyclerView recyclerView;
        RecyclerAdapter recyclerAdapter;
        List<TransactionModel> listOfTrans = new List<TransactionModel>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.WardTransactionHistory);
            toolbar = FindViewById<Toolbar>(Resource.Id.wardTransToolbar);
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView1);

            //setup toolbar

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Transaction History";
            ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.black_arrow);
            actionBar.SetDisplayHomeAsUpEnabled(true);

            SetupRecyclerView();

        }

        void SetupRecyclerView()
        {
            recyclerView.SetLayoutManager(new LinearLayoutManager(this));
            recyclerAdapter = new RecyclerAdapter(GetContact());
            recyclerView.SetAdapter(recyclerAdapter);
        }

        private List<TransactionModel> GetContact()
        {
            List<TransactionModel> contacts = new List<TransactionModel>();
            contacts.Add(new TransactionModel() { Phone = "08132613744", Amount = 5000 , Date = "12/07/22"});
            contacts.Add(new TransactionModel() { Phone = "07012345678", Amount = 4000, Date = "01/02/09" });
            contacts.Add(new TransactionModel() { Phone = "08132613744", Amount = 5000, Date = "12/07/22" });
            contacts.Add(new TransactionModel() { Phone = "07012345678", Amount = 4000, Date = "01/02/09" });
            contacts.Add(new TransactionModel() { Phone = "08132613744", Amount = 5000, Date = "12/07/22" });
            contacts.Add(new TransactionModel() { Phone = "07012345678", Amount = 4000, Date = "01/02/09" });
            contacts.Add(new TransactionModel() { Phone = "08132613744", Amount = 2000, Date = "12/07/22" });
            contacts.Add(new TransactionModel() { Phone = "07012345678", Amount = 4000, Date = "01/02/09" });
            contacts.Add(new TransactionModel() { Phone = "08132613744", Amount = 5000, Date = "12/07/22" });
            contacts.Add(new TransactionModel() { Phone = "07012345678", Amount = 1000, Date = "01/02/09" });
            contacts.Add(new TransactionModel() { Phone = "08132613744", Amount = 50500, Date = "12/07/22" });
            contacts.Add(new TransactionModel() { Phone = "07012345678", Amount = 4000, Date = "01/02/09" });
            contacts.Add(new TransactionModel() { Phone = "08132613744", Amount = 5000, Date = "12/07/22" });
            contacts.Add(new TransactionModel() { Phone = "07012345678", Amount = 4000, Date = "01/02/09" });
            return contacts;
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