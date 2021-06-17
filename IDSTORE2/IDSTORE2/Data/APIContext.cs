using IDSTORE2.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace IDSTORE2.Data
{
    public class APIContext : DbContext
    {
        public APIContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<File> File { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<TypeLog> TypeLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>().ToTable("Tag");
            modelBuilder.Entity<File>().ToTable("File");
            modelBuilder.Entity<Log>().ToTable("Log");
            modelBuilder.Entity<TypeLog>().ToTable("TypeLog");
        }
    }
}
