using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IDSTORE2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using IDSTORE2.Services;
using IDSTORE2.Data;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace IDSTORE2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private string FolderPath { get; set; }

        private readonly APIContext Context;
        private readonly LogServices LogServices;
        private readonly ArchivesServices ArchiveServices;

        private readonly ILogger<FileController> logger;
        //private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration config;
        private IWebHostEnvironment env;
        private String user;
        private String modeLog;

        public FileController(ILogger<FileController> _logger, APIContext _context, IConfiguration _config, IWebHostEnvironment _env, LogServices _logservice)
        {
            logger = _logger;
            Context = _context;
            var listDataFile = _context.File.ToList();
            LogServices = _logservice;
            config = _config;
            if (User == null) user = "admin";
            else if (User.Identity.Name == null) user = "admin";
            else user = User.Identity.Name;
            if (String.IsNullOrWhiteSpace(user)) user = "admin";
            env = _env;
            if (env.IsProduction())
            {
                string PathProd = config.GetSection("PathFile").GetSection("PathFileProd").Value;
                FolderPath = PathProd;
            }
            else if (env.IsDevelopment())
            {
                string PathDev = config.GetSection("PathFile").GetSection("PathFileDev").Value;
                FolderPath = PathDev;
            }
            modeLog = config.GetSection("ModeLog").Value;

        }

        /// <summary>
        /// Get all name and path of all file of a classe.
        /// </summary>
        /// <param name="classe"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getByClasse/{classe}")]
        public async Task<List<FileOverride>> GetByClasse(string classe)
        {
            List<FileOverride> response = new List<FileOverride>();

            FolderPath = FolderPath + classe;

            // Log 
            if (modeLog == "complete")
            {
                await LogServices.AddLog(LogServices.GetTypeLog("Get"), user, "GetByClasse : " + FolderPath + " By : " + user);
            }
            string[] filePaths = Directory.GetFiles(FolderPath);
            foreach (string path in filePaths.ToList())
            {
                FileInfo fi = new FileInfo(path);
                //content = System.IO.File.ReadAllBytes(path);
                response.Add(new FileOverride(/*context,*//* content,*/ fi.Name, fi.Extension, null, fi.FullName));
            }
            Console.WriteLine(response);
            return response;
        }

        /// <summary>
        /// Download a file by this class and name
        /// </summary>
        /// <param name="classe"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("download/{classe}/{name}")]
        public async Task<IActionResult> Download(string classe, string name)
        {
            // Check if file exist
            var filePath = "";
            if (env.IsProduction())
            {
                filePath = FolderPath + classe + '/' + name;
            }
            else if (env.IsDevelopment())
            {
                filePath = FolderPath + classe + '\\' + name;
            }

            if (!System.IO.File.Exists(filePath))
                return NotFound();

            // Log 
            if (modeLog == "complete")
            {
                await LogServices.AddLog(LogServices.GetTypeLog("Get"), user, "Download : " + filePath + " By : " + user);
            }
            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            //Response.ContentType = "application/pdf";
            //Response.Body = File(memory, GetContentType(filePath), name).FileStream;


            return File(memory, /*GetContentType(filePath)*/"application/pdf", name);
            // return File(memory, /*GetContentType(filePath)*/ "application/pdf", filePath);
        }

        /// <summary>
        /// Delete a file by this class and name
        /// </summary>
        /// <param name="classe"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delete/{classe}/{name}")]
        public async Task<IActionResult> Delete(string classe, string name)
        {
            // Check if file exist
            var filePath = "";
            if (env.IsProduction())
            {
                filePath = FolderPath + classe + '/' + name;
            }
            else if (env.IsDevelopment())
            {
                filePath = FolderPath + classe + '\\' + name;
            }
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            // Log 
            await LogServices.AddLog(LogServices.GetTypeLog("Delete"), user, "Delete : " + filePath + " By : " + user);
            // Archive
            await ArchiveServices.ArchiveFile(TypeArchives.Delete, filePath, classe, user);
            // Delete on disk
            System.IO.File.Delete(filePath);
            
            // Delete on DataBase
            return Ok("Document Supprimer.");
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("upload/{classe}/{name}")]
        public async Task<IActionResult> Upload(string classe, string name )
        {
            if (string.IsNullOrWhiteSpace(classe) | String.IsNullOrWhiteSpace(name)) return BadRequest();
            try
            {
                // Check if directory exist
                var ressourcePath = "";
                if (env.IsProduction())
                {
                    ressourcePath = FolderPath + classe;
                }
                else if (env.IsDevelopment())
                {
                    ressourcePath = FolderPath + classe;
                }
                if (!System.IO.Directory.Exists(ressourcePath))
                    return NotFound();

              
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                if (file.Length > 0)
                {
                    if (String.IsNullOrEmpty(name)) name = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(ressourcePath, name);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    // Log 
                    await LogServices.AddLog(LogServices.GetTypeLog("Add"), user, "Upload : " + fullPath + " By : " + user);
                    return Ok(new { fullPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        /// Rename file or/and update content by this class and name
        [HttpPost]
        [Route("update/{classe}/{name}/{newname}")]
        public async Task<IActionResult> Update(string classe, string name, string newName)
        {
            try
            {
                //String Classes = "B1";
                //String AncienneClasses = "B1";
                //String NewClasses = "B1";

                // Check if file exist
                var filePath = ""; 
                var ressourcePath = "";
                if (env.IsProduction())
                {
                    filePath = FolderPath + classe + '/' + name;
                    ressourcePath = FolderPath + classe;
                }
                else if (env.IsDevelopment())
                {
                    filePath = FolderPath + classe + '\\' + name;
                    ressourcePath = FolderPath + classe;
                }
                if (!System.IO.File.Exists(filePath))
                    return NotFound();
                if (String.IsNullOrWhiteSpace(newName)) newName = name;

                // Log 
                await LogServices.AddLog(LogServices.GetTypeLog("Update"), user, "Update : " + filePath + "To" + newName + " By : " + user);
                // Archive
                //await ArchiveServices.ArchiveFile(TypeArchives.Update, filePath, classe, user);

                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                // Check if there is new content to update
                if (file != null || file.Length > 0)
                {
                    String newFilePath = String.Empty;
                    // Check if there is new name to update
                    if (String.IsNullOrEmpty(newName))
                    {
                        newFilePath = filePath;
                    }
                    else
                    {
                        newFilePath = Path.Combine(ressourcePath, newName);
                    }
                     
                    using (var stream = new FileStream(newFilePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { newFilePath, file.Name });
                }
                else
                {
                    if (String.IsNullOrEmpty(newName))
                    {
                        return BadRequest("No Paremeters.");
                    }
                    System.IO.File.Delete(filePath);
                    var newFilePath =  Path.Combine(ressourcePath, newName);
                    System.IO.File.Move(filePath, newFilePath);
                    return Ok(new { newFilePath, newName });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }


        }
        /// <summary>
        /// For Test
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("hello")]
        public string Hello()
        {
            return "Hello World";
        }
        /// <summary>
        /// For Test 
        /// </summary>
        [HttpGet]
        [Route("dl2")]
        public void Dl2()
        {
            WebClient myWebClient = new WebClient();
            myWebClient.DownloadFileAsync(new Uri(FolderPath + "B2/ticket.pdf"), "ticket.pdf");
        }
        /// <summary>
        /// For Test 
        /// </summary>
        [HttpGet]
        [Route("getURL")]
        public string[] getURL_folderPath()
        {
            Console.WriteLine(Directory.GetFiles(FolderPath));
            return Directory.GetFiles(FolderPath);
        }

        //public List<FileOverride> Get()
        //{
        //    List<FileOverride> response = new List<FileOverride>();
        //    byte[] content;
        //    string[] filePaths = Directory.GetFiles(folderPath);
        //    string fileDefaultName;
        //    foreach (string path in filePaths.ToList())
        //    {
        //        fileDefaultName = path;
        //        FileInfo fi = new FileInfo(path);
        //        content = System.IO.File.ReadAllBytes(path);
        //        response.Add(new FileOverride(/*context,*/content, fi.Name, fi.Extension, null,fi.FullName));
        //    }
        //    Console.WriteLine(response);
        //    return response;
        //}
    }
}

