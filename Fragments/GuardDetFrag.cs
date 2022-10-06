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
using Google.Android.Material.Button;
using Newtonsoft.Json;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;

namespace ALAT_Lite.Fragments
{
    [Obsolete]
    public class GuardDetFrag : Fragment
    {
        public static RegisterWardModel registerWard = new RegisterWardModel();
        public static string token;
        public ProgressFragment progressDialog;
        MaterialButton btnSubmit;
        EditText edtGuardFirstName, edtGuardLastName, edtGuardEmail, edtGuardMiddleName, edtPhone, edtAddress;



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
            edtPhone = view.FindViewById<EditText>(Resource.Id.edtBVN);
            edtAddress = view.FindViewById<EditText>(Resource.Id.edtAddress);

            token = Preferences.Get("token", "");
            int userId = Preferences.Get("userId", 0);
            var wardFirstName = Preferences.Get("WardFirstName", "");
            var wardLastName = Preferences.Get("WardLastName", "");
            var wardMiddleName = Preferences.Get("WardMiddleName", "");
            var wardEmail = Preferences.Get("WardEmail", "");
            var wardGender = Preferences.Get("WardGender", "");
            var wardDOB = Preferences.Get("WardDOB", "");

            registerWard.firstName = wardFirstName;
            registerWard.lastName = wardLastName;
            registerWard.middleName = wardMiddleName;
            registerWard.emailAddress = wardEmail;
            registerWard.gender = wardGender;
            registerWard.dateOfBirth = wardDOB;
            registerWard.address = edtAddress.Text;
            registerWard.guardianId = userId;
            registerWard.phoneNumber = edtPhone.Text;


            btnSubmit.Click += BtnSubmit_Click;
          

            return view;
        }

     

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            var firstName = edtGuardFirstName.Text;
            var lastName = edtGuardLastName.Text;
            var mail = edtGuardEmail.Text;
            var phone = edtPhone.Text;
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
            else if (string.IsNullOrEmpty(phone))
            {
                Toast.MakeText(Activity, "Phone number is required", ToastLength.Short).Show();
                return;
            }
            else if (phone.Length != 11)
            {
                Toast.MakeText(Activity, "Invalid phone number", ToastLength.Short).Show();
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

            CreateWard(registerWard);
        }

        public async void CreateWard(RegisterWardModel model)
        {
            string result = string.Empty;
            try
            {
                ShowProgressDialog("Submitting");
                var rawString = JsonConvert.SerializeObject(model);
                result = await NetworkUtils.PostUserData("Guardian/GuardianRegisterWard", rawString, token);
                if (!string.IsNullOrEmpty(result))
                {
                    CloseProgressDialog();

                    ShowAlert();
                }
                else
                {
                    CloseProgressDialog();
                }
            }
            catch (Exception e)
            {
                CloseProgressDialog();
                    Toast.MakeText(Activity, e.Message, ToastLength.Short).Show();
            }


        }
        void ShowAlert()
        {
            var alertFrag = new AcctCreatedAlertFrag();
            var trans = FragmentManager.BeginTransaction();
            alertFrag.Show(trans, "Dialog");
        }

        public void ShowProgressDialog(string status)
        {
            progressDialog = new ProgressFragment(status);
            var trans = FragmentManager.BeginTransaction();
            progressDialog.Cancelable = false;
            progressDialog.Show(trans, "progress");

        }

        public void CloseProgressDialog()
        {
            if (progressDialog != null)
            {
                progressDialog.Dismiss();
                progressDialog = null;
            }
        }

    }
}