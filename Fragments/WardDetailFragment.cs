using ALAT_Lite.Activities;
using ALAT_Lite.Classes;
using Android;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using Google.Android.Material.Button;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALAT_Lite.Fragments
{
    [Obsolete]
    public class WardDetailFragment : Fragment
    {
        Spinner spinner;
        AppCompatButton btnDOB;
        MaterialButton btnNext;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.WardFragLayout,container,false);
            
            spinner = view.FindViewById<Spinner>(Resource.Id.spinner3);
            btnDOB = view.FindViewById<AppCompatButton>(Resource.Id.btnDOB);
            btnNext = view.FindViewById<MaterialButton>(Resource.Id.btnNext);

            ArrayAdapter adapter;
            adapter = ArrayAdapter.CreateFromResource(Activity, Resource.Array.Gender, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            btnDOB.Click += BtnDOB_Click;
            btnNext.Click += BtnNext_Click;

            return view;
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            var trans = Activity.FragmentManager.BeginTransaction().Replace(Resource.Id.frameLayout1, new GuardDetFrag());
            trans.AddToBackStack(null);
            trans.Commit();
        }

      

        private void BtnDOB_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                btnDOB.Text = time.ToShortDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        
    }
}