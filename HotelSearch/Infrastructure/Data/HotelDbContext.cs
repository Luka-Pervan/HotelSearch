using Microsoft.EntityFrameworkCore;
using HotelSearch.Core.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace HotelSearch.Infrastructure.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }
        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity properties if needed
            modelBuilder.Entity<Hotel>()
                .HasKey(h => h.Id);

            // Additional configurations
        }
    }
}
