using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyLifeStyle.Types.Repository
{
    //Entity frameworkCore paketlerini indirmeyi unutma.
    public class HealthyLifeStyleDbContext:DbContext
    {
        public DbSet<BloodGroup> BloodGroup { get; set; }

        public HealthyLifeStyleDbContext(DbContextOptions<HealthyLifeStyleDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Fiziksel DB nerede olacak? ConnectionString bilgisini burada verebilirsiniz.
            //optionsBuilder.UseSqlServer("Data Source=(localdb)\\Mssqllocaldb;Initial Catalog=turkcellShopDb;Integrated Security=True");

            //fakat Program.cs içerisinde optionBuilder'dada ekleyebilriiz.s
            base.OnConfiguring(optionsBuilder);
        }

    }
}
