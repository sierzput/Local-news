using LocalNews.Services;
using Moq;
using Xunit;

namespace LocalNews.Tests.Services
{
    public class NewsServiceTests
    {
        [Fact]
        public async void GetItemsFromDownloaderAsync()
        {
            var downloader = new Mock<INewsDownloader>();
            var sut = new NewsService(downloader.Object);

            await sut.GetItemsAsync();

            downloader.Verify(d => d.Download());
        }
    }
}