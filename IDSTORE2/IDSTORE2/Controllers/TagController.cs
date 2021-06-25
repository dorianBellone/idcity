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

        private readonly IConfiguration Config;
        private String user;
        private String modeLog;
        public TagController(APIContext _context, IConfiguration _config, LogServices _logservice, TagServices _tagServices)
        {
            Context = _context;
            TagServices = _tagServices;
            LogServices = _logservice;
            Config = _config;
            if (/*String.IsNullOrWhiteSpace(_user)|| */ User == null) user = "admin";
            modeLog = Config.GetSection("ModeLog").Value;
        }

        [HttpGet]
        public async Task<Tag[]> Get()
        {
            var typeLog = Context.TypeLog.FirstOrDefault(l => l.Name == "Get");
            if (modeLog == "complete")
            {
                await LogServices.AddLog(typeLog, user, "GetTag, By : " + user);
            }
            //return await TagServices.GetAllTagName();
            return TagServices.GetAllTag().Result.ToArray();

        }

        [HttpPost]
        [Route("add")]
        public async Task<Boolean> AddTag(Tag _tag)
        {
            var typeLog = Context.TypeLog.FirstOrDefault(l => l.Name == "Add");
            await LogServices.AddLog(typeLog, user, "AddTag : " + _tag.Name + ", By : " + user);
            return await TagServices.AddTag(_tag);
        }
    }
}

