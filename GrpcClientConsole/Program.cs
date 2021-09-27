using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace GrpcClientConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var grpcChannel = GrpcChannel.ForAddress("http://localhost:5000");
            var cacheGrpcServiceClient = new CacheServiceProvider.CacheServiceProviderClient(grpcChannel);
            
            // Test Add
            var addRequest = new AddRequest { Key = "City", Value = "Toronto" };
            var addReply = await cacheGrpcServiceClient.AddAsync(addRequest);
            Console.WriteLine($"Add result: {addReply.Result}");

            // Test Get - Verify Add
            var getRequest = new GetRequest { Key = "City" };
            var getReply = await cacheGrpcServiceClient.GetAsync(getRequest);
            Console.WriteLine($"Get result: {getReply.Result}, Key: {getReply.Key}, Value: {getReply.Value}");

            // Test Update
            var updateRequest = new UpdateRequest { Key = "City", Value = "Ottawa" };
            var updateReply = await cacheGrpcServiceClient.UpdateAsync(updateRequest);
            Console.WriteLine($"Update result: {updateReply.Result}");

            // Test Get - Verify Update
            getReply = await cacheGrpcServiceClient.GetAsync(getRequest);
            Console.WriteLine($"Get result: {getReply.Result}, Key: {getReply.Key}, Value: {getReply.Value}");

            // Test Delete
            var deleteRequest = new DeleteRequest { Key = "City" };
            var deleteReply = await cacheGrpcServiceClient.DeleteAsync(deleteRequest);
            Console.WriteLine($"Delete result: {deleteReply.Result}");

            // Test Get - Verify Delete
            getReply = await cacheGrpcServiceClient.GetAsync(getRequest);
            Console.WriteLine($"Get result: {getReply.Result}, Key: {getReply.Key}, Value: {getReply.Value}");
        }
    }
}
