using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IDSTORE2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.StaticFiles;

namespace IDSTORE2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private string folderPath { get; set; }
        

        //private readonly ILogger<WeatherForecastController> _logger;

        public FileController(/*ILogger<WeatherForecastController> logger*/)
        {
            //folderPath = "E:\\Bureau\\M2\\IDCity_IDStore\\RessourceFile";
            folderPath = "C:\\RessourceFile";
            //_logger = logger;
        }
        //[HttpGet]
        //public string Get()
        //{
        //    Console.WriteLine("TOTOT");
        //    return "toto";
        //}
        [HttpGet("dwn")]
        public void Dwn()
        {
            string remoteUri = "C:\\RessourceFile\\";
            string fileName = "rrr.pdf", myStringWebResource = null;
            // Create a new WebClient instance.
            WebClient myWebClient = new WebClient();
            // Concatenate the domain with the Web resource filename.
            myStringWebResource = remoteUri + fileName;
            Console.WriteLine("Downloading File \"{0}\" from \"{1}\" .......\n\n", fileName, myStringWebResource);
            // Download the Web resource and save it into the current filesystem folder.
            myWebClient.DownloadFile(myStringWebResource, fileName);
            Console.WriteLine("Successfully Downloaded File \"{0}\" from \"{1}\"", fileName, myStringWebResource);
            //Console.WriteLine("\nDownloaded file saved in the following file system folder:\n\t" + Application.StartupPath);
        }
        [HttpGet]
        [Route("dl")]
        public async Task<IActionResult> Dl([FromQuery] string file)
        {
        var filePath = "C:\\RessourceFile\\rrr.pdf";

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, GetContentType(filePath), file);
        }

        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/pdf";
            }
            return contentType;
        }

        public List<Fichier> Get()
        {
            List<Fichier> response = new List<Fichier>();
            byte[] content;
            string[] filePaths = Directory.GetFiles(folderPath);
            string s;
            foreach (string path in filePaths.ToList())
            {
                s = path;
                FileInfo fi = new FileInfo(path);
                string extension = fi.Extension;
                content = System.IO.File.ReadAllBytes(path);
                s = s.Remove(0, 17);
                response.Add(new Fichier(content, s, extension));
            }
            Console.WriteLine(response);
            return response;
        }
        [HttpGet("getget")]
        public async Task<HttpResponseMessage> GetGet()
        {
            var fileName = "C:\\RessourceFile\\rrr.pdf";
            var contentType = GetMimeType(fileName);

            var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            return result;
        }
        [HttpGet ("dlmm")]
        public FileStreamResult Download()
        {
            var file = "C:\\RessourceFile\\rrr.pdf";
            var memory = new MemoryStream();
            using (var stream = new FileStream(file, FileMode.Open))
            {
                stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return File(memory, GetMimeType(file), file);
        }


        private string GetMimeType(string file)
        {
            string extension = Path.GetExtension(file).ToLowerInvariant();
            switch (extension)
            {
                case ".txt": return "text/plain";
                case ".pdf": return "application/pdf";
                case ".doc": return "application/vnd.ms-word";
                case ".docx": return "application/vnd.ms-word";
                case ".xls": return "application/vnd.ms-excel";
                case ".png": return "image/png";
                case ".jpg": return "image/jpeg";
                case ".jpeg": return "image/jpeg";
                case ".gif": return "image/gif";
                case ".csv": return "text/csv";
                default: return "";
            }
        }
    }
}

