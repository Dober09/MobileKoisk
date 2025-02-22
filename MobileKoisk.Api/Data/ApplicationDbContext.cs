using Microsoft.EntityFrameworkCore;
using MobileKoisk.Api.Models;

namespace MobileKoisk.Api.Data
{
    public class ApplicationDbContext  : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<UserData> Users { get; set; }


        protected override void  OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserData>()
                .HasIndex(u => u.Email).IsUnique();
        }
    }
}
