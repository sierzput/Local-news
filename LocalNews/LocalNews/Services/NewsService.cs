using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalNews.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocalNews.Services.NewsService))]
namespace LocalNews.Services
{
    public class NewsService : IDataStore<NewsListItem>
    {
        private bool _isInitialized;
        private List<NewsListItem> _items;

        public async Task<NewsListItem> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(_items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<NewsListItem>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(_items);
        }
        
        public async Task InitializeAsync()
        {
            if (_isInitialized)
                return;

            _items = new List<NewsListItem>();
            var items = new List<NewsListItem>
            {
                new NewsListItem { Id = Guid.NewGuid().ToString(), Title = "Buy some cat food", Summary="The cats are hungry"},
                new NewsListItem { Id = Guid.NewGuid().ToString(), Title = "Learn F#", Summary="Seems like a functional idea"},
                new NewsListItem { Id = Guid.NewGuid().ToString(), Title = "Learn to play guitar", Summary="Noted"},
                new NewsListItem { Id = Guid.NewGuid().ToString(), Title = "Buy some new candles", Summary="Pine and cranberry for that winter feel"},
                new NewsListItem { Id = Guid.NewGuid().ToString(), Title = "Complete holiday shopping", Summary="Keep it a secret!"},
                new NewsListItem { Id = Guid.NewGuid().ToString(), Title = "Finish a todo list", Summary="Done"},
            };

            foreach (NewsListItem item in items)
            {
                _items.Add(item);
            }

            _isInitialized = true;
        }
    }
}