using FluentAssertions;
using HtmlAgilityPack;
using LocalNews.Helpers;
using LocalNews.Models;
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
            var downloader = Fixture.Mock<IHtmlDocumentDownloader>();

            await _sut.GetItemsAsync(forceRefresh: true);

            downloader.Verify(d => d.DownloadAsync(It.IsAny<string>()));
        }

        [Fact]
        public async void DoNotGetHtmlFromDownloaderWhenNotForceRefreshAsync()
        {
            var downloader = Fixture.Mock<IHtmlDocumentDownloader>();

            await _sut.GetItemsAsync(forceRefresh: false);

            downloader.Verify(d => d.DownloadAsync(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public async void ParseHtmlFromDownloaderAsync()
        {
            var htmlDocument = Fixture.Create<HtmlDocument>();
            var parser = Fixture.Mock<IKurierPageParser>();

            Fixture.Mock<IHtmlDocumentDownloader>()
                .Setup(d => d.DownloadAsync(It.IsAny<string>()))
                .ReturnsAsync(htmlDocument);

            await _sut.GetItemsAsync(forceRefresh: true);

            parser.Verify(p => p.Parse(htmlDocument));
        }

        [Fact]
        public async void ReturnNewsItems()
        {
            var expected = Fixture.CreateMany<NewsListItem>().Materialize();

            Fixture.Mock<IKurierPageParser>()
                .Setup(parser => parser.Parse(It.IsAny<HtmlDocument>()))
                .Returns(expected);

            var actual = await _sut.GetItemsAsync(forceRefresh: true);

            actual.Should().BeSameAs(expected);
        }
    }
}