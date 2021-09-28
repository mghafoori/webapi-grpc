using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WebApi.Clients;
using WebApi.Model;
using WebApi.Options;
using static WebApi.Clients.CacheServiceProvider;

namespace WebApi.Services
{
    public class CacheGrpcClientService : ICacheGrpcClientService
    {

        private readonly CacheServiceProviderClient _client;
        private readonly ILogger<CacheGrpcClientService> _logger;

        public CacheGrpcClientService(ILogger<CacheGrpcClientService> logger, IOptions<GrpcOptions> options)
        {
            var grpcChannel = GrpcChannel.ForAddress(options.Value.Address);
            _client = new CacheServiceProviderClient(grpcChannel);
            _logger = logger;
        }

        public async Task<bool> AddAsync(string key, string value)
        {
            var addRequest = new AddRequest { Key = key, Value = value };
            var addReply = await _client.AddAsync(addRequest);
            _logger.LogInformation($"Add result: {addReply.Result}");
            return addReply.Result;
        }

        public async Task<CacheItemModel> GetAsync(string key)
        {
            var getRequest = new GetRequest { Key = key };
            var getReply = await _client.GetAsync(getRequest);
            _logger.LogInformation($"Get result: {getReply.Result}, Key: {getReply.Key}, Value: {getReply.Value}");
            if (getReply.Result)
            {
                return new CacheItemModel
                {
                    Key = getReply.Key,
                    Value = getReply.Value
                };
            }
            return null;
        }

        public async Task<bool> UpdateAsync(string key, string value)
        {
            var updateRequest = new UpdateRequest { Key = key, Value = value };
            var updateReply = await _client.UpdateAsync(updateRequest);
            _logger.LogInformation($"Update result: {updateReply.Result}");
            return updateReply.Result;
        }

        public async Task<bool> DeleteAsync(string key)
        {
            var deleteRequest = new DeleteRequest { Key = key };
            var deleteReply = await _client.DeleteAsync(deleteRequest);
            _logger.LogInformation($"Delete result: {deleteReply.Result}");
            return deleteReply.Result;
        }
    }
}
