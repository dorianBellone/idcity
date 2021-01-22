using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IDSTORE2.Models
{
    public class File
    {
        public Guid FileId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Tag> Tags { get; } = new List<Tag>();
   
    }
}
