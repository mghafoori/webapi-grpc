using System.Threading.Tasks;
using WebApi.Model;

namespace WebApi.Services
{
    public interface ICacheGrpcClientService
    {
        Task<bool> AddAsync(string key, string value);
        Task<bool> DeleteAsync(string key);
        Task<CacheItemModel> GetAsync(string key);
        Task<bool> UpdateAsync(string key, string value);
    }
}