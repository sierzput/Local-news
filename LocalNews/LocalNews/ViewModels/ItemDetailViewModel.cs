using System.Collections.Generic;
using Castle.Core.Internal;
using LocalNews.Models;
using LocalNews.Services;

namespace LocalNews.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public NewsListItem Item { get; set; }
        public IEnumerable<string> Images { get; }

        public ItemDetailViewModel(NewsListItem item, IDataStore<NewsListItem> dataStore) : base(dataStore)
        {
            Title = item.Title;
            Item = item;
            var images = new List<string>();
            if (!item.Thumbnail.IsNullOrEmpty())
            {
                images.Add(item.Thumbnail);
                images.Add(item.Thumbnail);
            }
            Images = images;
        }
    }
}