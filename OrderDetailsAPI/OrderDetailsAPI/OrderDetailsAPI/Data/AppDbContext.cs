using Microsoft.EntityFrameworkCore;
using OrderDetailsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderDetailsAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order_Item>()
                .HasKey(i => i.Id);
            modelBuilder.Entity<Order_Item>()
                .HasOne(i => i.Order)
                .WithMany(j => j.Order_Items)
                .HasForeignKey(k => k.OrderId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Order_Item>()
                .HasOne(i => i.Item)
                .WithMany(j => j.Order_Items)
                .HasForeignKey(k => k.ItemId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }

        public DbSet<Order> Orders{ get; set; }
        public DbSet<Item> Items{ get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order_Item> Order_Items{ get; set; }


    }
}
