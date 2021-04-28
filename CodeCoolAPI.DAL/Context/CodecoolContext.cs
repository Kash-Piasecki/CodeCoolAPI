using CodeCoolAPI.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeCoolAPI.DAL.Context
{
    public class CodecoolContext : IdentityDbContext
    {
        public CodecoolContext(DbContextOptions<CodecoolContext> options) : base (options)
        {
            
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialType> MaterialTypes { get; set; }
        public DbSet<Review> Reviews { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Author>().HasMany(x => x.Materials).WithOne(x => x.Author)
                .HasForeignKey(x => x.AuthorId);
            modelBuilder.Entity<Material>().HasMany(x => x.Reviews).WithOne(x => x.Material)
                .HasForeignKey(x => x.MaterialId);
            modelBuilder.Entity<Material>().HasOne(x => x.MaterialType).WithMany(x => x.Materials)
                .HasForeignKey(x => x.MaterialTypeId);
            this.SeedDatabase(modelBuilder);
        }
    }
}