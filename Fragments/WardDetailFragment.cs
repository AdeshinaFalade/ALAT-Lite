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
        ImageView imgAttachBirthCert, imgAttachPassport;
        AppCompatButton btnDOB;
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
            View view = inflater.Inflate(Resource.Layout.WardFragLayout,container,false);
            

            spinner = view.FindViewById<Spinner>(Resource.Id.spinner3);
            btnDOB = view.FindViewById<AppCompatButton>(Resource.Id.btnDOB);
            imgAttachPassport = view.FindViewById<ImageView>(Resource.Id.imgAttachPassport);
            imgAttachBirthCert = view.FindViewById<ImageView>(Resource.Id.imgAttachBirthCert);

            ArrayAdapter adapter;
            adapter = ArrayAdapter.CreateFromResource(Activity, Resource.Array.Gender, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            btnDOB.Click += BtnDOB_Click;
            imgAttachBirthCert.Click += ImgAttachBirthCert_Click;
            imgAttachPassport.Click += ImgAttachPassport_Click;

            return view;
        }

        private void ImgAttachPassport_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder passportAlert = new AlertDialog.Builder(Activity);
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

            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
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
                Toast.MakeText(Activity, "Upload not supported", ToastLength.Short).Show();
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
            AlertDialog.Builder passportAlert = new AlertDialog.Builder(Activity);
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

    }
}