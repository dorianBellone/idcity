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
        private Byte[] Content { get; set; }
        private String Name { get; set; }

        private String Type { get; set; }
    }
}

