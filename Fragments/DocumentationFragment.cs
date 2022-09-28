using ALAT_Lite.Classes;
using Android;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Google.Android.Material.Button;
using Microsoft.WindowsAzure.Storage;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ALAT_Lite.Fragments
{
    [Obsolete]
    public class DocumentationFragment : Fragment
    {
        public Bitmap bitmap;
        View view;
        private Android.Net.Uri filePath;
        public List<string> links = new List<string>();
        private const int PICK_IMAGE_REQUSET = 71;
        private const int TAKE_IMAGE_REQUSET = 0;
        ProgressClass progress = new ProgressClass();
        readonly string[] permissionGroup =
        {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.Camera
        };
        private MemoryStream inputStream;
        public string URL { get; private set; }
        MaterialButton btnSubmit;
        ImageView imgAttachWardPassport, imgAttachBirthCert, imgAttachID, imgAttachGuardPassport;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.DocumentationLayout, container, false);

            imgAttachBirthCert = (ImageView)view.FindViewById(Resource.Id.imgAttachBirthCert);
            imgAttachGuardPassport = (ImageView)view.FindViewById(Resource.Id.imgAttachGuardPassport);
            imgAttachID = (ImageView)view.FindViewById(Resource.Id.imgAttachID);
            imgAttachWardPassport = (ImageView)view.FindViewById(Resource.Id.imgAttachWardPassport);
            btnSubmit = view.FindViewById<MaterialButton>(Resource.Id.btnSubmit);

            btnSubmit.Click += BtnSubmit_Click;
            imgAttachBirthCert.Click += ImgAttachBirthCert_Click;
            imgAttachGuardPassport.Click += ImgAttachGuardPassport_Click;
            imgAttachID.Click += ImgAttachID_Click;
            imgAttachWardPassport.Click += ImgAttachWardPassport_Click; 


            return view;
        }

        private void ImgAttachWardPassport_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder passportAlert = new AlertDialog.Builder(Activity);
            passportAlert.SetMessage("Upload Ward Passport");
            passportAlert.SetNegativeButton("Take Picture", (sender, e) =>
            {
                //capture image
                TakePhoto(imgAttachWardPassport);
            });

            passportAlert.SetPositiveButton("Upload Picture", (sender, e) =>
            {
                //select image
                SelectPhoto(imgAttachWardPassport);
            });
            passportAlert.Show();
        }

        private void ImgAttachID_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder passportAlert = new AlertDialog.Builder(Activity);
            passportAlert.SetMessage("Upload ID");
            passportAlert.SetNegativeButton("Take Picture", (sender, e) =>
            {
                //capture image
                TakePhoto(imgAttachID);
            });

            passportAlert.SetPositiveButton("Upload Picture", (sender, e) =>
            {
                //select image
                SelectPhoto(imgAttachID);
            });
            passportAlert.Show();
        }

        private void ImgAttachGuardPassport_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder passportAlert = new AlertDialog.Builder(Activity);
            passportAlert.SetMessage("Upload Your Passport");
            passportAlert.SetNegativeButton("Take Picture", (sender, e) =>
            {
                //capture image
                TakePhoto(imgAttachGuardPassport);
            });

            passportAlert.SetPositiveButton("Upload Picture", (sender, e) =>
            {
                //select image
                SelectPhoto(imgAttachGuardPassport);
            });
            passportAlert.Show();
        }

        private void ImgAttachBirthCert_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder passportAlert = new AlertDialog.Builder(Activity);
            passportAlert.SetMessage("Upload Birth Certificate");
            passportAlert.SetNegativeButton("Take Picture", (sender, e) =>
            {
                //capture image
                TakePhoto(imgAttachBirthCert);
            });

            passportAlert.SetPositiveButton("Upload Picture", (sender, e) =>
            {
                //select image
                SelectPhoto(imgAttachBirthCert);

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


       

        private void UploadImage()
        {
            if (inputStream != null)
            {
                Upload(inputStream);
            }
        }

       
        

        //Upload to blob function  
        private async void Upload(Stream stream)
        {
            try
            {
                var account = CloudStorageAccount.Parse("BlobEndpoint=https://bitproject.blob.core.windows.net/;QueueEndpoint=https://bitproject.queue.core.windows.net/;FileEndpoint=https://bitproject.file.core.windows.net/;TableEndpoint=https://bitproject.table.core.windows.net/;SharedAccessSignature=sv=2021-06-08&ss=bfqt&srt=sco&sp=rwdlacupiytfx&se=2022-10-07T17:59:44Z&st=2022-09-28T09:59:44Z&spr=https,http&sig=fFmGMBq%2FGwrp6Xcs%2FM8%2FDEOqJWJ7CAsFRZF3A25vQg0%3D");
                var client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference("imageblob");
                await container.CreateIfNotExistsAsync();
                var name = Guid.NewGuid().ToString();
                var blockBlob = container.GetBlockBlobReference($"{name}.png");
                await blockBlob.UploadFromStreamAsync(stream);
                URL = blockBlob.Uri.OriginalString;
                links.Add(URL);

                Toast.MakeText(Activity, "Image uploaded Successfully!", ToastLength.Short).Show();

            }
            catch (Exception e)
            {
                Toast.MakeText(Activity, "exception" + e.ToString(), ToastLength.Short);
            }
            
        }


        async public void TakePhoto(ImageView imageView)
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
            bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            imageView.SetImageBitmap(bitmap);
            byte[] bitmapData;
            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                bitmapData = stream.ToArray();
            }
            inputStream = new MemoryStream(bitmapData);
            UploadImage();

        }

        public string GenerateRandomString(int length)
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

        async public void SelectPhoto(ImageView imageView)
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
            byte[] bitmapData;
            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                bitmapData = stream.ToArray();
            }
            inputStream = new MemoryStream(bitmapData);
            UploadImage();

        }
    }
}