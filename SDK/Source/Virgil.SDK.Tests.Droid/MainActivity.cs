using System.IO;
using System.Reflection;
using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using NUnit.Runner;
using NUnit.Runner.Services;
using PCLAppConfig;
using Virgil.SDK.Tests;

namespace AndroidUnitTestApp
{
    [Activity(Label = "NUnit 3", MainLauncher = true, Theme = "@android:style/Theme.Holo.Light", 
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);


            ConfigurationManager.Initialise(PCLAppConfig.FileSystemStream.PortableStream.Current);
            var AppId = ConfigurationManager.AppSettings["virgil:AppID"];
           // var AppId = ConfigurationManager.AppSettings["virgil:AppID"];
            using (StreamReader sr = new StreamReader(Application.Assets.Open("crypto_compatibility_data.json")))
            {
                var content = sr.ReadToEnd();
                AppSettings.CryptoCompatibilityData = content;
            }

            // This will load all tests within the current project
            var nunit = new NUnit.Runner.App();

            // If you want to add tests in another assembly
            //nunit.AddTestAssembly(typeof(MyTests).Assembly);

            // Do you want to automatically run tests when the app starts?
            nunit.Options = new TestOptions
            {
                AutoRun = true
            };

            LoadApplication(nunit);
        }
    }
}
