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

        [HttpGet]
        [Route("dl/{path}")]
        public async Task<IActionResult> Dl(string path)
        {
        var filePath = "C:\\RessourceFile\\" + path;

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, GetContentType(filePath), filePath);
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
        [HttpGet]
        [Route("getByClasse/{classe}")]
        public List<Fichier> GetByClasse(string classe)
        {
            List<Fichier> response = new List<Fichier>();
            byte[] content;
            folderPath = folderPath + "\\" + classe;
            string[] filePaths = Directory.GetFiles(folderPath);
            string s;
            foreach (string path in filePaths.ToList())
            {
                s = path;
                FileInfo fi = new FileInfo(path);
                string extension = fi.Extension;
                content = System.IO.File.ReadAllBytes(path);
                s = s.Remove(0, 20);
                response.Add(new Fichier(content, s, extension));
            }
            Console.WriteLine(response);
            return response;
        }
    }
}

