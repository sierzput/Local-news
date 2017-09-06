using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalNews.Services
{
    public interface IDataStore<T>
    {
        Task InitializeAsync();
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
