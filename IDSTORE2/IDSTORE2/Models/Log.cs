using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IDSTORE2.Models
{
    public class Log
    {
        [Key]
        public int LogId { get; set; }
        [Required]
        public int TypeLogID { get; set; }
        public TypeLog TypeLog { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public String User { get; set; }
        [Required]
        public String Information { get; set; }
    }
}
