using System.Threading.Tasks;

namespace SF.Service.Api {
    public interface ISearchApi {
        string ProviderName { get; }
        Task<long> SearchAsync(string searchContent);
    }
}