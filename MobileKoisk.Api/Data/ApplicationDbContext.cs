using Microsoft.EntityFrameworkCore;
using MobileKoisk.Api.Models;

namespace MobileKoisk.Api.Data
{
    public class ApplicationDbContext  : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<UserData> Users { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<ReceiptItem> ReceiptItems { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void  OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User configuration
            modelBuilder.Entity<UserData>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Surname).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.Email).IsUnique();
            });


            // Basket configuration
            modelBuilder.Entity<Basket>(entity =>
            {
                entity.HasKey(e => e.BasketId);
                entity.HasOne<UserData>()
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            //BasketItem configuration

            modelBuilder.Entity<BasketItem>(entity =>
            {
                entity.HasKey(e => e.Barcode);
                entity.Property(e => e.Price).HasColumnType("REAL");
                // Ignore Command properties as they shouldn't be stored
               
            });


            // Product configuration
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.barcode);
                entity.Property(e => e.selling_price).HasColumnType("REAL");
                entity.Property(e => e.item_description).IsRequired();
            });


            // Receipt configuration
            modelBuilder.Entity<Receipt>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TotalAmount).HasColumnType("REAL");
                entity.HasOne<UserData>()
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            // ReceiptItem configuration
            modelBuilder.Entity<ReceiptItem>(entity =>
            {
                entity.HasKey(e => e.ReceiptItemId);
                entity.Property(e => e.PriceAtPurchase).HasColumnType("REAL");
            });


            // Notification configuration
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne<UserData>()
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
