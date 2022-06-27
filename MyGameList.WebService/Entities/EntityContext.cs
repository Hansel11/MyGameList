using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyGameList.WebService;

namespace MyGameList.Entities
{
    public class EntityContext<T> : DbContext where T : class
    {
        public DbSet<T> Data { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                if (!optionsBuilder.IsConfigured)
                {
                    var parent = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
                    ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                    string path = Path.Combine(parent, "appsettings.json");
                    configurationBuilder.AddJsonFile(path, optional: false);
                    IConfigurationRoot configurationRoot = configurationBuilder.Build();

                    var ConnectionString = configurationRoot.GetSection("ConnectionStrings").GetSection("PsdDB").Value;
                    optionsBuilder.UseSqlite(ConnectionString);
                }
            }
            catch (Exception ex)
            {
                var s = ex;
            }
        }
    }
}