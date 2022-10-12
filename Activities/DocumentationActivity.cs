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
using Google.Android.Material.Button;
using Microsoft.WindowsAzure.Storage;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ActionBar = AndroidX.AppCompat.App.ActionBar;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;
using AlertDialog = AndroidX.AppCompat.App.AlertDialog;
using Google.Android.Material.Snackbar;
using Xamarin.Essentials;
using ALAT_Lite.Classes;
using Newtonsoft.Json;

namespace ALAT_Lite.Activities
{
    [Activity(Label = "DocumentationActivity")]
    public class DocumentationActivity : AppCompatActivity
    {
        Toolbar toolbar;
        LinearLayout linearLayoutDoc;
        public Bitmap bitmap;
        public static int wardId;
        public static RegisterWardModel registerWard = new RegisterWardModel();
        public static string token;
        public List<string> links = new List<string>();
        public ProgressFragment progressDialog;
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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here 
            SetContentView(Resource.Layout.DocumentationLayout);
            imgAttachBirthCert = (ImageView)FindViewById(Resource.Id.imgAttachBirthCert);
            imgAttachGuardPassport = (ImageView)FindViewById(Resource.Id.imgAttachGuardPassport);
            imgAttachID = (ImageView)FindViewById(Resource.Id.imgAttachID);
            imgAttachWardPassport = (ImageView)FindViewById(Resource.Id.imgAttachWardPassport);
            btnSubmit = FindViewById<MaterialButton>(Resource.Id.btnSubmit);
            toolbar = FindViewById<Toolbar>(Resource.Id.documentationToolbar);
            linearLayoutDoc = FindViewById<LinearLayout>(Resource.Id.linearLayoutDoc);

            //setup toolbar

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Documentation";
            ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.black_arrow);
            actionBar.SetDisplayHomeAsUpEnabled(true);

            wardId = Preferences.Get("wardId", 0);
            token = Preferences.Get("token", "");

            



            btnSubmit.Click += BtnSubmit_Click;
            imgAttachBirthCert.Click += ImgAttachBirthCert_Click;
            imgAttachGuardPassport.Click += ImgAttachGuardPassport_Click;
            imgAttachID.Click += ImgAttachID_Click;
            imgAttachWardPassport.Click += ImgAttachWardPassport_Click;
        }
        private void ImgAttachWardPassport_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder passportAlert = new AlertDialog.Builder(this);
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
            AlertDialog.Builder passportAlert = new AlertDialog.Builder(this);
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
            AlertDialog.Builder passportAlert = new AlertDialog.Builder(this);
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
            AlertDialog.Builder passportAlert = new AlertDialog.Builder(this);
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
            if (links.Count < 4)
            {
                Toast.MakeText(this, "Upload all the required documents", ToastLength.Short).Show();
                return;
            }

            Setup();

            CreateWard(registerWard);

        }

        void Setup()
        {
            int userId = Preferences.Get("userId", 0);
            var wardFirstName = Preferences.Get("WardFirstName", "");
            var wardLastName = Preferences.Get("WardLastName", "");
            var wardMiddleName = Preferences.Get("WardMiddleName", "");
            var wardEmail = Preferences.Get("WardEmail", "");
            var wardGender = Preferences.Get("WardGender", "");
            var wardDOB = Preferences.Get("WardDOB", "");
            var firstName = Preferences.Get("firstName", "");
            var lastName = Preferences.Get("lastName", "");
            var guardMail = Preferences.Get("email", "");
            var bvn = Preferences.Get("bvn", "");
            var phone = Preferences.Get("phone", "");
            var address = Preferences.Get("address", "");
            registerWard.firstName = wardFirstName;
            registerWard.lastName = wardLastName;
            registerWard.middleName = wardMiddleName;
            registerWard.emailAddress = wardEmail;
            registerWard.gender = wardGender;
            registerWard.dateOfBirth = wardDOB;
            registerWard.address = address;
            registerWard.guardianId = userId;
            registerWard.phoneNumber = phone;
            registerWard.birthCertUrl = links[1];
            registerWard.idUrl = links[2];
            registerWard.wardPassportUrl = links[0];
            registerWard.guardianPassportUrl = links[3];
        }

        public async void CreateWard(RegisterWardModel model)
        {
            string result = string.Empty;
            try
            {
                ShowProgressDialog("Submitting");
                var rawString = JsonConvert.SerializeObject(model);
                result = await NetworkUtils.PostUserData("Guardian/GuardianRegisterWardNew", rawString, token);
                if (!string.IsNullOrEmpty(result) && result == "Ward Account Created! Update Your Documents")
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
                Toast.MakeText(this, e.Message, ToastLength.Short).Show();
            }


        }

        public async void UploadDocuments(int id, UploadDocsModel model)
        {
            string result = string.Empty;
            try
            {
                ShowProgressDialog("Submitting");
                var rawString = JsonConvert.SerializeObject(model);
                result = await NetworkUtils.PostUserData($"Guardian/UploadDocument?Wardid={id}", rawString, token);
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
                Toast.MakeText(this, e.Message, ToastLength.Short).Show();
            }


        }


        void ShowAlert()
        {
            var alertFrag = new DocUploadeAlertFragment();
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
            ShowProgressDialog("Uploading");
            try
            {

                var account = CloudStorageAccount.Parse("BlobEndpoint=https://bitproject.blob.core.windows.net/;QueueEndpoint=https://bitproject.queue.core.windows.net/;FileEndpoint=https://bitproject.file.core.windows.net/;TableEndpoint=https://bitproject.table.core.windows.net/;SharedAccessSignature=sv=2021-06-08&ss=b&srt=sco&sp=rwdlaciytfx&se=2022-10-31T03:41:56Z&st=2022-10-07T19:41:56Z&spr=https,http&sig=PIcKUoF26sAYjiB5A702oOomWfMDyIY2MJ8b0OkZ5Ls%3D");
                var client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference("user-" + wardId.ToString());
                await container.CreateIfNotExistsAsync(Microsoft.WindowsAzure.Storage.Blob.BlobContainerPublicAccessType.Container, default,default);
                var name = Guid.NewGuid().ToString();
                var blockBlob = container.GetBlockBlobReference($"{name}.png");
                await blockBlob.UploadFromStreamAsync(stream);
                URL = blockBlob.Uri.OriginalString;
                links.Add(URL);
                Snackbar.Make(linearLayoutDoc, "Image uploaded Successfully!", Snackbar.LengthShort).Show();
                CloseProgressDialog();
            }
                
            catch (Exception e)
            {
                Snackbar.Make(linearLayoutDoc, "Error: " + e.Message, Snackbar.LengthShort).Show();
                CloseProgressDialog();
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
            byte[] bitmapData;
            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                bitmapData = stream.ToArray();
            }
            inputStream = new MemoryStream(bitmapData);

            UploadImage();
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
    }
}