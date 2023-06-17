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
        public DbSet<User> Users { get; set; }

        public DbSet<Request> Requests { get; set; }

        public DbSet<Result> Results { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseNpgsql("User ID = postgres;Password=admin;Server=localhost;Port=5433;Database=bitirme;Integrated Security=true;Pooling=true;");
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


            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("Requests");

                entity.HasKey(e => e.RequestId);
                entity.Property(e => e.RequestId).HasColumnName("RequestId").IsRequired();
                entity.Property(e => e.Answers).HasColumnName("Answers").HasMaxLength(255);
                entity.Property(e => e.UserId).HasColumnName("UserId").IsRequired();

                entity.HasOne(e => e.User)
                    .WithMany(e => e.Requests)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.ToTable("Results");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("Id").IsRequired();
                entity.Property(e => e.Protocol).HasColumnName("Protocol").IsRequired();
                entity.Property(e => e.RequestId).HasColumnName("RequestId").IsRequired();

                entity.HasOne(e => e.Request)
                    .WithOne(e => e.Result)
                    .HasForeignKey<Result>(e => e.RequestId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}
