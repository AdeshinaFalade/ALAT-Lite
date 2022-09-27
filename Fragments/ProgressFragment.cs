using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALAT_Lite.Fragments
{
    [Obsolete]
    public class ProgressFragment : DialogFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        string thisStatus;
        public ProgressFragment(string thisStatus)
        {
            this.thisStatus = thisStatus;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.ProgressLayout, container, false);
            TextView txtProgress = view.FindViewById<TextView>(Resource.Id.txtProgress);
            txtProgress.Text = thisStatus;
            return view;
            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}