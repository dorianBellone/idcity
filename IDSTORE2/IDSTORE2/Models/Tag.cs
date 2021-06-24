using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IDSTORE2.Models
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<File> Files { get; set; }
    }
}
