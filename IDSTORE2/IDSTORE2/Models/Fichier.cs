using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDSTORE2.Models
{
    public class Fichier
    {
        public Fichier(byte[] content, string name, string type)
        {
            this.Content = content;
            this.Name = name;
            this.Type = type;
        }
        public byte[] Content { get; set; }
        public string Name { get; set; }

        public string Type { get; set; }
    }
}

