using System;
using CodeCoolAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeCoolAPI.DAL.Context
{
    public static class CodecoolContextSeeder
    {
        public static void SeedDatabase(this CodecoolContext context, ModelBuilder modelBuilder)
        {
            SeedAuthors(modelBuilder);
            SeedMaterialTypes(modelBuilder);
            SeedMaterials(modelBuilder);
            SeedReviews(modelBuilder);
            SeedUserRoles(modelBuilder);
            // SeedUsers(modelBuilder);
        }

        private static void SeedUserRoles(ModelBuilder modelBuilder)
        {
            var adminRole = new UserRole()
            {
                Id = 1,
                Name = "Admin",
            };
            var userRole = new UserRole()
            {
                Id = 2,
                Name = "User",
            };
            modelBuilder.Entity<UserRole>().HasData(adminRole);
            modelBuilder.Entity<UserRole>().HasData(userRole);
        }
        
        private static void SeedUsers(ModelBuilder modelBuilder)
        {
            var admin = new User()
            {
                Id = 1,
                Email = "admin@mail.com",
                UserRoleId = 1,
            };
            var user = new User()
            {
                Id = 2,
                Email = "user@mail.com",
                UserRoleId = 2,
            };
            modelBuilder.Entity<User>().HasData(admin);
            modelBuilder.Entity<User>().HasData(user);
        }

        private static void SeedAuthors(ModelBuilder modelBuilder)
        {
            var author1 = new Author()
            {
                Id = 1,
                Name = "Jan Kowalski",
                Description = "Codecool teacher",
                Counter = 3,
            };
            var author2 = new Author()
            {
                Id = 2,
                Name = "Motorola Solutions",
                Description = "They don't produce phones.",
                Counter = 3,
            };

            modelBuilder.Entity<Author>().HasData(author1);
            modelBuilder.Entity<Author>().HasData(author2);
        }

        private static void SeedMaterialTypes(ModelBuilder modelBuilder)
        {
            var materialType1 = new MaterialType()
            {
                Id = 1,
                Name = "Video",
                Definition = "Step-by-step video guide"
            };
            var materialType2 = new MaterialType()
            {
                Id = 2,
                Name = "Tutorial",
                Definition = "Video guide with exercises"
            };
            var materialType3 = new MaterialType()
            {
                Id = 3,
                Name = "Text",
                Definition = "Written guide with extra resources"
            };
            var materialType4 = new MaterialType()
            {
                Id = 4,
                Name = "Binary code",
                Definition = "Guide readable only by CPU"
            };
            
            modelBuilder.Entity<MaterialType>().HasData(materialType1);
            modelBuilder.Entity<MaterialType>().HasData(materialType2);
            modelBuilder.Entity<MaterialType>().HasData(materialType3);
            modelBuilder.Entity<MaterialType>().HasData(materialType4);
        }
        
        private static void SeedMaterials(ModelBuilder modelBuilder)
        {
            var material1 = new Material()
            {
                Id = 1,
                AuthorId = 1,
                Description = "How to twerk",
                Location = "www.google.com",
                MaterialTypeId = 1,
                PublishTime = DateTime.Now,
            };
            
            var material2 = new Material()
            {
                Id = 2,
                AuthorId = 1,
                Description = "How to not twerk",
                Location = "www.google.com",
                MaterialTypeId = 2,
                PublishTime = DateTime.Now,
            };
            
            var material3 = new Material()
            { 
                Id = 3,
                AuthorId = 1,
                Description = "How to code",
                Location = "www.google.com",
                MaterialTypeId = 3,
                PublishTime = DateTime.Now,
            };
            
            var material4 = new Material()
            {
                Id = 4,
                AuthorId = 2,
                Description = "How not to code",
                Location = "www.google.com",
                MaterialTypeId = 4,
                PublishTime = DateTime.Now,
            };
            
            var material5 = new Material()
            {
                Id = 5,
                AuthorId = 2,
                Description = "How to sing",
                Location = "www.google.com",
                MaterialTypeId = 1,
                PublishTime = DateTime.Now,
            };
            
            var material6 = new Material()
            {
                Id = 6,
                AuthorId = 2,
                Description = "How not to sing",
                Location = "www.google.com",
                MaterialTypeId = 2,
                PublishTime = DateTime.Now,
            };
            
            modelBuilder.Entity<Material>().HasData(material1);
            modelBuilder.Entity<Material>().HasData(material2);
            modelBuilder.Entity<Material>().HasData(material3);
            modelBuilder.Entity<Material>().HasData(material4);
            modelBuilder.Entity<Material>().HasData(material5);
            modelBuilder.Entity<Material>().HasData(material6);

        }
        
        private static void SeedReviews(ModelBuilder modelBuilder)
        {
            var review1 = new Review()
            {
                Id = 1,
                MaterialId = 1,
                DigitBased = 2,
                TextBased = "Best stuff ever",
            };
            
            var review2 = new Review()
            {
                Id = 2,
                MaterialId = 1,
                DigitBased = 10,
                TextBased = "Best stuff ever",
            };
            
            var review3 = new Review()
            {
                Id = 3,
                MaterialId = 2,
                DigitBased = 5,
                TextBased = "Mediocre",
            };
            
            var review4 = new Review()
            {
                Id = 4,
                MaterialId = 3,
                DigitBased = 5,
                TextBased = "Mediocre",
            };
            
            var review5 = new Review()
            {
                Id = 5,
                MaterialId = 4,
                DigitBased = 1,
                TextBased = "Tl;Dr",
            };
            
            var review6 = new Review()
            {
                Id = 6,
                MaterialId = 5,
                DigitBased = 10,
                TextBased = "Tl;Dr",
            };
            
            modelBuilder.Entity<Review>().HasData(review1);
            modelBuilder.Entity<Review>().HasData(review2);
            modelBuilder.Entity<Review>().HasData(review3);
            modelBuilder.Entity<Review>().HasData(review4);
            modelBuilder.Entity<Review>().HasData(review5);
            modelBuilder.Entity<Review>().HasData(review6);
        }
    }
}