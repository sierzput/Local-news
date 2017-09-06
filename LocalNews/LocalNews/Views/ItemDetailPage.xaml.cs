using LocalNews.ViewModels;

namespace LocalNews.Views
{
    public partial class ItemDetailPage
    {
        private ItemDetailViewModel _viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public ItemDetailPage()
        {
            InitializeComponent();
        }

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }
    }
}
