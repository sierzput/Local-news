using System.Threading.Tasks;
using HtmlAgilityPack;

namespace LocalNews.Services
{
    public interface INewsDownloader
    {
        Task<HtmlDocument> DownloadAsync();
    }
}