using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using praktika_desktop_dotNET7.Models.Entities;

namespace praktika_desktop_dotNET7.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServicePhoto> ServicePhotos { get; set; }
        public DbSet<AspNetUsers> AspNetUsers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Service>()
                .Ignore(s => s.ServiceImage); // Явно указываем EF игнорировать это свойство
        }
    }
}
