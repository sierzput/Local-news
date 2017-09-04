using System.Collections.Generic;

namespace LocalNews.Models
{
    public class NewsItemDetails
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public List<string> Thumbnails { get; set; }
        public string PublicationDate { get; set; }
        public string FullArticle { get; set; }
        public string Author { get; set; }
        public string SourceLink { get; set; }
    }
}