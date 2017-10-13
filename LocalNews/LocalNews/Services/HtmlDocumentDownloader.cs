using System.Threading.Tasks;
using HtmlAgilityPack;

namespace LocalNews.Services
{
    public class HtmlDocumentDownloader : IHtmlDocumentDownloader
    {
        public async Task<HtmlDocument> DownloadAsync(string url)
        {
            var web = new HtmlWeb();
            var document = await web.LoadFromWebAsync(url);
            return document;
        }
    }
}