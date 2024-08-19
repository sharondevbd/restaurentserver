using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Express_Cafe_Core.Models;

public partial class dbRestaurentContext : DbContext
{
    public dbRestaurentContext()
    {
    }

    public dbRestaurentContext(DbContextOptions<dbRestaurentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DailyMenu> DailyMenus { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeItem> RecipeItems { get; set; }

    public virtual DbSet<Requisition> Requisitions { get; set; }

    public virtual DbSet<SaleDetail> SaleDetails { get; set; }

    public virtual DbSet<SaleHeader> SaleHeaders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
			optionsBuilder.UseSqlServer("server=SHARONDEVBD\\SQLEXPRESS; Database=RestaurentDatabase; TrustServerCertificate=True; Trusted_Connection=True");
		}
	}
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
       
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       

        modelBuilder.Entity<DailyMenu>(entity =>
        {
            entity.HasIndex(e => e.RecipeId, "IX_DailyMenus_RecipeId");

            entity.Property(e => e.CookedQuantity).HasColumnType("int");
            entity.Property(e => e.DemandQuantity).HasColumnType("int");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ServingQuantity).HasColumnType("int");

            entity.HasOne(d => d.Recipe).WithMany(p => p.DailyMenus).HasForeignKey(d => d.RecipeId);
        });

        modelBuilder.Entity<RecipeItem>(entity =>
        {
            entity.HasIndex(e => e.ItemId, "IX_RecipeItems_ItemId");

            entity.HasIndex(e => e.RecipeId, "IX_RecipeItems_RecipeId");

            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Item).WithMany(p => p.RecipeItems).HasForeignKey(d => d.ItemId);

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeItems).HasForeignKey(d => d.RecipeId);
        });

        modelBuilder.Entity<Requisition>(entity =>
        {
            entity.HasIndex(e => e.ItemId, "IX_Requisitions_ItemId");

            entity.Property(e => e.RequestedBy).HasDefaultValue("");
            entity.Property(e => e.RequestedQuantity).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Item).WithMany(p => p.Requisitions).HasForeignKey(d => d.ItemId);
        });

        modelBuilder.Entity<SaleDetail>(entity =>
        {
            entity.HasIndex(e => e.DailyMenuId, "IX_SaleDetails_DailyMenuId");

            entity.HasIndex(e => e.SaleHeaderId, "IX_SaleDetails_SaleHeaderId");

            entity.HasOne(d => d.DailyMenu).WithMany(p => p.SaleDetails).HasForeignKey(d => d.DailyMenuId);

            entity.HasOne(d => d.SaleHeader).WithMany(p => p.SaleDetails).HasForeignKey(d => d.SaleHeaderId);
        });

        modelBuilder.Entity<SaleHeader>(entity =>
        {
            entity.ToTable("SaleHeader");

            entity.Property(e => e.TotalBill).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Vat)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("VAT");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
