using JetBrains.Annotations;
using LocalNews.Helpers;
using LocalNews.Models;
using LocalNews.ViewModels;
using Xamarin.Forms;

namespace LocalNews.Views
{
    public partial class ItemsPage
    {
        private readonly ItemsViewModel _viewModel;
        private readonly IItemDetailViewModelFactory _detailViewModelFactory;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public ItemsPage()
        {
            InitializeComponent();
        }

        [UsedImplicitly]
        public ItemsPage(ItemsViewModel viewModel, IItemDetailViewModelFactory detailViewModelFactory)
        {
            _viewModel = viewModel;
            _detailViewModelFactory = detailViewModelFactory;
            InitializeComponent();

            BindingContext = _viewModel;
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is NewsListItem item))
                return;

            var viewModel = _detailViewModelFactory.Create(item);
            var itemDetailPage = new ItemDetailPage(viewModel);
            await Navigation.PushAsync(itemDetailPage);

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        private async void DisplayAlertAsync(Application application, MessagingCenterAlert alert)
        {
            await DisplayAlert(alert.Title, alert.Message, alert.Cancel);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<Application, MessagingCenterAlert>(Application.Current, "error", DisplayAlertAsync);

            if (_viewModel.Items.Count == 0)
            {
                _viewModel.LoadItemsCommand.Execute(null);
            }
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<Application, MessagingCenterAlert>(Application.Current, "error");
            base.OnDisappearing();
        }
    }
}
