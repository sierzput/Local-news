using LocalNews.Models;

namespace LocalNews.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public NewsListItem Item { get; set; }
        public ItemDetailViewModel(NewsListItem item = null)
        {
            Title = item.Title;
            Item = item;
        }

        int quantity = 1;
        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }
    }
}