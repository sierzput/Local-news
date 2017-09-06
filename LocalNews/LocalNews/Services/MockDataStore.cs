using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalNews.Models;
using Xamarin.Forms;

namespace LocalNews.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        bool _isInitialized;
        List<Item> _items;

        public async Task<Item> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(_items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(_items);
        }
        
        public async Task InitializeAsync()
        {
            if (_isInitialized)
                return;

            _items = new List<Item>();
            var items = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "Buy some cat food", Description="The cats are hungry"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Learn F#", Description="Seems like a functional idea"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Learn to play guitar", Description="Noted"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Buy some new candles", Description="Pine and cranberry for that winter feel"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Complete holiday shopping", Description="Keep it a secret!"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Finish a todo list", Description="Done"},
            };

            foreach (Item item in items)
            {
                _items.Add(item);
            }

            _isInitialized = true;
        }
    }
}