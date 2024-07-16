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

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

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
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<DailyMenu>(entity =>
        {
            entity.HasIndex(e => e.RecipeId, "IX_DailyMenus_RecipeId");

            entity.Property(e => e.CookedQuantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DemandQuantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ServingQuantity).HasColumnType("decimal(18, 2)");

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
