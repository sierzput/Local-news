using Android.App;
using Android.Content.PM;
using Android.OS;
using JetBrains.Annotations;
using LocalNews.Ioc;
using Ninject;
using Xamarin.Forms.Platform.Android;
using XLabs.Forms;
using XLabs.Ioc;
using XLabs.Ioc.Ninject;

namespace LocalNews.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [UsedImplicitly]
    public class MainActivity : XFormsApplicationDroid
    {
        protected override void OnCreate(Bundle bundle)
        {
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.Tabbar;
            FormsAppCompatActivity.ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            SetIoc();

            LoadApplication(new App());
        }

        private void SetIoc()
        {
            var standardKernel = new StandardKernel();
            var resolverContainer = new NinjectContainer(standardKernel);

            standardKernel.Load(new IocModule());

            Resolver.SetResolver(resolverContainer.GetResolver());
        }
    }
}