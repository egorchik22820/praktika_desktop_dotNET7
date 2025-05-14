using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using praktika_desktop_dotNET7.Models;

namespace RecepiesMODULS.AppData
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb; Database=MyCompany; Persist Security Info=False; MultipleActiveResultSets=true; Trusted_Connection=True; TrustServerCertificate=True;");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
