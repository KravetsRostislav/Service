using System;
using Microsoft.EntityFrameworkCore;
using Shop.API.Domain.Models;

namespace Shop.API.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Good> Goods { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            #region Users

            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(x => x.Id);
            builder.Entity<User>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(x => x.Firstname).IsRequired().HasMaxLength(30);
            builder.Entity<User>().Property(x => x.Lastname).IsRequired().HasMaxLength(30);
            builder.Entity<User>().Property(x => x.Login).IsRequired().HasMaxLength(30);
            builder.Entity<User>().HasAlternateKey(x => x.Login);
            builder.Entity<User>().Property(x => x.Password).IsRequired().HasMaxLength(30);
            builder.Entity<User>().HasMany(x => x.UserRoles).WithOne(x => x.User);

            #endregion


            #region Admins

            builder.Entity<Admin>().ToTable("Admins");
            builder.Entity<Admin>().HasKey(x => x.Id);
            builder.Entity<Admin>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Admin>().Property(x => x.Firstname).IsRequired().HasMaxLength(30);
            builder.Entity<Admin>().Property(x => x.Lastname).IsRequired().HasMaxLength(30);
            builder.Entity<Admin>().Property(x => x.Login).IsRequired().HasMaxLength(30);
            builder.Entity<Admin>().HasAlternateKey(x => x.Login);
            builder.Entity<Admin>().Property(x => x.Password).IsRequired().HasMaxLength(30);
            builder.Entity<Admin>().HasMany(x => x.UserRoles).WithOne(x => x.User);

            #endregion


            #region Roles

            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<Role>().HasKey(x => x.Id);
            builder.Entity<Role>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Role>().Property(x => x.Name).IsRequired();
            builder.Entity<Role>().HasMany(x => x.UserRoles).WithOne(x => x.Role);

            #endregion

            #region UserRoles

            builder.Entity<UserRole>().ToTable("UserRoles");
            builder.Entity<UserRole>().HasKey(x => new { x.RoleId, x.UserId });

            #endregion

            builder.Entity<Role>().HasData
            (
                new Role { Id = 1000, Name = "driver" },
                new Role { Id = 1005, Name = "BigBoss" },
                new Role { Id = 1001, Name = "lexus" }
            );


            builder.Entity<User>().HasData
            (
                new User
                {
                    Id = 777,
                    Firstname = "Кравец",
                    Lastname = "Ростислав",
                    Login = "driver",
                    Password = "driver"
                },
                new User
                {
                    Id = 666,
                    Firstname = "Гурич",
                    Lastname = "Виталий",
                    Login = "lexus",
                    Password = "lexus"
                }
            );

            builder.Entity<UserRole>().HasData
            (
                new UserRole { UserId = 777, RoleId = 1000 },
                new UserRole { UserId = 666, RoleId = 1001 },
                new UserRole { UserId = 777, RoleId = 1005 }
            );


            builder.Entity<Category>().ToTable("Categories");
            builder.Entity<Category>().HasKey(x => x.Id);
            builder.Entity<Category>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Category>().Property(x => x.Name).IsRequired().HasMaxLength(50);

            builder.Entity<Category>().HasData
            (
                new Category { Id = 10000, Name = "Notebook" },
                new Category { Id = 10001, Name = "Smartphone" }
            );

            builder.Entity<Good>().ToTable("Goods");
            builder.Entity<Good>().HasKey(x => x.Id);
            builder.Entity<Good>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Good>().Property(x => x.GoodName).IsRequired().HasMaxLength(50);



        }

    }
}