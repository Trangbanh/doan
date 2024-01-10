using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Fruit.Base.Models
{
    public partial class fruitkhaContext : DbContext
    {
        public fruitkhaContext()
        {
        }

        public fruitkhaContext(DbContextOptions<fruitkhaContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Bill> Bills { get; set; } = null!;
        public virtual DbSet<Blog> Blogs { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<DetailedInvoice> DetailedInvoices { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductType> ProductTypes { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Contacts> Contacts { get; set; } = null!;  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");
                entity.Property(e => e.Id);
                entity.Property(e => e.FullName).HasMaxLength(500);
                entity.Property(e => e.Phone).HasMaxLength(50);
                entity.Property(e => e.Address).HasMaxLength(500);
                entity.Property(e => e.Username).HasMaxLength(500);
                entity.Property(e => e.Password).HasMaxLength(500);
            });
            modelBuilder.Entity<Bill>(entity =>
            {
                entity.HasKey(e => e.Idbill);

                entity.ToTable("Bill");

                entity.Property(e => e.Idbill)
                    .ValueGeneratedNever()
                    .HasColumnName("IDBill");

                entity.Property(e => e.Address).HasMaxLength(300);

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UseId).HasColumnName("UseID");

                entity.HasOne(d => d.Use)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.UseId)
                    .HasConstraintName("FK_Bill_User");
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.ToTable("Blog");
                entity.HasKey(e => e.Idblog);

                entity.Property(e => e.Contents);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.ModifyDate).HasColumnType("date");

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.Title).HasMaxLength(500);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.Idcomment);

                entity.ToTable("Comment");

                entity.Property(e => e.Idcomment)
                    .HasColumnName("IDComment");

                entity.Property(e => e.CommentContent).HasMaxLength(1000);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Idblog).HasColumnName("IDBlog");

                entity.Property(e => e.Idcommentator).HasColumnName("IDCommentator");
            });

            modelBuilder.Entity<DetailedInvoice>(entity =>
            {
                entity.ToTable("DetailedInvoice");

                entity.Property(e => e.Id)
                    .HasColumnName("ID");

                entity.Property(e => e.Idbill).HasColumnName("IDBill");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.TotalPrice).HasColumnType("money");

                entity.HasOne(d => d.IdbillNavigation)
                    .WithMany(p => p.DetailedInvoices)
                    .HasForeignKey(d => d.Idbill)
                    .HasConstraintName("FK_DetailedInvoice_Bill");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.DetailedInvoices)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_DetailedInvoice_Product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID");

                entity.Property(e => e.Description);

                entity.Property(e => e.Idtype).HasColumnName("IDType");

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.Price).HasColumnType("decimal(18,0)");

                entity.Property(e => e.ProductName).HasMaxLength(500);

                entity.Property(e => e.PromotionalPrice).HasColumnType("decimal(18,0)");

                entity.HasOne(d => d.IdtypeNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Idtype)
                    .HasConstraintName("FK_Product_ProductType");
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.HasKey(e => e.Idtype);

                entity.ToTable("ProductType");

                entity.Property(e => e.Idtype)
                    .HasColumnName("IDType");
                entity.Property(e => e.Images).HasMaxLength(500);
                entity.Property(e => e.TypeName).HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Idrole);

                entity.ToTable("Role");

                entity.Property(e => e.Idrole)
                    .ValueGeneratedNever()
                    .HasColumnName("IDRole");

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UseId);

                entity.ToTable("User");

                entity.Property(e => e.UseId)
                    .HasColumnName("UseID");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Password).HasMaxLength(500);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UseName).HasMaxLength(500);
            });
            modelBuilder.Entity<Contacts>(entity =>
            {
                entity.HasKey(e => e.ContactId);

                entity.ToTable("Contacts");
                entity.Property(x => x.Name).HasMaxLength(500);
                entity.Property(x => x.Email).HasMaxLength(150);
                entity.Property(x => x.Phone).HasMaxLength(50);
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
