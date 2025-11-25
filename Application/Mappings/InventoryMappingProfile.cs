using Application.DTOs.Inventory;
using AutoMapper;
using Infrastructure.Data.Entities;

namespace Application.Mappings;

/// <summary>
/// Perfil de AutoMapper para el módulo de Inventario
/// </summary>
public class InventoryMappingProfile : Profile
{
    public InventoryMappingProfile()
    {
        // ============================================
        // MAPEOS DE PRODUCTOS
        // ============================================

        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

        CreateMap<CreateProductDto, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<UpdateProductDto, Product>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        // ============================================
        // MAPEOS DE CATEGORÍAS
        // ============================================

        CreateMap<Category, CategoryDto>()
            .ForMember(dest => dest.ProductCount, opt => opt.MapFrom(src => src.Products.Count));

        CreateMap<CreateCategoryDto, Category>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<UpdateCategoryDto, Category>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        // ============================================
        // MAPEOS DE ALMACENES
        // ============================================

        CreateMap<Warehouse, WarehouseDto>()
            .ForMember(dest => dest.CurrentLoad, opt => opt.MapFrom(src =>
                src.WarehouseProducts.Sum(wp => wp.Quantity)));

        CreateMap<CreateWarehouseDto, Warehouse>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<UpdateWarehouseDto, Warehouse>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        // ============================================
        // MAPEOS DE PROVEEDORES
        // ============================================

        CreateMap<Supplier, SupplierDto>();

        CreateMap<CreateSupplierDto, Supplier>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<UpdateSupplierDto, Supplier>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        // ============================================
        // MAPEOS DE RECURSOS
        // ============================================

        CreateMap<Resource, ResourceDto>()
            .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src =>
                src.Supplier != null ? src.Supplier.SuplierName : null));

        CreateMap<CreateResourceDto, Resource>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<UpdateResourceDto, Resource>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        // ============================================
        // MAPEOS DE STOCK EN ALMACENES
        // ============================================

        CreateMap<WarehouseProduct, WarehouseProductDto>()
            .ForMember(dest => dest.WarehouseName, opt => opt.MapFrom(src => src.Warehouse.Name))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

        CreateMap<AddWarehouseProductDto, WarehouseProduct>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.EntryDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        // ============================================
        // MAPEOS DE MOVIMIENTOS
        // ============================================

        CreateMap<WarehouseMovementProduct, WarehouseMovementProductDto>()
            .ForMember(dest => dest.WarehouseName, opt => opt.MapFrom(src => src.Warehouse.Name))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

        CreateMap<CreateWarehouseMovementDto, WarehouseMovementProduct>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.MovementDate, opt => opt.MapFrom(src => DateTime.UtcNow));

        // ============================================
        // MAPEOS DE COMPRAS
        // ============================================

        CreateMap<BuysProduct, BuysProductDto>()
            .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.SuplierName))
            .ForMember(dest => dest.WarehouseName, opt => opt.MapFrom(src => src.Warehouse.Name));

        CreateMap<CreateBuysProductDto, BuysProduct>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
    }
}
