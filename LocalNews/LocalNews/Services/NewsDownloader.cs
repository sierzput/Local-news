using System.Threading.Tasks;
using HtmlAgilityPack;

namespace LocalNews.Services
{
    public class NewsDownloader : INewsDownloader
    {
        public async Task<HtmlDocument> DownloadAsync()
        {
            var url = "http://www.kurierbytowski.com.pl/kurier/category/aktualnosci/";
            var web = new HtmlWeb();
            var document = await web.LoadFromWebAsync(url);
            return document;
        }
    }
}