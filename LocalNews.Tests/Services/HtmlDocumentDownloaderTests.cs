using FluentAssertions;
using LocalNews.Services;
using LocalNews.Tests.Helpers;
using Ploeh.AutoFixture;
using Xunit;

namespace LocalNews.Tests.Services
{
    public class HtmlDocumentDownloaderTests : TestsBase
    {
        private readonly string _url= "http://example.com/";

        [Fact]
        public async void ReturnHtmlDocumentFromUrlAsync()
        {
            var sut = Fixture.Create<HtmlDocumentDownloader>();

            var actual = await sut.DownloadAsync(_url);

            actual.DocumentNode.InnerHtml.Should().NotBeEmpty();
        }
    }
}