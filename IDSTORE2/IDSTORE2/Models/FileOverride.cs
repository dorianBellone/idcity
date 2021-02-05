using System.Linq;

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

        public FileOverride(APIContext _context, byte[] content, string _name, string _type = null, string _description = null, string _path = null)
        {
            context = _context;
            if (content != null)
            {
                Content = content;
            }
            if (_name != null)
            {
                Name = _name;
                // Test Pour voir si on retrouve bien le fichier en paramétre dans la DB. a voir plus tard si on ajoute le path et type dans la table File  
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
            if (_path != null)
            {
                Path = _path;
            }

        }
        public byte[] Content { get; set; }
    }
}

