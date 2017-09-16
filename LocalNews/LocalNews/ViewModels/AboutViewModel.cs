using System;
using System.Windows.Input;
using LocalNews.Models;
using LocalNews.Services;
using Xamarin.Forms;

namespace LocalNews.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel(IDataStore<NewsListItem> dataStore) : base(dataStore)
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }

        /// <summary>
        /// Command to open browser to xamarin.com
        /// </summary>
        public ICommand OpenWebCommand { get; }
    }
}
