using System;
using System.Diagnostics;
using System.Threading.Tasks;
using LocalNews.Helpers;
using LocalNews.Models;
using LocalNews.Services;
using Xamarin.Forms;

namespace LocalNews.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<NewsListItem> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel(IDataStore<NewsListItem> dataStore) : base(dataStore)
        {
            Title = "Browse";
            Items = new ObservableRangeCollection<NewsListItem>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                Items.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(Application.Current, "error", new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items. \n" + ex,
                    Cancel = "OK"
                });
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}