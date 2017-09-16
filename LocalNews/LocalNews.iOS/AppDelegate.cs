using Foundation;
using JetBrains.Annotations;
using LocalNews.Ioc;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace LocalNews.iOS
{
    [Register("AppDelegate")]
	[UsedImplicitly]
	public partial class AppDelegate : FormsApplicationDelegate
    {
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
            IocConfiguration.Configure();
			LoadApplication(new App());

            return base.FinishedLaunching(app, options);
		}
	}
}
