using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IDSTORE2.Models
{
    public class File
    {
        [Key]
        public Guid FileId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Path { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public ICollection<Tag> Tags { get; set; }
   
    }
}
