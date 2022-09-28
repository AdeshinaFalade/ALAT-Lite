using Android;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Google.Android.Material.Button;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALAT_Lite.Fragments
{
    [Obsolete]
    public class GuardDetFrag : Fragment
    {
        
        MaterialButton btnSubmit;
      

        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.GuardDetailsFragLayout, container, false);


            btnSubmit = view.FindViewById<MaterialButton>(Resource.Id.btnNextPage);

            btnSubmit.Click += BtnSubmit_Click;
          

            return view;
        }

     

        private void BtnSubmit_Click(object sender, EventArgs e)
        {

            var trans = Activity.FragmentManager.BeginTransaction().Replace(Resource.Id.frameLayout1, new DocumentationFragment());
            trans.AddToBackStack(null);
            trans.Commit();
        }
       
       
    }
}