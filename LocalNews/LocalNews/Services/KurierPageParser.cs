using System.Collections.Generic;
using HtmlAgilityPack;
using LocalNews.Models;

namespace LocalNews.Services
{
    public class KurierPageParser : IKurierPageParser
    {
        public IEnumerable<NewsListItem> Parse(HtmlDocument htmlDocument)
        {
            var htmlNode = htmlDocument.GetElementbyId("archiwum_1");
            var articleNodes = htmlNode.SelectNodes("article");
            var items = new List<NewsListItem>();
            foreach (var node in articleNodes)
            {
                var title = node.SelectSingleNode("h1").InnerText;
                var publicationDate = node.SelectSingleNode("div[@class='meta']/h2").LastChild.InnerText;
                var detailsLink = node.SelectSingleNode("a").GetAttributeValue("href", "");
                var summary = HtmlEntity.DeEntitize(node.SelectSingleNode("p").InnerText);
                var thumbnailLink = node
                    .SelectSingleNode("div[contains(@class, 'thumb_gallery')]")
                    ?.SelectSingleNode("ul[@class='slides']/li[1]/img")
                    ?.GetAttributeValue("src", "");

                var item = new NewsListItem
                {
                    Title = title,
                    PublicationDate = publicationDate,
                    DetailsLink = detailsLink,
                    Summary = summary,
                    Thumbnail = thumbnailLink,
                };
                items.Add(item);
            }
            return items;
        }
    }
}