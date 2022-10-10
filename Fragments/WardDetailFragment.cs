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
using Xamarin.Essentials;

namespace ALAT_Lite.Fragments
{
    [Obsolete]
    public class WardDetailFragment : Fragment
    {
        Spinner spinner;
        AppCompatButton btnDOB;
        MaterialButton btnNext;
        EditText edtWardFirstName, edtWardLastName, edtWardMiddleName, edtWardEmail, edtPhone;
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
            edtWardEmail = view.FindViewById<EditText>(Resource.Id.edtWardEmail);
            edtWardFirstName = view.FindViewById<EditText>(Resource.Id.edtWardFirstName);
            edtWardLastName = view.FindViewById<EditText>(Resource.Id.edtWardlastName);
            edtWardMiddleName = view.FindViewById<EditText>(Resource.Id.edtWardMiddleName);
            edtPhone = view.FindViewById<EditText>(Resource.Id.edtPhone);

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
            var firstName = edtWardFirstName.Text;
            var lastName = edtWardLastName.Text;
            var mail = edtWardEmail.Text;
            var dob = btnDOB.Text;

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
            else if (string.IsNullOrEmpty(dob))
            {
                Toast.MakeText(Activity, "Date of birth is required", ToastLength.Short).Show();
                return;
            }
            else if (DateTime.Compare(DateTime.Parse(dob), DateTime.Now) >= 0)
            {
                Toast.MakeText(Activity, "Invalid date", ToastLength.Short).Show();
                return;
            }
            else if (string.IsNullOrEmpty(edtPhone.Text))
            {
                Toast.MakeText(Activity, "Phone number is required", ToastLength.Short).Show();
                return;
            }
            else if (edtPhone.Text.Length != 11)
            {
                Toast.MakeText(Activity, "Invalid Phone number", ToastLength.Short).Show();
                return;
            }
            else if (string.IsNullOrEmpty(mail))
            {
                Toast.MakeText(Activity, "Email is required", ToastLength.Short).Show();
                return;
            }
            else if (!Patterns.EmailAddress.Matcher(mail).Matches())
            {
                Toast.MakeText(Activity, "Invalid Email Address", ToastLength.Short).Show();
                return;
            }

            Preferences.Set("WardFirstName", edtWardFirstName.Text);
            Preferences.Set("WardLastName", edtWardLastName.Text);
            Preferences.Set("WardMiddleName", edtWardMiddleName.Text);
            Preferences.Set("WardEmail", edtWardEmail.Text);
            Preferences.Set("Phone", edtPhone.Text);
            Preferences.Set("WardGender", spinner.SelectedItem.ToString());
            Preferences.Set("WardDOB", DateTime.Parse(btnDOB.Text).ToString(@"yyy-MM-dd"));

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