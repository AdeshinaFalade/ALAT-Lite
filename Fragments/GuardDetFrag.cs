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
        EditText edtGuardFirstName, edtGuardLastName, edtGuardEmail, edtGuardMiddleName, edtBVN, edtAddress;



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
            edtGuardFirstName = view.FindViewById<EditText>(Resource.Id.edtGuardFirstName);
            edtGuardLastName = view.FindViewById<EditText>(Resource.Id.edtGuardLastName);
            edtGuardEmail = view.FindViewById<EditText>(Resource.Id.edtGuardEmail);
            edtGuardMiddleName = view.FindViewById<EditText>(Resource.Id.edtGuardMiddleName);
            edtBVN = view.FindViewById<EditText>(Resource.Id.edtBVN);
            edtAddress = view.FindViewById<EditText>(Resource.Id.edtAddress);

            btnSubmit.Click += BtnSubmit_Click;
          

            return view;
        }

     

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            var firstName = edtGuardFirstName.Text;
            var lastName = edtGuardLastName.Text;
            var mail = edtGuardEmail.Text;
            var bvn = edtBVN.Text;
            var address = edtAddress.Text;

            if (string.IsNullOrEmpty(firstName))
            {
                Toast.MakeText(Activity, "First name is required", ToastLength.Short).Show();
                return;
            }
            else if (firstName.Length < 2)
            {
                Toast.MakeText(Activity, "Invalid first name", ToastLength.Short).Show();
                return;
            }
            else if (string.IsNullOrEmpty(lastName))
            {
                Toast.MakeText(Activity, "Last name is required", ToastLength.Short).Show();
                return;
            }
            else if (lastName.Length < 2)
            {
                Toast.MakeText(Activity, "Invalid last name", ToastLength.Short).Show();
                return;
            }
            else if (string.IsNullOrEmpty(bvn))
            {
                Toast.MakeText(Activity, "BVN is required", ToastLength.Short).Show();
                return;
            }
            else if (bvn.Length != 11)
            {
                Toast.MakeText(Activity, "Invalid BVN", ToastLength.Short).Show();
                return;
            }
            else if (string.IsNullOrEmpty(address))
            {
                Toast.MakeText(Activity, "Address is required", ToastLength.Short).Show();
                return;
            }
            else if (address.Length < 5)
            {
                Toast.MakeText(Activity, "Invalid address", ToastLength.Short).Show();
                return;
            }
            else if (string.IsNullOrEmpty(mail))
            {
                Toast.MakeText(Activity, "Email is required", ToastLength.Short).Show();
                return;
            }
            else if (!Android.Util.Patterns.EmailAddress.Matcher(mail).Matches())
            {
                Toast.MakeText(Activity, "Invalid Email Address", ToastLength.Short).Show();
                return;
            }
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