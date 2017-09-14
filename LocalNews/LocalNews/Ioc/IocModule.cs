using LocalNews.Models;
using LocalNews.Services;

namespace LocalNews.Ioc
{
    public class IocModule : BaseIocModule
    {
        public override void Load()
        {
            RegisterTransient<INewsDownloader, NewsDownloader>();
            RegisterTransient<IDataStore<NewsListItem>, NewsService>();
        }
    }
}