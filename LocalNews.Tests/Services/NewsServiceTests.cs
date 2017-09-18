using HtmlAgilityPack;
using LocalNews.Services;
using LocalNews.Tests.Helpers;
using Moq;
using Ploeh.AutoFixture;
using Xunit;

namespace LocalNews.Tests.Services
{
    public class NewsServiceTests : TestsBase
    {
        private readonly NewsService _sut;

        public NewsServiceTests()
        {
            _sut = Fixture.CreateWithFrozen<NewsService>();
        }

        [Fact]
        public async void GetHtmlFromDownloaderWhenForceRefreshAsync()
        {
            var downloader = Fixture.Mock<INewsDownloader>();

            await _sut.GetItemsAsync(forceRefresh: true);

            downloader.Verify(d => d.DownloadAsync());
        }

        [Fact]
        public async void DoNotGetHtmlFromDownloaderWhenNotForceRefreshAsync()
        {
            var downloader = Fixture.Mock<INewsDownloader>();

            await _sut.GetItemsAsync(forceRefresh: false);

            downloader.Verify(d => d.DownloadAsync(), Times.Never());
        }

        [Fact]
        public async void ParseHtmlFromDownloaderAsync()
        {
            var htmlDocument = Fixture.Create<HtmlDocument>();
            var parser = Fixture.Mock<IKurierParser>();

            Fixture.Mock<INewsDownloader>()
                .Setup(d => d.DownloadAsync())
                .ReturnsAsync(htmlDocument);

            await _sut.GetItemsAsync(forceRefresh: true);

            parser.Verify(p => p.Parse(htmlDocument));
        }
    }
}