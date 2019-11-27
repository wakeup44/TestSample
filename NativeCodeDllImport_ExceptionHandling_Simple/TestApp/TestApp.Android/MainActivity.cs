using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using System.Threading.Tasks;

namespace TestApp.Droid
{
    [Activity(Label = "TestApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            #region ExceptionHandling
            // platform extension hook
            // provides more detailed informatin compared to AppDomain.CurrentDomain.UnhandledException
            // specific for Android
            AndroidEnvironment.UnhandledExceptionRaiser += (sender, e) =>
            {
                try
                {
                    var exception = e.Exception.GetBaseException();
                    System.Diagnostics.Debug.WriteLine("UnhandledExceptionRaiser: " + exception);
                }
                catch
                {
                    throw;
                }
            };

            // specific for Android and iOS
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                try
                {
                    var exception = e.Exception.GetBaseException();
                    System.Diagnostics.Debug.WriteLine("UnhandledException: " + exception);
                }
                catch
                {
                    throw;
                }
            };

            // .NET Standard build in event hook, handles last chance error 
            // specific for Android and iOS
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                try
                {
                    var exception = ((Exception)e.ExceptionObject).GetBaseException();
                    System.Diagnostics.Debug.WriteLine("UnhandledException: " + exception);
                }
                catch
                {
                    throw;
                }
            };
            #endregion

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            // check if library mathods are available
            var str = NativeWrapper.ReturnInt().ToString("X");
            System.Diagnostics.Debug.WriteLine(str);

            // now, lets see what happens when native code throws an exception
            NativeWrapper.ThrowAnException();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}