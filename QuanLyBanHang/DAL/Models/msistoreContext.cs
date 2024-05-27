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
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Orderitem> Orderitems { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Statusorder> Statusorders { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Userinfo> Userinfos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=msistore;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("brand", "msistoredb");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category", "msistoredb");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("feedback", "msistoredb");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment)
                    .HasMaxLength(200)
                    .HasColumnName("comment");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("createdAt");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__feedback__order___403A8C7D");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__feedback__produc__412EB0B6");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__feedback__user_i__4222D4EF");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("image", "msistoredb");

                entity.HasIndex(e => e.ProdcutId, "msistore_image_ProdcutId_20c1b923_fk_msistore_ProdcutId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.File).HasMaxLength(100);

                entity.HasOne(d => d.Prodcut)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.ProdcutId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("image$msistore_image_ProdcutId_20c1b923_fk_msistore_ProdcutId");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("location", "msistoredb");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(15);

                entity.Property(e => e.PostalCode).HasMaxLength(20);

                entity.Property(e => e.StoreName).HasMaxLength(100);

                entity.Property(e => e.Street).HasMaxLength(100);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order", "msistoredb");

                entity.HasIndex(e => e.UserId, "msistore_order_UserId_e1bb818f_fk_msistore_userinfo_UserId");

                entity.HasIndex(e => e.Uuid, "order$uuid")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt).HasColumnType("date");

                entity.Property(e => e.UpdatedAt).HasColumnType("date");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(32)
                    .HasColumnName("uuid")
                    .IsFixedLength();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("order$msistore_order_UserId_e1bb818f_fk_msistore_userinfo_UserId");
            });

            modelBuilder.Entity<Orderitem>(entity =>
            {
                entity.ToTable("orderitem", "msistoredb");

                entity.HasIndex(e => e.OrderId, "msistore_orderitem_OrderId_fd7cd6f3_fk_msistore_OrderId");

                entity.HasIndex(e => e.ProdcutId, "msistore_orderitem_ProdcutId_98f2f5c8_fk_msistore_ProdcutId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Orderitems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orderitem$msistore_orderitem_OrderId_fd7cd6f3_fk_msistore_OrderId");

                entity.HasOne(d => d.Prodcut)
                    .WithMany(p => p.Orderitems)
                    .HasForeignKey(d => d.ProdcutId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orderitem$msistore_orderitem_ProdcutId_98f2f5c8_fk_msistore_ProdcutId");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product", "msistoredb");

                entity.HasIndex(e => e.CategoryId, "msistore_product_CategoryId_fbae0197_fk_msistore_CategoryId");

                entity.HasIndex(e => e.BrandId, "msistore_product_brand_id_31330f4e_fk_msistore_brand_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BrandId).HasColumnName("brand_id");

                entity.Property(e => e.CreatedAt).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.NewPrice).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.OldPrice).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.UpdatedAt).HasColumnType("date");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("product$msistore_product_brand_id_31330f4e_fk_msistore_brand_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product$msistore_product_CategoryId_fbae0197_fk_msistore_CategoryId");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role", "msistoredb");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasMaxLength(20);
            });

            modelBuilder.Entity<Statusorder>(entity =>
            {
                entity.ToTable("statusorder", "msistoredb");

                entity.HasIndex(e => e.OrderId, "msistore_statusorder_OrderId_2a6131fd_fk_msistore_OrderId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt).HasColumnType("date");

                entity.Property(e => e.DeliveryMethod).HasMaxLength(50);

                entity.Property(e => e.DeliveryStage).HasMaxLength(50);

                entity.Property(e => e.PaymentMethod).HasMaxLength(50);

                entity.Property(e => e.UpdatedAt).HasColumnType("date");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Statusorders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("statusorder$msistore_statusorder_OrderId_2a6131fd_fk_msistore_OrderId");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user", "msistoredb");

                entity.HasIndex(e => e.Username, "UQ_Username")
                    .IsUnique();

                entity.HasIndex(e => e.RoleId, "msistore_user_RoleId_ebd2668b_fk_msistore_RoleId");

                entity.HasIndex(e => e.Username, "user$Username")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Avatar).HasMaxLength(100);

                entity.Property(e => e.DateJoined).HasPrecision(6);

                entity.Property(e => e.Email).HasMaxLength(254);

                entity.Property(e => e.FirstName).HasMaxLength(150);

                entity.Property(e => e.LastLogin).HasPrecision(6);

                entity.Property(e => e.LastName).HasMaxLength(150);

                entity.Property(e => e.Password)
                    .HasMaxLength(128)
                    .HasColumnName("password");

                entity.Property(e => e.Username).HasMaxLength(150);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user$msistore_user_RoleId_ebd2668b_fk_msistore_RoleId");
            });

            modelBuilder.Entity<Userinfo>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_userinfo_UserId");

                entity.ToTable("userinfo", "msistoredb");

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
                    .HasConstraintName("userinfo$msistore_userinfo_UserId_aec5a4e8_fk_msistore_UserId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
