using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PRN221_Project_02.Models
{
    public partial class PRN221_Project_02Context : DbContext
    {
        public static PRN221_Project_02Context instance = new PRN221_Project_02Context();
        public PRN221_Project_02Context()
        {
            if (instance == null) instance = this;
        }


      

        public PRN221_Project_02Context(DbContextOptions<PRN221_Project_02Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Agent> Agents { get; set; } = null!;
        public virtual DbSet<Batch> Batches { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Inventory> Inventories { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<StockExport> StockExports { get; set; } = null!;
        public virtual DbSet<StockImport> StockImports { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;
        public virtual DbSet<TemperatureLog> TemperatureLogs { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Warehouse> Warehouses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

                if (!optionsBuilder.IsConfigured) { optionsBuilder.UseSqlServer(config.GetConnectionString("value")); }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agent>(entity =>
            {
                entity.Property(e => e.AgentId).HasColumnName("AgentID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.AgentName).HasMaxLength(100);

                entity.Property(e => e.ContactNumber).HasMaxLength(20);

                entity.Property(e => e.Email).HasMaxLength(100);
            });

            modelBuilder.Entity<Batch>(entity =>
            {
                entity.Property(e => e.BatchId).HasColumnName("BatchID");

                entity.Property(e => e.BatchNumber).HasMaxLength(50);

                entity.Property(e => e.ExpiryDate).HasColumnType("date");

                entity.Property(e => e.ManufactureDate).HasColumnType("date");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.RejectReason)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.UpdateDate).HasColumnType("date");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Batches__Product__412EB0B6");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK__Batches__Supplie__440B1D61");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Batches__UserID__4316F928");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.WarehouseId)
                    .HasConstraintName("FK__Batches__Warehou__4222D4EF");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName).HasMaxLength(255);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Categorie__Statu__2D27B809");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.WarehouseId)
                    .HasConstraintName("FK__Categorie__Wareh__2E1BDC42");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventory");

                entity.Property(e => e.InventoryId).HasColumnName("InventoryID");

                entity.Property(e => e.CheckedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExpiryDate).HasColumnType("date");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Temperature).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");

                entity.HasOne(d => d.CheckedByNavigation)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.CheckedBy)
                    .HasConstraintName("FK__Inventory__Check__5535A963");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventory__Produ__5441852A");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventory__Wareh__534D60F1");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExpiryDate).HasColumnType("date");

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ProductName).HasMaxLength(255);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Products__Catego__3C69FB99");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Products__Status__3D5E1FD2");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK__Products__Suppli__3E52440B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Products__UserID__3B75D760");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B616080F3F3C4")
                    .IsUnique();

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.HasIndex(e => e.StatusName, "UQ__Status__05E7698AD7E1FB9B")
                    .IsUnique();

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StatusName).HasMaxLength(50);
            });

            modelBuilder.Entity<StockExport>(entity =>
            {
                entity.HasKey(e => e.ExportId)
                    .HasName("PK__StockExp__E5C997A42396C8A5");

                entity.Property(e => e.ExportId).HasColumnName("ExportID");

                entity.Property(e => e.AgentId).HasColumnName("AgentID");

                entity.Property(e => e.MovementDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.StockExports)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK__StockExpo__Agent__114A936A");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.StockExports)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StockExpo__Produ__0E6E26BF");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.StockExports)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__StockExpo__Statu__123EB7A3");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.StockExports)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__StockExpo__UserI__0F624AF8");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.StockExports)
                    .HasForeignKey(d => d.WarehouseId)
                    .HasConstraintName("FK__StockExpo__Wareh__10566F31");
            });

            modelBuilder.Entity<StockImport>(entity =>
            {
                entity.HasKey(e => e.MovementId)
                    .HasName("PK__StockImp__D1822466FE703232");

                entity.Property(e => e.MovementId).HasColumnName("MovementID");

                entity.Property(e => e.MovementDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.StockImports)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StockImpo__Produ__06CD04F7");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.StockImports)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__StockImpo__Statu__0A9D95DB");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.StockImports)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK__StockImpo__Suppl__09A971A2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.StockImports)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__StockImpo__UserI__07C12930");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.StockImports)
                    .HasForeignKey(d => d.WarehouseId)
                    .HasConstraintName("FK__StockImpo__Wareh__08B54D69");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.ContactName).HasMaxLength(255);

                entity.Property(e => e.ContactPhone).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.SupplierName).HasMaxLength(255);

                entity.Property(e => e.Website).HasMaxLength(100);
            });

            modelBuilder.Entity<TemperatureLog>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("PK__Temperat__5E5499A8E6F3C691");

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.RecordedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RecordedTemperature).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TemperatureLogs)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Temperatu__Produ__4F7CD00D");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.TemperatureLogs)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Temperatu__Wareh__4E88ABD4");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username, "UQ__Users__536C85E4E87C85BF")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Users__A9D10534E79BEE9B")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.CodePwd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CodePWD");

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FullName).HasMaxLength(255);

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.Username).HasMaxLength(100);

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Users__RoleID__34C8D9D1");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Users__StatusID__35BCFE0A");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.WarehouseId)
                    .HasConstraintName("FK__Users__Warehouse__36B12243");
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");

                entity.Property(e => e.Location).HasMaxLength(255);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TemperatureRange).HasMaxLength(50);

                entity.Property(e => e.WarehouseName).HasMaxLength(100);

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Warehouses)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Warehouse__Statu__2A4B4B5E");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
