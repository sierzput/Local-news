using System.Collections.Generic;
using HtmlAgilityPack;
using LocalNews.Models;

namespace LocalNews.Services
{
    public class KurierParser : IKurierParser
    {
        public IEnumerable<NewsListItem> Parse(HtmlDocument htmlDocument)
        {
            throw new System.NotImplementedException();
        }
    }
}