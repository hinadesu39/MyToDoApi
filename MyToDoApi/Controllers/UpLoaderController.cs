using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyToDoApi.Sevice;
using UserMgrWebApi;

namespace MyToDoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UpLoaderController : ControllerBase
    {
        private readonly IStorageClient client;
        public UpLoaderController(IStorageClient client)
        {
            this.client = client;
        }
        [RequestSizeLimit(60_000_000)]
        [HttpPost]
        public async Task<ActionResult<Uri>> Upload([FromForm] IFormFile file)
        {
            string fileName = file.FileName;
            using Stream content = file.OpenReadStream();
            var RemoteUrl = await client.SaveAsync(fileName, content);
            return RemoteUrl;
        }
    }
}
