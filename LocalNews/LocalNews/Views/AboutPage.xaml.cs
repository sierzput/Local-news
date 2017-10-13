using JetBrains.Annotations;
using LocalNews.ViewModels;

namespace LocalNews.Views
{
    public partial class AboutPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        [UsedImplicitly]
        public AboutPage(AboutViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
