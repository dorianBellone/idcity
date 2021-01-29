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

        private readonly APIContext context;
        private readonly ILogger<FileController> logger;

        public FileController(ILogger<FileController> _logger, APIContext _context)
        {
            //folderPath = "E:\\Bureau\\M2\\IDCity_IDStore\\RessourceFile";
            folderPath = "C:\\RessourceFile";

            logger = _logger;
            context = _context;
            //var listDataFile = _context.Files.ToList();
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

        public List<FileOverride> Get()
        {
            List<FileOverride> response = new List<FileOverride>();
            byte[] content;
            string[] filePaths = Directory.GetFiles(folderPath);
            string fileDefaultName;
            foreach (string path in filePaths.ToList())
            {
                fileDefaultName = path;
                FileInfo fi = new FileInfo(path);
                content = System.IO.File.ReadAllBytes(path);
                response.Add(new FileOverride(context,content, fi.Name, fi.Extension, null,fi.FullName));
            }
            Console.WriteLine(response);
            return response;
        }
        [HttpGet]
        [Route("getAll")]
        public List<FileOverride> GetAll()
        {
            List<FileOverride> response = new List<FileOverride>();
            byte[] content;
            string[] filePaths = Directory.GetFiles(folderPath, "*.*",
            SearchOption.AllDirectories);
            string fileDefaultName;
            foreach (string path in filePaths.ToList())
            {
                
                FileInfo fi = new FileInfo(path);
                content = System.IO.File.ReadAllBytes(path);
                response.Add(new FileOverride(context, content, fi.Name, fi.Extension,null,fi.FullName));
            }
            Console.WriteLine(response);
            return response;
        }
        [HttpGet]
        [Route("getByClasse/{classe}")]
        public List<FileOverride> GetByClasse(string classe)
        {
            List<FileOverride> response = new List<FileOverride>();
            byte[] content;
            folderPath = folderPath + "\\" + classe;
            string[] filePaths = Directory.GetFiles(folderPath);
            string fileDefaultName;
            foreach (string path in filePaths.ToList())
            {
                FileInfo fi = new FileInfo(path);
                content = System.IO.File.ReadAllBytes(path);
                response.Add(new FileOverride(context, content, fi.Name, fi.Extension,null,fi.FullName));
            }
            Console.WriteLine(response);
            return response;
        }

        
        public Boolean RenameFile(Dictionary<Guid,String> _dicoFile_IDName)
        {

            string[] filePaths = Directory.GetFiles(folderPath, "*.*",SearchOption.AllDirectories);
            string fileDefaultName;
            foreach (string path in filePaths.ToList())
            {
                foreach (KeyValuePair<Guid, String> kvp in _dicoFile_IDName)
                {

                }
                //fileDefaultName = path;
                //FileInfo fi = new FileInfo(path);
                //string extension = fi.Extension;
                //fileDefaultName = fileDefaultName.Remove(0, 17);
                //System.IO.File.Move(oldNameFullPath, newNameFullPath);
            }

            //foreach (KeyValuePair<Guid, String> kvp in _dicoFile_IDName)
            //{ 
            
            //}


            return false;

        }

        //add getbyclassebymatter
    }
}

