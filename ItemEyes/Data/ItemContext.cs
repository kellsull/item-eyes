using Microsoft.EntityFrameworkCore;
using ItemEyes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemEyes.Data
{
    public class ItemContext : DbContext
    {
        public ItemContext(DbContextOptions<ItemContext> options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().Ignore(i => i.Location);
            modelBuilder.Entity<Location>().Ignore(l => l.Items);

            modelBuilder.Entity<Item>().ToTable("Item");
            modelBuilder.Entity<Location>().ToTable("Location");

            modelBuilder.Entity<Item>()
                .HasOne(i => i.Location)
                .WithMany(l => l.Items);

            modelBuilder.Entity<Item>()
                .Property(i => i.Id)
                .UseIdentityColumn(seed: 1, increment: 1);

            modelBuilder.Entity<Item>()
                .Property(i => i.LocationId)
                .HasDefaultValue(1);

        }
    }
}
