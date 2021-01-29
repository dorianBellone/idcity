using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IDSTORE2.Models
{
    public class File
    {
        public Guid FileId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public List<Tag> Tags { get; } = new List<Tag>();
   
    }
}
