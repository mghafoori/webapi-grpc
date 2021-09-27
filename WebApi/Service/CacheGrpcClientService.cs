using Grpc.Net.Client;
using System.Threading.Tasks;
using WebApi.Clients;

namespace WebApi.Service
{
    public class CacheGrpcClientService : ICacheGrpcClientService
    {
        private readonly CacheServiceProvider.CacheServiceProviderClient _client;
        public CacheGrpcClientService()
        {
            var grpcChannel = GrpcChannel.ForAddress("http://localhost:5000");
            _client = new CacheServiceProvider.CacheServiceProviderClient(grpcChannel);
        }

        public async Task<bool> AddToCacheAsync(string key, string value)
        {
            var addRequest = new AddRequest { Key = key, Value = "Toronto" };
            var addReply = await _client.AddAsync(addRequest);
            return addReply.Result;
        }

    }
}
