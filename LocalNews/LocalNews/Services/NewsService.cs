using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalNews.Models;

namespace LocalNews.Services
{
    public class NewsService : IDataStore<NewsListItem>
    {
        private bool _isInitialized;
        private IEnumerable<NewsListItem> _items;
        private readonly IHtmlDocumentDownloader _downloader;
        private readonly IKurierPageParser _kurierPageParser;

        public NewsService(IHtmlDocumentDownloader downloader, IKurierPageParser kurierPageParser)
        {
            _downloader = downloader;
            _kurierPageParser = kurierPageParser;
        }

        public async Task<NewsListItem> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(_items.FirstOrDefault(s => s.Id.ToString() == id));
        }

        public async Task<IEnumerable<NewsListItem>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();
            if (forceRefresh)
            {
                await RefreshListAsync();
            }

            return await Task.FromResult(_items);
        }
        
        public async Task InitializeAsync()
        {
            if (_isInitialized)
                return;

            var items = new List<NewsListItem>
            {
                new NewsListItem { Id = Guid.NewGuid(), Title = "Buy some cat food", Summary="The cats are hungry"},
                new NewsListItem { Id = Guid.NewGuid(), Title = "Learn F#", Summary="Seems like a functional idea"},
                new NewsListItem { Id = Guid.NewGuid(), Title = "Learn to play guitar", Summary="Noted"},
                new NewsListItem { Id = Guid.NewGuid(), Title = "Buy some new candles", Summary="Pine and cranberry for that winter feel"},
                new NewsListItem { Id = Guid.NewGuid(), Title = "Complete holiday shopping", Summary="Keep it a secret!"},
                new NewsListItem { Id = Guid.NewGuid(), Title = "Finish a todo list", Summary="Done"},
            };
            _items = await Task.FromResult(items);

            _isInitialized = true;
        }

        private async Task RefreshListAsync()
        {
            var kurierUrl = "http://www.kurierbytowski.com.pl/kurier/category/aktualnosci/";
            var htmlDocument = await _downloader.DownloadAsync(kurierUrl);
            _items = _kurierPageParser.Parse(htmlDocument);
        }
    }
}