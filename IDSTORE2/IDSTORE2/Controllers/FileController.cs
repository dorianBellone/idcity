using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IDSTORE2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        [HttpGet]
        public List<Fichier> Get()
        {
            List<Fichier> response = new List<Fichier>();
            List<String> responseCONIO = new List<String>();
            byte[] content = null;
            string[] filePaths = Directory.GetFiles(folderPath);
           

            foreach (string path in filePaths.ToList())
            {
                FileInfo fi = new FileInfo(path);
                string extension = fi.Extension;
                content = System.IO.File.ReadAllBytes(path);
                response.Add(new Fichier(content, path, extension));
                responseCONIO.Add(path);
            }
            return response;
        }
    }
}
