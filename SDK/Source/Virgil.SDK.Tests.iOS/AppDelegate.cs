using System;
using System.Linq;
using System.Collections.Generic;

using Foundation;
using UIKit;
using NUnit.Runner.Services;
using PCLAppConfig;
using System.IO;
using System.Reflection;
using NUnit.Runner;

namespace Virgil.SDK.Tests.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            // This will load all tests within the current project
            var nunit = new NUnit.Runner.App();

            // If you want to add tests in another assembly
            ConfigurationManager.Initialise(PCLAppConfig.FileSystemStream.PortableStream.Current);
            var text = System.IO.File.ReadAllText("crypto_compatibility_data.json");
            AppSettings.CryptoCompatibilityData = text;
            // Do you want to automatically run tests when the app starts?
            nunit.Options = new TestOptions
            {
                AutoRun = true
            };

            LoadApplication(nunit);

            return base.FinishedLaunching(app, options);
        }
    }
}
