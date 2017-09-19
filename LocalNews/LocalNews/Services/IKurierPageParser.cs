using System.Collections.Generic;
using HtmlAgilityPack;
using LocalNews.Models;

namespace LocalNews.Services
{
    public interface IKurierPageParser
    {
        IEnumerable<NewsListItem> Parse(HtmlDocument htmlDocument);
    }
}