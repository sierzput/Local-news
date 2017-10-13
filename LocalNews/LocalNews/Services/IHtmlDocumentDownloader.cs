using System.Threading.Tasks;
using HtmlAgilityPack;

namespace LocalNews.Services
{
    public interface IHtmlDocumentDownloader
    {
        Task<HtmlDocument> DownloadAsync(string url);
    }
}