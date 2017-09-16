using LocalNews.Models;
using LocalNews.Services;
using LocalNews.ViewModels;
using LocalNews.Views;

namespace LocalNews.Ioc
{
    public class IocModule : BaseIocModule
    {
        public override void Load()
        {
            RegisterTransient<INewsDownloader, NewsDownloader>();
            RegisterTransient<IDataStore<NewsListItem>, NewsService>();
            RegisterTransient<ItemsViewModel>();
            RegisterTransient<ItemsPage>();
            RegisterTransient<IItemDetailViewModelFactory, ItemDetailViewModelFactory>();
            RegisterTransient<AboutPage>();
            RegisterTransient<AboutViewModel>();
        }
    }
}