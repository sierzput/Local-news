using System.IO;
using System.Linq;
using FluentAssertions;
using HtmlAgilityPack;
using LocalNews.Models;
using LocalNews.Services;
using LocalNews.Tests.Helpers;
using Ploeh.AutoFixture;
using Xunit;

namespace LocalNews.Tests.Services
{
    public class KurierPageParserTests : TestsBase
    {
        private readonly KurierPageParser _sut;
        private readonly HtmlDocument _htmlDocument;

        public KurierPageParserTests()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var url = Path.Combine(currentDirectory, "../../../test_page.html");

            _htmlDocument = new HtmlWeb().Load(url);
            _sut = Fixture.Create<KurierPageParser>();
        }

        [Fact]
        public void ReturnNotEmptyCollection()
        {
            var actual = _sut.Parse(_htmlDocument);

            actual.Should().NotBeEmpty();
        }

        [Fact]
        public void ReturnElementsWithCorrectTextData()
        {
            var expected = new NewsListItem
            {
                Title = "Początek sezonu z mocnym brzmieniem",
                Summary = "Za nami pierwszy koncert",
                DetailsLink = "http://www.kurierbytowski.com.pl/kurier/aktualnosci/poczatek-sezonu-mocnym-brzmieniem/",
                PublicationDate = "19/09/2017"
            };

            var actual = _sut.Parse(_htmlDocument).First();

            ShouldHaveCorrectTextFields(actual, expected);
        }

        [Fact]
        public void ReturnElementsWithEmptyThumbnailLinkIfDoesNotExist()
        {
            var actual = _sut.Parse(_htmlDocument).First();

            actual.Thumbnail.Should().BeNullOrEmpty();
        }

        [Fact]
        public void ReturnElementsWithCorrectThumbnailLinkIfExists()
        {
            var expected = "http://www.kurierbytowski.com.pl/kurier/wp-content/uploads/2017/09/189-330x185.jpg";

            var actual = _sut.Parse(_htmlDocument).Skip(1).First();

            actual.Thumbnail.Should().Be(expected);
        }

        private void ShouldHaveCorrectTextFields(NewsListItem actual, NewsListItem expected)
        {
            actual.Title.Should().Be(expected.Title);
            actual.Summary.Should().StartWith(expected.Summary);
            actual.PublicationDate.Should().Be(expected.PublicationDate);
            actual.DetailsLink.Should().Be(expected.DetailsLink);
        }
    }
}