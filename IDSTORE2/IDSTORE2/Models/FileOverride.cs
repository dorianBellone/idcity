using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDSTORE2.Models
{
    public class FileOverride : File
    {
        private readonly APIContext context;
        //public FileOverride(APIContext _context, byte[] content)
        //{
        //    context = _context;
        //    this.Content = content;
        //}

        public FileOverride(APIContext _context, byte[] content, string _name, string _type = null, string _description = null)
        {
            context = _context;
            if (content != null)
            {
                Content = content;
            }
            if (_name != null)
            {
                Name = _name;
                // Test Pour voir si on retrouve bien le fichier en paramétre dans la DB 
                if(context.Files.Count() != 0)
                {
                    var FileInDB = context.Files.FirstOrDefault(f => f.Name == Name);
                    if (FileInDB != null)
                    {
                        var toto = FileInDB;
                    }
                }
                
            }
            if (_type != null)
            {
                Type = _type;
            }
            if (_description != null)
            {
                Description = _description;
            }

        }
        public byte[] Content { get; set; }
        public string Type { get; set; }
    }
}

