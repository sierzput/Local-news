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
        private readonly INewsDownloader _newsDownloader;
        private readonly IKurierParser _kurierParser;

        public NewsService(INewsDownloader newsDownloader, IKurierParser kurierParser)
        {
            _newsDownloader = newsDownloader;
            _kurierParser = kurierParser;
        }

        public async Task<NewsListItem> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(_items.FirstOrDefault(s => s.Id == id));
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
                new NewsListItem { Id = Guid.NewGuid().ToString(), Title = "Buy some cat food", Summary="The cats are hungry"},
                new NewsListItem { Id = Guid.NewGuid().ToString(), Title = "Learn F#", Summary="Seems like a functional idea"},
                new NewsListItem { Id = Guid.NewGuid().ToString(), Title = "Learn to play guitar", Summary="Noted"},
                new NewsListItem { Id = Guid.NewGuid().ToString(), Title = "Buy some new candles", Summary="Pine and cranberry for that winter feel"},
                new NewsListItem { Id = Guid.NewGuid().ToString(), Title = "Complete holiday shopping", Summary="Keep it a secret!"},
                new NewsListItem { Id = Guid.NewGuid().ToString(), Title = "Finish a todo list", Summary="Done"},
            };
            _items = await Task.FromResult(items);

            _isInitialized = true;
        }

        private async Task RefreshListAsync()
        {
            var htmlDocument = await _newsDownloader.DownloadAsync();
            _items = _kurierParser.Parse(htmlDocument);
        }
    }
}