using LocalNews.Services;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Xunit;

namespace LocalNews.Tests.Services
{
    public class NewsServiceTests
    {
        private readonly IFixture _fixture;

        public NewsServiceTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        [Fact]
        public async void GetItemsFromDownloaderAsync()
        {
            var downloader = _fixture.Freeze<Mock<INewsDownloader>>();
            var sut = _fixture.Create<NewsService>();

            await sut.GetItemsAsync();

            downloader.Verify(d => d.Download());
        }
    }
}