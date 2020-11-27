using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IDSTORE2.Controllers
{
    [ApiController]
    [Route("api")]
    public class UploadDownloadController : ControllerBase
    {
        private IHostEnvironment _hostingEnvironment;

        public UploadDownloadController(IHostEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }
            if (file.Length > 0)
            {
                var filePath = Path.Combine(uploads, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            return Ok();
        }


        //[FromQuery]
        //string file
        [HttpGet]
        [Route("download")]
        public async Task<IActionResult> Download()
        {
            var uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads");
            var filePath = "E:\\Bureau\\M2\\IDCity_IDStore\\RessourceFile";
            //var filePath = Path.Combine(uploads, file);
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, GetContentType(filePath), /*file*/ "TOTO");
        }

        [HttpGet]
        [Route("files")]
        public IActionResult Files()
        {
            var result = new List<string>();

            var uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads");
            if (Directory.Exists(uploads))
            {
                var provider = _hostingEnvironment.ContentRootFileProvider;
                foreach (string fileName in Directory.GetFiles(uploads))
                {
                    var fileInfo = provider.GetFileInfo(fileName);
                    result.Add(fileInfo.Name);
                }
            }
            return Ok(result);
        }

        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
