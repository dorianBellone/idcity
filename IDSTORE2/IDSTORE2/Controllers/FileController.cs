using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
            folderPath = "E:\\Bureau\\M2\\IDCity_IDStore\\RessourceFile";
            //_logger = logger;
        }
        
        [HttpGet]
        public List<File> Get()
        {
            List<File> response = new List<File>();
            string[] filePaths = Directory.GetFiles(folderPath);

            foreach(string path in filePaths.ToList())
            {
                response.Add(new File(null, path, null));
            }
            return response;
        }
    }

    public class File
    {
        public File(byte[] content, string name, string type)
        {
            this.Content = content;
            this.Name = name;
            this.Type = type;
        }

        private Byte[] Content { get; set; }
        private String Name { get; set; }

        private String Type { get; set; }

    }
}
