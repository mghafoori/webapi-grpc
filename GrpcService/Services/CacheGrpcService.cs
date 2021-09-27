using Grpc.Core;
using GrpcService.Interfaces;
using System.Threading.Tasks;

namespace GrpcService.Services
{
    public class CacheGrpcService : CacheServiceProvider.CacheServiceProviderBase
    {
        private readonly ICache _cacheService;

        public CacheGrpcService(ICache cacheService) 
            => _cacheService = cacheService;

        public override Task<AddReply> Add(AddRequest request, ServerCallContext context)
        {
            var addResult = _cacheService.Add(request.Key, request.Value);
            var addReply = new AddReply { Result = addResult };
            var taskResult = Task.FromResult(addReply);
            return taskResult;
        }

        public override Task<DeleteReply> Delete(DeleteRequest request, ServerCallContext context)
        {
            var deleteResult = _cacheService.Delete(request.Key);
            var deleteReply = new DeleteReply { Result = deleteResult };
            var taskResult = Task.FromResult(deleteReply);
            return taskResult;
        }

        public override Task<GetReply> Get(GetRequest request, ServerCallContext context)
        {
            var value = _cacheService.Get(request.Key);
            var getReply = new GetReply { Result = value != null, Key = request.Key, Value = string.IsNullOrEmpty(value) ? string.Empty : value };
            var taskResult = Task.FromResult(getReply);
            return taskResult;
        }

        public override Task<UpdateReply> Update(UpdateRequest request, ServerCallContext context)
        {
            var updateResult = _cacheService.Update(request.Key, request.Value);
            var updateReply = new UpdateReply { Result = updateResult };
            var taskResult = Task.FromResult(updateReply);
            return taskResult;
        }
    }
}
