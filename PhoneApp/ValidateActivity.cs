using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PhoneApp
{
    [Activity(Label = "@string/ValidateActivity")]
    public class ValidateActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ValidateActivity);

            var ValidateButton = FindViewById<Button>(Resource.Id.ValidateButton);

            ValidateButton.Click += (object sender, System.EventArgs e) =>
            {
                Validate();
            };
        }

        private async void Validate()
        {
            
            var ServiceClient = new SALLab06.ServiceClient();

            var EmailAddressText = FindViewById<EditText>(Resource.Id.EmailAddressText);
            var PasswordText = FindViewById<EditText>(Resource.Id.PasswordText);

            string StudentEmail = EmailAddressText.Text;
            string Password = PasswordText.Text;

            string myDevice = Android.Provider.Settings.Secure.GetString(
                ContentResolver, Android.Provider.Settings.Secure.AndroidId);

            var Result = await ServiceClient.ValidateAsync(
                StudentEmail, Password, myDevice);

            var ValidateText = FindViewById<TextView>(Resource.Id.ValidateText);

            ValidateText.Text = $"{Result.Status}\n{Result.Fullname}\n{Result.Token}";
        }
    }
}