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
    public class TagController : ControllerBase
    {
        private string FolderPath { get; set; }

        private readonly APIContext Context;
        private readonly TagServices TagServices;

        private readonly LogServices LogServices;
        private readonly ArchivesServices ArchiveServices;

        private readonly ILogger<TagController> logger;
        private readonly IConfiguration config;
        private IWebHostEnvironment env;
        private String user;
        public TagController(ILogger<TagController> _logger, APIContext _context, IConfiguration _config, IWebHostEnvironment _env, LogServices _logservice, TagServices _tagServices)
        {
            logger = _logger;
            Context = _context;

            TagServices = _tagServices;







            //LogServices = _logservice;
            //config = _config;
            //if (User == null) user = "null";
            //else if (User.Identity.Name == null) user = "null";
            //else user = User.Identity.Name;
            //if (String.IsNullOrWhiteSpace(user)) user = "null";
            //env = _env;
            //if (env.IsProduction())
            //{
            //    string PathProd = config.GetSection("PathFile").GetSection("PathFileProd").Value;
            //    FolderPath = PathProd;
            //}
            //else if (env.IsDevelopment())
            //{
            //    string PathDev = config.GetSection("PathFile").GetSection("PathFileDev").Value;
            //    FolderPath = PathDev;
            //}
        }

        [HttpGet]
        public async Task<string[]> Get()
        {
            return await TagServices.GetAllTagName();
        }
        [HttpGet]
        [Route("add/{newtag}/{description}")]
        public async Task<Boolean> Get(string newtag, string description)
        {
            return await TagServices.AddTag(newtag, description);
        }
    }
}

