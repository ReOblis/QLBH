using Microsoft.EntityFrameworkCore;
using QLBH.Models;

namespace QLBH.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Novel> Novels { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<NovelGenre> NovelGenres { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        //cấu hình mô hình database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<NovelGenre>()
                .HasKey(ng => new { ng.NovelID, ng.GenreID });

            modelBuilder.Entity<Rating>()
                .HasKey(r => r.RatingID);

            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderID, od.NovelID });

            modelBuilder.Entity<CartDetail>()
                .HasKey(cd => cd.CartDetailID);

            // Các quan hệ khóa ngoại
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.UserID);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Novel)
                .WithMany(n => n.Ratings)
                .HasForeignKey(r => r.NovelID);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserID);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderID);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Novel)
                .WithMany(n => n.OrderDetails)
                .HasForeignKey(od => od.NovelID);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.UserID);

            modelBuilder.Entity<CartDetail>()
                .HasOne(cd => cd.Cart)
                .WithMany(c => c.CartDetails)
                .HasForeignKey(cd => cd.CartID);

            modelBuilder.Entity<CartDetail>()
                .HasOne(cd => cd.Novel)
                .WithMany(n => n.CartDetails)
                .HasForeignKey(cd => cd.NovelID);

            modelBuilder.Entity<NovelGenre>()
                .HasOne(ng => ng.Novel)
                .WithMany(n => n.NovelGenres)
                .HasForeignKey(ng => ng.NovelID);

            modelBuilder.Entity<NovelGenre>()
                .HasOne(ng => ng.Genre)
                .WithMany(g => g.NovelGenres)
                .HasForeignKey(ng => ng.GenreID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
