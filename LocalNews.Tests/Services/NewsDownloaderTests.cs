using LocalNews.Services;
using LocalNews.Tests.Helpers;
using Ploeh.AutoFixture;
using Xunit;

namespace LocalNews.Tests.Services
{
    public class NewsDownloaderTests : TestsBase
    {
        [Fact]
        public async void ReturnHtmlFromUrlAsync()
        {
            var sut = Fixture.Create<NewsDownloader>();

            var actual = await sut.DownloadAsync();

            Assert.NotNull(actual);
        }
    }
}