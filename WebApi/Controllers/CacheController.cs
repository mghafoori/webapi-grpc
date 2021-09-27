using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApi.Clients;
using WebApi.Service;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly ICacheGrpcClientService _client;

        public CacheController(ICacheGrpcClientService client) => _client = client;

        [HttpGet("{key}")]
        public async Task<IActionResult> GetAsync(string key)
        {

            return new OkObjectResult("Some City");
        }
    }
}
