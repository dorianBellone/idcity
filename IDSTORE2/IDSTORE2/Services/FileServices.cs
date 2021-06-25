using IDSTORE2.Models;
using Microsoft.AspNetCore.StaticFiles;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IDSTORE2.Services
{

    public static class FileServices
    {
        public static string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/pdf";
            }
            return contentType;
        }
        public static IEnumerable<Models.File> GetAllFile(string path)
        {
            List<Models.File> response = new List<Models.File>();
            //byte[] content;
            string[] filePaths = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            foreach (string _path in filePaths)
            {
                if (_path.Contains("Archives")) continue;
                FileInfo fi = new FileInfo(path);
                //content = System.IO.File.ReadAllBytes(path);

                var _file = new Models.File();
                char slashEnv;
                if (path.Contains('\\')) slashEnv = '\\';
                else slashEnv = '/';
                _file.Name = _path.Split(slashEnv).ToList().Last();
                _file.Path = _path;
                _file.Type = _path.Split(slashEnv).ToList().Last().Split('.').Last();
                _file.Tags = new List<Tag>() { new Tag() { Name = GetTagByPath(_path) } };
                response.Add(_file);
            }
            return response;
        }
        public static string GetTagByPath(string path)
        {
            if (!path.Contains('-')) return null;
            char slashEnv;
            if (path.Contains('\\')) slashEnv = '\\';
            else slashEnv = '/';
            string defaultTag = path.Split(slashEnv).Last().Split('-').First();

            if (defaultTag.All(char.IsUpper) && defaultTag.Length > 0 && defaultTag.Length <= 10) return defaultTag;
            else return null;
        }
    }
}
