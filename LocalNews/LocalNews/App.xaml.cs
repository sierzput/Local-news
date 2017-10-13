using LocalNews.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Ioc;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LocalNews
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SetMainPage();
        }

        private void SetMainPage()
        {
            Current.MainPage = new TabbedPage
            {
                Children =
                {
                    new NavigationPage(Resolver.Resolve<ItemsPage>())
                    {
                        Title = "Browse",
                        Icon = Device.OnPlatform("tab_feed.png", null, null)
                    },
                    new NavigationPage(Resolver.Resolve<AboutPage>())
                    {
                        Title = "About",
                        Icon = Device.OnPlatform("tab_about.png", null, null)
                    },
                }
            };
        }
    }
}
