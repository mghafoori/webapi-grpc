using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Model;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly ICacheGrpcClientService _client;

        public CacheController(ICacheGrpcClientService client) 
            => _client = client;

        [HttpGet("{key}")]
        public async Task<IActionResult> GetAsync(string key)
        {
            var model = await _client.GetAsync(key);
            return model == null ? NotFound() : new OkObjectResult(model);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CacheItemModel model)
        {
            var result = await _client.AddAsync(model.Key, model.Value);
            return result ? Ok() : Conflict();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] CacheItemModel model)
        {
            var result = await _client.UpdateAsync(model.Key, model.Value);
            return result ? Ok() : NotFound();
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> DeleteAsync(string key)
        {
            var result = await _client.DeleteAsync(key);
            return result ? Ok() : NotFound();
        }
    }
}
