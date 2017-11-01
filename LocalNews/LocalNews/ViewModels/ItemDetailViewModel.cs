using LocalNews.Models;
using LocalNews.Services;

namespace LocalNews.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public NewsListItem Item { get; set; }
        public ItemDetailViewModel(NewsListItem item, IDataStore<NewsListItem> dataStore) : base(dataStore)
        {
            Title = item.Title;
            Item = item;
        }
    }
}