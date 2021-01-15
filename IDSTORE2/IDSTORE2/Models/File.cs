using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDSTORE2.Models
{
    public class File
    {
        public Guid FileId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Tag> Tags { get; } = new List<Tag>();
    }
}
