using System.Threading.Tasks;

namespace SF.Service.Data {
    public interface ISearcherFightService {
        Task<SearchFightResult> FightAsync(string[] args);
    }
}