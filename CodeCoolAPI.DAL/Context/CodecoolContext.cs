using CodeCoolAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeCoolAPI.DAL.Context
{
    public class CodecoolContext : DbContext
    {
        public CodecoolContext(DbContextOptions<CodecoolContext> options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialType> MaterialTypes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Author>().HasMany(x => x.Materials).WithOne(x => x.Author)
                .HasForeignKey(x => x.AuthorId);
            modelBuilder.Entity<Material>().HasMany(x => x.Reviews).WithOne(x => x.Material)
                .HasForeignKey(x => x.MaterialId);
            modelBuilder.Entity<Material>().HasOne(x => x.MaterialType).WithMany(x => x.Materials)
                .HasForeignKey(x => x.MaterialTypeId);
            modelBuilder.Entity<User>().HasOne(x => x.UserRole).WithMany(x => x.Users)
                .HasForeignKey(x => x.UserRoleId);
            modelBuilder.Entity<Review>().HasOne(x => x.User).WithMany(x => x.Reviews)
                .HasForeignKey(x => x.UserId);
            this.SeedDatabase(modelBuilder);
        }
    }
}