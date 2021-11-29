﻿using Microsoft.EntityFrameworkCore;
using TitanPass.PasswordManager.DB.Entities;

namespace TitanPass.PasswordManager.DB
{
    public class PasswordManagerDbContext : DbContext
    {
        public DbSet<AccountEntity> Accounts { get; set; }
        
        public DbSet<CustomerEntity> Customers { get; set; }
        
        public DbSet<GroupEntity> Groups { get; set; }

        public PasswordManagerDbContext(DbContextOptions<PasswordManagerDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerEntity>().HasData(new CustomerEntity
            {
                Id = 1,
                Email = "test@gmail.com"
            });

            modelBuilder.Entity<GroupEntity>().HasData(new GroupEntity
            {
                Id = 1,
                Name = "Work"
            });

            modelBuilder.Entity<GroupEntity>().HasData(new GroupEntity
            {
                Id = 2,
                Name = "Social Media"
            });
            
            modelBuilder.Entity<AccountEntity>().HasData(new AccountEntity
            {
                Id = 1,
                Name = "Moodle",
                Email = "test@gmail.com",
                CustomerId = 1,
                GroupId = 1
            });

            modelBuilder.Entity<AccountEntity>().HasData(new AccountEntity
            {
                Id = 2,
                Name = "Facebook",
                Email = "test@gmail.com",
                CustomerId = 1,
                GroupId = 2
            });
            
            modelBuilder.Entity<AccountEntity>()
                .HasOne(account => account.Customer).WithMany()
                .HasForeignKey(entity => new {entity.CustomerId});

            modelBuilder.Entity<AccountEntity>()
                .HasOne(account => account.Group).WithMany()
                .HasForeignKey(entity => new {entity.GroupId});
        }
    }
}