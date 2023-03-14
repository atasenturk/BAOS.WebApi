using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAOS.Web.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BAOS.Web.Data
{
    public class BAOSDbContext : DbContext
    {
        public BAOSDbContext(DbContextOptions options) : base(options)
        {

        }
        private DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Source=(localdb)\\ProjectModels;Initial Catalog=BaosDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.Property(q => q.Id).UseIdentityColumn();
                entity.Property(q => q.UserName).HasMaxLength(20).IsRequired(true);
                entity.Property(q => q.Email).HasMaxLength(40).IsRequired(true);
                entity.Property(q => q.Password).IsRequired(true);
            });

        }
    }
}
