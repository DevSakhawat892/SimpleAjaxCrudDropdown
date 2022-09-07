﻿using Microsoft.EntityFrameworkCore;
using SimpleCrudDropdown.Models;

namespace SimpleCrudDropdown.DAL
{
   public class DataContext : DbContext
   {
      public DataContext(DbContextOptions<DataContext> options) : base(options)
      {
      }

      public DbSet<Customer> Customers { get; set; }
      public DbSet<Product> Products { get; set; }

      protected override void OnModelCreating(ModelBuilder builder)
      {
         builder.Entity<Customer>(entity =>
         {
            entity.HasIndex(c => c.Name).IsUnique();
         });
      }
   }
}
