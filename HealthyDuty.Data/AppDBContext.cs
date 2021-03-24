using HealthyDuty.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyDuty.Data
{
    public class AppDBContext : DbContext
    {

        private IConfiguration _config;

        public AppDBContext(IConfiguration config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config.GetConnectionString("AppDbConnectionString"));
            }
        }

        //entities
        public DbSet<Auth> Auth { get; set; }
        public DbSet<BloodGroup> BloodGroup { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Hospital> Hospital { get; set; }
        public DbSet<NeedForBlood> NeedForBlood { get; set; }
        public DbSet<Personnel> Personnel { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<ProfileDetail> ProfileDetail { get; set; }
        public DbSet<ProfilePersonnel> ProfilePersonnel { get; set; }
        public DbSet<Sex> Sex { get; set; }
        public DbSet<User> User { get; set; }

    }
}
