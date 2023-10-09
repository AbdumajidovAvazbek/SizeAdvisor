using Microsoft.EntityFrameworkCore;
using SizeAdvisor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SizeAdvisor.Data.DbContexts
{
    internal class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server = localhost; port = 5432; database = SizeAdvisor; User Id = postgres; password = 123456@avbek");
        }
    }
}
