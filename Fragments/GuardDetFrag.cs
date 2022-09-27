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
        ImageView imgAttachGuardPassport, imgAttachID;
        MaterialButton btnSubmit;
        WardDetailFragment wardDetailFragment = new WardDetailFragment();
        readonly string[] permissionGroup =
        {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.Camera
        };

        
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


            btnSubmit = view.FindViewById<MaterialButton>(Resource.Id.btnSubmit);
            imgAttachGuardPassport = view.FindViewById<ImageView>(Resource.Id.imgAttachGuardPassport);
            imgAttachID = view.FindViewById<ImageView>(Resource.Id.imgAttachID);

            btnSubmit.Click += BtnSubmit_Click;
            imgAttachGuardPassport.Click += ImgAttachGuardPassport_Click;
            imgAttachID.Click += ImgAttachID_Click; 

            return view;
        }

        private void ImgAttachID_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder passportAlert = new AlertDialog.Builder(Activity);
            passportAlert.SetMessage("Upload ID");
            passportAlert.SetNegativeButton("Take Photo", (sender, e) =>
            {
                //capture image
                wardDetailFragment.TakePhoto(imgAttachID);
            });

            passportAlert.SetPositiveButton("Upload Photo", (sender, e) =>
            {
                //select image
                wardDetailFragment.SelectPhoto(imgAttachID);
            });
            passportAlert.Show();
        }

        private void ImgAttachGuardPassport_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder passportAlert = new AlertDialog.Builder(Activity);
            passportAlert.SetMessage("Upload Your Passport");
            passportAlert.SetNegativeButton("Take Photo", (sender, e) =>
            {
                //capture image
                wardDetailFragment.TakePhoto(imgAttachGuardPassport);
            });

            passportAlert.SetPositiveButton("Upload Photo", (sender, e) =>
            {
                //select image
                wardDetailFragment.SelectPhoto(imgAttachGuardPassport);
            });
            passportAlert.Show();
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            ShowAlert();
        }
        void ShowAlert()
        {
            var alertFrag = new AcctCreatedAlertFrag();
            var trans = FragmentManager.BeginTransaction();
            alertFrag.Show(trans, "Dialog");
        }
       
    }
}