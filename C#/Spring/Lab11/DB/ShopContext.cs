using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM
{
    internal class ShopContext : DbContext
    {
        public ShopContext() : base()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=Fedosdekudrille;Database=Fivochcka;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        public DbSet<Good> Goods { get; set; }
    }
}
