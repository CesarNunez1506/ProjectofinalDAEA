using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BuysProduct> BuysProducts { get; set; }

    public virtual DbSet<CashSession> CashSessions { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Church> Churches { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Entrance> Entrances { get; set; }

    public virtual DbSet<FinancialReport> FinancialReports { get; set; }

    public virtual DbSet<GeneralExpense> GeneralExpenses { get; set; }

    public virtual DbSet<GeneralIncome> GeneralIncomes { get; set; }

    public virtual DbSet<IncomeChurch> IncomeChurches { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Lost> Losts { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<MonasteryExpense> MonasteryExpenses { get; set; }

    public virtual DbSet<Overhead> Overheads { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Place> Places { get; set; }

    public virtual DbSet<PlantProduction> PlantProductions { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductPurchased> ProductPurchaseds { get; set; }

    public virtual DbSet<Production> Productions { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RentChurch> RentChurches { get; set; }

    public virtual DbSet<Rental> Rentals { get; set; }

    public virtual DbSet<Resource> Resources { get; set; }

    public virtual DbSet<Return> Returns { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolesPermission> RolesPermissions { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SaleDetail> SaleDetails { get; set; }

    public virtual DbSet<SalesChannel> SalesChannels { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<TypePerson> TypePersons { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    public virtual DbSet<WarehouseMovementProduct> WarehouseMovementProducts { get; set; }

    public virtual DbSet<WarehouseMovementResource> WarehouseMovementResources { get; set; }

    public virtual DbSet<WarehouseProduct> WarehouseProducts { get; set; }

    public virtual DbSet<WarehouseResource> WarehouseResources { get; set; }

    public virtual DbSet<WarehouseStore> WarehouseStores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=local;Username=admin;Password=admin123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("enum_cash_sessions_status", new[] { "open", "closed" })
            .HasPostgresEnum("enum_financial_reports_status", new[] { "activo", "inactivo", "finalizado", "proceso" })
            .HasPostgresEnum("enum_recipes_unit", new[] { "g", "kg", "ml", "l", "unidades" });

        modelBuilder.Entity<BuysProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("buys_products_pkey");

            entity.ToTable("buys_products");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.EntryDate).HasColumnName("entry_date");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.TotalCost).HasColumnName("total_cost");
            entity.Property(e => e.UnitPrice).HasColumnName("unit_price");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
            entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");

            entity.HasOne(d => d.Product).WithMany(p => p.BuysProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("buys_products_product_id_fkey");

            entity.HasOne(d => d.Supplier).WithMany(p => p.BuysProducts)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("buys_products_supplier_id_fkey");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.BuysProducts)
                .HasForeignKey(d => d.WarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("buys_products_warehouse_id_fkey");
        });

        modelBuilder.Entity<CashSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cash_sessions_pkey");

            entity.ToTable("cash_sessions");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.EndAmount)
                .HasPrecision(10, 2)
                .HasColumnName("end_amount");
            entity.Property(e => e.EndedAt).HasColumnName("ended_at");
            entity.Property(e => e.StartAmount)
                .HasPrecision(10, 2)
                .HasColumnName("start_amount");
            entity.Property(e => e.StartedAt).HasColumnName("started_at");
            entity.Property(e => e.StoreId)
                .HasMaxLength(255)
                .HasColumnName("store_id");
            entity.Property(e => e.TotalReturns)
                .HasPrecision(10, 2)
                .HasColumnName("total_returns");
            entity.Property(e => e.TotalSales)
                .HasPrecision(10, 2)
                .HasColumnName("total_sales");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
            entity.Property(e => e.UserId)
                .HasMaxLength(255)
                .HasColumnName("user_id");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Church>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("churches_pkey");

            entity.ToTable("churches");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.State)
                .HasDefaultValue(true)
                .HasColumnName("state");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customers_pkey");

            entity.ToTable("customers");

            entity.HasIndex(e => e.Dni, "customers_dni_key").IsUnique();

            entity.HasIndex(e => e.Email, "customers_email_key").IsUnique();

            entity.HasIndex(e => e.Phone, "customers_phone_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.Dni).HasColumnName("dni");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("phone");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Entrance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("entrances_pkey");

            entity.ToTable("entrances");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.Free).HasColumnName("free");
            entity.Property(e => e.PaymentMethod).HasColumnName("payment_method");
            entity.Property(e => e.SaleChannel).HasColumnName("sale_channel");
            entity.Property(e => e.SaleDate)
                .HasMaxLength(255)
                .HasColumnName("sale_date");
            entity.Property(e => e.SaleNumber)
                .HasMaxLength(255)
                .HasColumnName("sale_number");
            entity.Property(e => e.TotalSale).HasColumnName("total_sale");
            entity.Property(e => e.TypePersonId).HasColumnName("type_person_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.PaymentMethodNavigation).WithMany(p => p.Entrances)
                .HasForeignKey(d => d.PaymentMethod)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("entrances_payment_method_fkey");

            entity.HasOne(d => d.SaleChannelNavigation).WithMany(p => p.Entrances)
                .HasForeignKey(d => d.SaleChannel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("entrances_sale_channel_fkey");

            entity.HasOne(d => d.TypePerson).WithMany(p => p.Entrances)
                .HasForeignKey(d => d.TypePersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("entrances_type_person_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Entrances)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("entrances_user_id_fkey");
        });

        modelBuilder.Entity<FinancialReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("financial_reports_pkey");

            entity.ToTable("financial_reports");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.NetProfit)
                .HasPrecision(10, 2)
                .HasColumnName("net_profit");
            entity.Property(e => e.Observations).HasColumnName("observations");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.TotalExpenses)
                .HasPrecision(10, 2)
                .HasColumnName("total_expenses");
            entity.Property(e => e.TotalIncome)
                .HasPrecision(10, 2)
                .HasColumnName("total_income");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
        });

        modelBuilder.Entity<GeneralExpense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("general_expenses_pkey");

            entity.ToTable("general_expenses");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ExpenseType)
                .HasMaxLength(100)
                .HasColumnName("expense_type");
            entity.Property(e => e.ModuleId).HasColumnName("module_id");
            entity.Property(e => e.ReportId).HasColumnName("report_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");

            entity.HasOne(d => d.Module).WithMany(p => p.GeneralExpenses)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("general_expenses_module_id_fkey");

            entity.HasOne(d => d.Report).WithMany(p => p.GeneralExpenses)
                .HasForeignKey(d => d.ReportId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("general_expenses_report_id_fkey");
        });

        modelBuilder.Entity<GeneralIncome>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("general_incomes_pkey");

            entity.ToTable("general_incomes");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IncomeType)
                .HasMaxLength(100)
                .HasColumnName("income_type");
            entity.Property(e => e.ModuleId).HasColumnName("module_id");
            entity.Property(e => e.ReportId).HasColumnName("report_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");

            entity.HasOne(d => d.Module).WithMany(p => p.GeneralIncomes)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("general_incomes_module_id_fkey");

            entity.HasOne(d => d.Report).WithMany(p => p.GeneralIncomes)
                .HasForeignKey(d => d.ReportId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("general_incomes_report_id_fkey");
        });

        modelBuilder.Entity<IncomeChurch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("INCOME_CHURCH_pkey");

            entity.ToTable("INCOME_CHURCH");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.Date)
                .HasMaxLength(255)
                .HasColumnName("date");
            entity.Property(e => e.IdChurch).HasColumnName("id_church");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            entity.HasOne(d => d.IdChurchNavigation).WithMany(p => p.IncomeChurches)
                .HasForeignKey(d => d.IdChurch)
                .HasConstraintName("INCOME_CHURCH_id_church_fkey");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("locations_pkey");

            entity.ToTable("locations");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Lost>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("lost_pkey");

            entity.ToTable("lost");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedAt1).HasColumnName("createdAt");
            entity.Property(e => e.LostType)
                .HasMaxLength(255)
                .HasColumnName("lost_type");
            entity.Property(e => e.Observations)
                .HasMaxLength(255)
                .HasColumnName("observations");
            entity.Property(e => e.ProductionId).HasColumnName("production_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");

            entity.HasOne(d => d.Production).WithMany(p => p.Losts)
                .HasForeignKey(d => d.ProductionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lost_production_id_fkey");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("modules_pkey");

            entity.ToTable("modules");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
        });

        modelBuilder.Entity<MonasteryExpense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("monastery_expenses_pkey");

            entity.ToTable("monastery_expenses");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Category)
                .HasMaxLength(255)
                .HasColumnName("category");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripción");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.OverheadsId).HasColumnName("overheadsId");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");

            entity.HasOne(d => d.Overheads).WithMany(p => p.MonasteryExpenses)
                .HasForeignKey(d => d.OverheadsId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("monastery_expenses_overheadsId_fkey");
        });

        modelBuilder.Entity<Overhead>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("overheads_pkey");

            entity.ToTable("overheads");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("payment_methods_pkey");

            entity.ToTable("payment_methods");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("permissions_pkey");

            entity.ToTable("permissions");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CanDelete)
                .HasDefaultValue(false)
                .HasColumnName("canDelete");
            entity.Property(e => e.CanEdit)
                .HasDefaultValue(false)
                .HasColumnName("canEdit");
            entity.Property(e => e.CanRead)
                .HasDefaultValue(false)
                .HasColumnName("canRead");
            entity.Property(e => e.CanWrite)
                .HasDefaultValue(false)
                .HasColumnName("canWrite");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.ModuleId).HasColumnName("moduleId");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");

            entity.HasOne(d => d.Module).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("permissions_moduleId_fkey");
        });

        modelBuilder.Entity<Place>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("places_pkey");

            entity.ToTable("places");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Area)
                .HasMaxLength(100)
                .HasColumnName("area");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.ImagenUrl)
                .HasColumnName("imagen_url");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");

            entity.HasOne(d => d.Location).WithMany(p => p.Places)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("places_location_id_fkey");
        });

        modelBuilder.Entity<PlantProduction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("plant_production_pkey");

            entity.ToTable("plant_production");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.PlantName)
                .HasMaxLength(255)
                .HasColumnName("plant_name");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
            entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("products_pkey");

            entity.ToTable("products");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.ImagenUrl)
                .HasColumnName("imagen_url");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Producible)
                .HasDefaultValue(true)
                .HasColumnName("producible");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_category_id_fkey");
        });

        modelBuilder.Entity<ProductPurchased>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_purchased_pkey");

            entity.ToTable("product_purchased");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.BuysproductId).HasColumnName("buysproduct_id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.ProductPurchasedId).HasColumnName("productPurchased_id");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");

            entity.HasOne(d => d.Buysproduct).WithMany(p => p.ProductPurchasedBuysproducts)
                .HasForeignKey(d => d.BuysproductId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("product_purchased_buysproduct_id_fkey");

            entity.HasOne(d => d.ProductPurchasedNavigation).WithMany(p => p.ProductPurchasedProductPurchasedNavigations)
                .HasForeignKey(d => d.ProductPurchasedId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("product_purchased_productPurchased_id_fkey");
        });

        modelBuilder.Entity<Production>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("production_pkey");

            entity.ToTable("production");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.Observation)
                .HasMaxLength(255)
                .HasColumnName("observation");
            entity.Property(e => e.PlantId).HasColumnName("plant_id");
            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.ProductionDate).HasColumnName("productionDate");
            entity.Property(e => e.QuantityProduced).HasColumnName("quantityProduced");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");

            entity.HasOne(d => d.Plant).WithMany(p => p.Productions)
                .HasForeignKey(d => d.PlantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("production_plant_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.Productions)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("production_productId_fkey");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("recipes_pkey");

            entity.ToTable("recipes");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ResourceId).HasColumnName("resourceId");
            entity.Property(e => e.Unit)
                .HasColumnName("unit")
                .HasColumnType("enum_recipes_unit");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");

            entity.HasOne(d => d.Product).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipes_productId_fkey");

            entity.HasOne(d => d.Resource).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipes_resourceId_fkey");
        });

        modelBuilder.Entity<RentChurch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("rent_church_pkey");

            entity.ToTable("rent_church");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.EndTime)
                .HasMaxLength(255)
                .HasColumnName("endTime");
            entity.Property(e => e.IdChurch).HasColumnName("idChurch");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasDefaultValueSql("'0'::double precision")
                .HasColumnName("price");
            entity.Property(e => e.StartTime)
                .HasMaxLength(255)
                .HasColumnName("startTime");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");

            entity.HasOne(d => d.IdChurchNavigation).WithMany(p => p.RentChurches)
                .HasForeignKey(d => d.IdChurch)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("rent_church_idChurch_fkey");
        });

        modelBuilder.Entity<Rental>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("rentals_pkey");

            entity.ToTable("rentals");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.PlaceId).HasColumnName("place_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rentals_customer_id_fkey");

            entity.HasOne(d => d.Place).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.PlaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rentals_place_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rentals_user_id_fkey");
        });

        modelBuilder.Entity<Resource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("resources_pkey");

            entity.ToTable("resources");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Observation)
                .HasMaxLength(255)
                .HasColumnName("observation");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Resources)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("resources_supplier_id_fkey");
        });

        modelBuilder.Entity<Return>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("returns_pkey");

            entity.ToTable("returns");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.Observations).HasColumnName("observations");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Reason)
                .HasMaxLength(255)
                .HasColumnName("reason");
            entity.Property(e => e.SalesId).HasColumnName("salesId");
            entity.Property(e => e.StoreId).HasColumnName("storeId");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");

            entity.HasOne(d => d.Product).WithMany(p => p.Returns)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("returns_productId_fkey");

            entity.HasOne(d => d.Sales).WithMany(p => p.Returns)
                .HasForeignKey(d => d.SalesId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("returns_salesId_fkey");

            entity.HasOne(d => d.Store).WithMany(p => p.Returns)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("returns_storeId_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
        });

        modelBuilder.Entity<RolesPermission>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.PermissionId }).HasName("roles_permissions_pkey");

            entity.ToTable("roles_permissions");

            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.PermissionId).HasColumnName("permissionId");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");

            entity.HasOne(d => d.Permission).WithMany(p => p.RolesPermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("roles_permissions_permissionId_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.RolesPermissions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("roles_permissions_roleId_fkey");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sales_pkey");

            entity.ToTable("sales");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.IncomeDate)
                .HasMaxLength(255)
                .HasColumnName("income_date");
            entity.Property(e => e.Observations)
                .HasMaxLength(255)
                .HasColumnName("observations");
            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.TotalIncome).HasColumnName("total_income");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");

            entity.HasOne(d => d.Store).WithMany(p => p.Sales)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sales_store_id_fkey");
        });

        modelBuilder.Entity<SaleDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("saleDetails_pkey");

            entity.ToTable("saleDetails");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.Mount).HasColumnName("mount");
            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SaleId).HasColumnName("saleId");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");

            entity.HasOne(d => d.Product).WithMany(p => p.SaleDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("saleDetails_productId_fkey");

            entity.HasOne(d => d.Sale).WithMany(p => p.SaleDetails)
                .HasForeignKey(d => d.SaleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("saleDetails_saleId_fkey");
        });

        modelBuilder.Entity<SalesChannel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sales_channels_pkey");

            entity.ToTable("sales_channels");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("stores_pkey");

            entity.ToTable("stores");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.Observations)
                .HasMaxLength(255)
                .HasColumnName("observations");
            entity.Property(e => e.StoreName)
                .HasMaxLength(255)
                .HasColumnName("store_name");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("suppliers_pkey");

            entity.ToTable("suppliers");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.ContactName)
                .HasMaxLength(255)
                .HasColumnName("contact_name");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Ruc).HasColumnName("ruc");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.SuplierName)
                .HasMaxLength(255)
                .HasColumnName("suplier_name");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
        });

        modelBuilder.Entity<TypePerson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("type_persons_pkey");

            entity.ToTable("type_persons");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.BasePrice).HasColumnName("base_price");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Dni, "users_dni_key").IsUnique();

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.Dni)
                .HasMaxLength(255)
                .HasColumnName("dni");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(255)
                .HasColumnName("phonenumber");
            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("users_roleId_fkey");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("warehouses_pkey");

            entity.ToTable("warehouses");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Observation)
                .HasMaxLength(255)
                .HasColumnName("observation");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
        });

        modelBuilder.Entity<WarehouseMovementProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("warehouse_movement_products_pkey");

            entity.ToTable("warehouse_movement_products");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.MovementDate).HasColumnName("movement_date");
            entity.Property(e => e.MovementType)
                .HasMaxLength(255)
                .HasColumnName("movement_type");
            entity.Property(e => e.Observations)
                .HasMaxLength(255)
                .HasColumnName("observations");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.StoreId)
                .HasMaxLength(255)
                .HasColumnName("store_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
            entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");

            entity.HasOne(d => d.Product).WithMany(p => p.WarehouseMovementProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("warehouse_movement_products_product_id_fkey");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.WarehouseMovementProducts)
                .HasForeignKey(d => d.WarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("warehouse_movement_products_warehouse_id_fkey");
        });

        modelBuilder.Entity<WarehouseMovementResource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("warehouse_movement_resources_pkey");

            entity.ToTable("warehouse_movement_resources");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.MovementDate).HasColumnName("movement_date");
            entity.Property(e => e.MovementType)
                .HasMaxLength(255)
                .HasColumnName("movement_type");
            entity.Property(e => e.Observations)
                .HasMaxLength(255)
                .HasColumnName("observations");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ResourceId).HasColumnName("resource_id");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
            entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");

            entity.HasOne(d => d.Resource).WithMany(p => p.WarehouseMovementResources)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("warehouse_movement_resources_resource_id_fkey");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.WarehouseMovementResources)
                .HasForeignKey(d => d.WarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("warehouse_movement_resources_warehouse_id_fkey");
        });

        modelBuilder.Entity<WarehouseProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("warehouse_products_pkey");

            entity.ToTable("warehouse_products");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.EntryDate).HasColumnName("entry_date");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
            entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");

            entity.HasOne(d => d.Product).WithMany(p => p.WarehouseProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("warehouse_products_product_id_fkey");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.WarehouseProducts)
                .HasForeignKey(d => d.WarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("warehouse_products_warehouse_id_fkey");
        });

        modelBuilder.Entity<WarehouseResource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("warehouse_resources_pkey");

            entity.ToTable("warehouse_resources");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.EntryDate).HasColumnName("entry_date");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ResourceId).HasColumnName("resource_id");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.TotalCost).HasColumnName("total_cost");
            entity.Property(e => e.TypeUnit)
                .HasMaxLength(50)
                .HasColumnName("type_unit");
            entity.Property(e => e.UnitPrice).HasColumnName("unit_price");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
            entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");

            entity.HasOne(d => d.Resource).WithMany(p => p.WarehouseResources)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("warehouse_resources_resource_id_fkey");

            entity.HasOne(d => d.Supplier).WithMany(p => p.WarehouseResources)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("warehouse_resources_supplier_id_fkey");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.WarehouseResources)
                .HasForeignKey(d => d.WarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("warehouse_resources_warehouse_id_fkey");
        });

        modelBuilder.Entity<WarehouseStore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("warehouse_stores_pkey");

            entity.ToTable("warehouse_stores");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.StoreId).HasColumnName("storeId");
            entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");

            entity.HasOne(d => d.Product).WithMany(p => p.WarehouseStores)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("warehouse_stores_productId_fkey");

            entity.HasOne(d => d.Store).WithMany(p => p.WarehouseStores)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("warehouse_stores_storeId_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
