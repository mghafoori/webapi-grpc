using System.Threading.Tasks;

namespace WebApi.Service
{
    public interface ICacheGrpcClientService
    {
        Task<bool> AddToCacheAsync(string key, string value);
    }
}