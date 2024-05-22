using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL.Models
{
    public partial class msistoreContext : DbContext
    {
        public msistoreContext()
        {
        }

        public msistoreContext(DbContextOptions<msistoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Brand> Brands { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Like> Likes { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderItem> OrderItems { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<StatusOrder> StatusOrders { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Userinfo> Userinfos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-PBPP8VB\\SQLEXPRESS;Initial Catalog=msistore;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brand", "msistore");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category", "msistore");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Image", "msistore");

                entity.HasIndex(e => e.ProductId, "msistore_image_ProductID_20c1b923_fk_msistore_ProductID");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.File).HasMaxLength(100);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("msistore_image$msistore_image_ProductID_20c1b923_fk_msistore_ProductID");
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.ToTable("Like", "msistore");

                entity.HasIndex(e => e.ProductId, "msistore_like_ProductID_0e7dcd5d_fk_msistore_ProductID");

                entity.HasIndex(e => e.UserId, "msistore_like_UserId_5a93f994_fk_msistore_userinfo_UserId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("msistore_like$msistore_like_ProductID_0e7dcd5d_fk_msistore_ProductID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("msistore_like$msistore_like_UserId_5a93f994_fk_msistore_userinfo_UserId");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order", "msistore");

                entity.HasIndex(e => e.Uuid, "msistore_order$uuid")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "msistore_order_UserId_e1bb818f_fk_msistore_userinfo_UserId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateAt).HasColumnType("date");

                entity.Property(e => e.UpdatedAt).HasColumnType("date");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(32)
                    .HasColumnName("uuid")
                    .IsFixedLength();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("msistore_order$msistore_order_UserId_e1bb818f_fk_msistore_userinfo_UserId");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItem", "msistore");

                entity.HasIndex(e => e.OrderId, "msistore_orderitem_OrderId_fd7cd6f3_fk_msistore_OrderId");

                entity.HasIndex(e => e.ProductId, "msistore_orderitem_ProductID_98f2f5c8_fk_msistore_ProductID");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasMaxLength(3);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("msistore_orderitem$msistore_orderitem_OrderId_fd7cd6f3_fk_msistore_OrderId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("msistore_orderitem$msistore_orderitem_ProductID_98f2f5c8_fk_msistore_ProductID");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product", "msistore");

                entity.HasIndex(e => e.BrandId, "msistore_product_BrandId_31330f4e_fk_msistore_BrandId");

                entity.HasIndex(e => e.CategoryId, "msistore_product_CategoryId_fbae0197_fk_msistore_CategoryId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateAt).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.NewPrice).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.OldPrice).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.UpdatedAt).HasColumnType("date");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("msistore_product$msistore_product_BrandId_31330f4e_fk_msistore_BrandId");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("msistore_product$msistore_product_CategoryId_fbae0197_fk_msistore_CategoryId");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role", "msistore");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasMaxLength(20);
            });

            modelBuilder.Entity<StatusOrder>(entity =>
            {
                entity.ToTable("StatusOrder", "msistore");

                entity.HasIndex(e => e.OrderId, "msistore_statusorder_OrderId_2a6131fd_fk_msistore_OrderId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateAt).HasColumnType("date");

                entity.Property(e => e.DeliveryMethod).HasMaxLength(50);

                entity.Property(e => e.DeliveryStage).HasMaxLength(50);

                entity.Property(e => e.PaymentMethod).HasMaxLength(50);

                entity.Property(e => e.UpdatedAt).HasColumnType("date");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.StatusOrders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("msistore_statusorder$msistore_statusorder_OrderId_2a6131fd_fk_msistore_OrderId");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "msistore");

                entity.HasIndex(e => e.UserName, "msistore_user$userName")
                    .IsUnique();

                entity.HasIndex(e => e.RoleId, "msistore_user_RoleId_ebd2668b_fk_msistore_RoleId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Avatar).HasMaxLength(100);

                entity.Property(e => e.DateJoined).HasPrecision(6);

                entity.Property(e => e.Email).HasMaxLength(254);

                entity.Property(e => e.FirstName).HasMaxLength(150);

                entity.Property(e => e.LastLogin).HasPrecision(6);

                entity.Property(e => e.LastName).HasMaxLength(150);

                entity.Property(e => e.Password).HasMaxLength(128);

                entity.Property(e => e.UserName)
                    .HasMaxLength(150)
                    .HasColumnName("userName");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("msistore_user$msistore_user_RoleId_ebd2668b_fk_msistore_RoleId");
            });

            modelBuilder.Entity<Userinfo>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_msistore_userinfo_UserId");

                entity.ToTable("Userinfo", "msistore");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .HasColumnName("city");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .HasColumnName("country");

                entity.Property(e => e.HomeNumber).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(10);

                entity.Property(e => e.Street)
                    .HasMaxLength(50)
                    .HasColumnName("street");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Userinfo)
                    .HasForeignKey<Userinfo>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("msistore_userinfo$msistore_userinfo_UserId_aec5a4e8_fk_msistore_UserId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
