using Microsoft.EntityFrameworkCore;
using Selu383.SP25.Api.Entities;

namespace Selu383.SP25.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

       public DbSet<Theater> Theaters { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Theater>().HasData(
                new Theater { Id = 1, Name = "Grand Cinema", Address = "123 Main St", SeatCount = 200 },
                new Theater { Id = 2, Name = "Regal Theater", Address = "456 Elm St", SeatCount = 150 },
                new Theater { Id = 3, Name = "Majestic Movies", Address = "789 Oak St", SeatCount = 300 }
            );
        }
    }
}
