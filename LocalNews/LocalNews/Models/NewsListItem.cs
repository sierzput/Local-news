using System;

namespace LocalNews.Models
{
    public class NewsListItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        public string PublicationDate { get; set; }
        public string Summary { get; set; }
        public string DetailsLink { get; set; }

        public NewsListItem()
        {
            Id = Guid.NewGuid();
        }
    }
}