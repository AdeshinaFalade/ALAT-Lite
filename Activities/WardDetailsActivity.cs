using ALAT_Lite.Fragments;
using Android;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActionBar = AndroidX.AppCompat.App.ActionBar;
using AlertDialog = AndroidX.AppCompat.App.AlertDialog;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace ALAT_Lite.Activities
{
    [Activity(Label = "WardDetailsActivity")]
    public class WardDetailsActivity : AppCompatActivity
    {
        Toolbar toolbar;
        FrameLayout frameLayout;
        //Spinner spinner;
        //ImageView imgAttachBirthCert, imgAttachPassport;
        //AppCompatButton btnDOB;
        //readonly string[] permissionGroup =
        //{
        //    Manifest.Permission.ReadExternalStorage,
        //    Manifest.Permission.WriteExternalStorage,
        //    Manifest.Permission.Camera
        //};
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.WardDetailsLayout);
            toolbar = FindViewById<Toolbar>(Resource.Id.wardDetToolbar);
            //frameLayout = FindViewById<FrameLayout>(Resource.Id.frameLayout1);
            //spinner = FindViewById<Spinner>(Resource.Id.spinner3);
            //btnDOB = FindViewById<AppCompatButton>(Resource.Id.btnDOB);
            //imgAttachPassport = FindViewById<ImageView>(Resource.Id.imgAttachPassport);
            //imgAttachBirthCert = FindViewById<ImageView>(Resource.Id.imgAttachBirthCert);

            //setup toolbar

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Create Account";
            ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.black_arrow);
            actionBar.SetDisplayHomeAsUpEnabled(true);

            var trans = FragmentManager.BeginTransaction();
            trans.Add(Resource.Id.frameLayout1, new WardDetailFragment(), "Fragment");
            trans.Commit();





            //spinner setup
            //ArrayAdapter adapter;
            //adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.Gender, Android.Resource.Layout.SimpleSpinnerItem);
            //adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            //spinner.Adapter = adapter;

            //btnDOB.Click += BtnDOB_Click;
            //imgAttachBirthCert.Click += ImgAttachBirthCert_Click;
            //imgAttachPassport.Click += ImgAttachPassport_Click; 
        }
        /**
        private void ImgAttachPassport_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder passportAlert = new AlertDialog.Builder(this);
            passportAlert.SetMessage("Upload Passport");
            passportAlert.SetNegativeButton("Take Photo", (sender, e) =>
                {
                    //capture image
                    TakePhoto(imgAttachPassport);
                });

            passportAlert.SetPositiveButton("Upload Photo", (sender, e) =>
            {
                //select image
                SelectPhoto(imgAttachPassport);
            });
            passportAlert.Show();
        }


        async void TakePhoto(ImageView imageView)
        {
            await CrossMedia.Current.Initialize();
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                CompressionQuality = 20,
                Directory = "Sample",
                Name = GenerateRandomString(6) + "alat.jpg"
            });

            if (file == null)
            {
                return;
            }


            //converts file to byte array and set resulting bitmap to imageview
            byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);

            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray,0, imageArray.Length);
            imageView.SetImageBitmap(bitmap);

        }

        string GenerateRandomString(int length)
        {
            Random rand = new Random();
            char[] allowChars = "QWERTYUIOPLKJHGFDSAZXCVBNMmnbvcxzasdfghjklpoiuytrewq0987654321".ToCharArray();
            string sResult = "";
            for (int i = 0; i < length; i++)
            {
                sResult += allowChars[rand.Next(0, allowChars.Length)];
            }
            return sResult;
        }

        async void SelectPhoto(ImageView imageView)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                Toast.MakeText(this, "Upload not supported", ToastLength.Short).Show();
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                CompressionQuality = 30
            });

            if (file == null)
            {
                return;
            }
            //converts file to byte array and set resulting bitmap to imageview
            byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);

            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            imageView.SetImageBitmap(bitmap);

        }

        private void ImgAttachBirthCert_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder passportAlert = new AlertDialog.Builder(this);
            passportAlert.SetMessage("Upload Birth Certificate");
            passportAlert.SetNegativeButton("Take Photo", (sender, e) =>
            {
                //capture image
                TakePhoto(imgAttachBirthCert);
            });

            passportAlert.SetPositiveButton("Upload Photo", (sender, e) =>
            {
                //select image
                SelectPhoto(imgAttachBirthCert);
            });
            passportAlert.Show();

        }

        private void BtnDOB_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                btnDOB.Text = time.ToShortDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }
        **/

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

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}