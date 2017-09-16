using LocalNews.Models;
using LocalNews.Services;

namespace LocalNews.ViewModels
{
    public interface IItemDetailViewModelFactory
    {
        ItemDetailViewModel Create(NewsListItem item);
    }

    public class ItemDetailViewModelFactory : IItemDetailViewModelFactory
    {
        private readonly IDataStore<NewsListItem> _dataStore;

        public ItemDetailViewModelFactory(IDataStore<NewsListItem> dataStore)
        {
            _dataStore = dataStore;
        }

        public ItemDetailViewModel Create(NewsListItem item)
        {
            return new ItemDetailViewModel(item, _dataStore);
        }
    }
}