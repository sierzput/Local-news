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
                var summary = node.SelectSingleNode("p").InnerText;
                var item = new NewsListItem
                {
                    Title = title,
                    PublicationDate = publicationDate,
                    DetailsLink = detailsLink,
                    Summary = summary
                };
                items.Add(item);

            }
            return items;
        }
    }
}