using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDSTORE2.Models
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options)
        {
        }

        public DbSet<File> Files { get; set; }
        public DbSet<Tag> Tags { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //  => options.UseSqlite("Data Source=DB_API_IDSTORE.db");
    }
}
