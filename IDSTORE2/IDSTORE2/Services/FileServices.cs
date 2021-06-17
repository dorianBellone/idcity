using IDSTORE2.Models;
using Microsoft.AspNetCore.StaticFiles;
using System.Collections.Generic;
using System.IO;

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
                FileInfo fi = new FileInfo(path);
                //content = System.IO.File.ReadAllBytes(path);

                var _file = new Models.File();
                _file.Name = _file.Name;
                _file.Path = fi.FullName;
                _file.Type = fi.Extension;
                response.Add(_file);
            }
            return response;
        }
    }
}
