using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IDSTORE2.Models
{
    public class TypeLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeLogId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Log> Log { get; set; }
    }
}
//public enum TypeLog
//{
//    Add = 0,
//    Get = 1,
//    Update = 2,
//    Delete = 3,
//    Rename = 4,
//    ArchivesFileDelete = 5,
//    ArchivesFileUpdate = 6,
//};