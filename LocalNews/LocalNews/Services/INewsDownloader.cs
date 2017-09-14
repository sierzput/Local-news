namespace LocalNews.Services
{
    public interface INewsDownloader
    {
        void Download();
    }

    public class NewsDownloader : INewsDownloader
    {
        public void Download()
        {
            throw new System.NotImplementedException();
        }
    }
}