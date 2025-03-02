using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Entities;

namespace Repositories;

public partial class ComputersContext : DbContext
{
    public ComputersContext()
    {
    }

    public ComputersContext(DbContextOptions<ComputersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer("Server=srv2\\PUPILS;Database=Computers;Trusted_Connection=True;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.CategoryName)
                .HasMaxLength(40)
                .IsFixedLength();
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserOrder");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("OrderItem");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.Productid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductOrderItem");
        });


        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsFixedLength();
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(200)
                .IsFixedLength();
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsFixedLength();

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CategoryProduct");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsFixedLength();
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
