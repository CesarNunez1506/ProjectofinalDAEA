using Domain.Interfaces.Repositories.Production;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Repositories.Users;
using Domain.Interfaces.Services.Production;
using Domain.Interfaces.Services;
using Domain.Interfaces.Services.Users;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Users;
using Infrastructure.Services.Production;
using Infrastructure.Services;
using Infrastructure.Services.Users;
using Application.UseCases.Production.Categories;
using Application.UseCases.Production.Products;
using Application.UseCases.Production.Recipes;
using Application.UseCases.Production.Productions;
using Application.UseCases.Production.Losts;
using Application.UseCases.Production.PlantProductions;
using Application.UseCases.Rentals;
using Application.UseCases.Rentals.Customers;
using Application.UseCases.Rentals.Places;
using Application.UseCases.Rentals.Locations;
using Application.UseCases.Finance.MonasteryExpenses;
using Application.UseCases.Finance.Overheads;
using Application.UseCases.Modules.Queries;
using Application.UseCases.Modules.Commands;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Proyecto_Final.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext (ya existente en el proyecto)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar LocalDbContext para módulo de producción
builder.Services.AddDbContext<LocalDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar MediatR para CQRS
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Application.DTOs.Users.UserDto>());

// Agregar AutoMapper
builder.Services.AddAutoMapper(typeof(Application.Mappings.UserMappingProfile).Assembly);
builder.Services.AddAutoMapper(typeof(Application.Mappings.InventoryMappingProfile).Assembly);

// Agregar FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Application.Validators.Users.CreateUserDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<Application.Validators.Inventory.CreateProductValidator>();

// Agregar controladores
builder.Services.AddControllers();

// Configurar JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey not configured");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"] ?? "DAEFinal",
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"] ?? "DAEFinalUsers",
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

// Configurar Swagger/OpenAPI con soporte JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ST-ERP API",
        Version = "v1",
        Description = "API del Sistema ERP Santa Teresa - Módulos de Producción, Usuarios e Inventario"
    });

    // Resolver conflictos de nombres de DTOs usando namespace completo
    options.CustomSchemaIds(type => type.FullName?.Replace("+", "."));

    // Configurar JWT en Swagger
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Ingrese 'Bearer' [espacio] y luego su token JWT"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Configurar CORS (si es necesario para el frontend)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // URL del frontend Next.js
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// ========== REPOSITORIOS DEL MÓDULO DE USUARIOS ==========
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IModuleRepository, Infrastructure.Repositories.Modules.ModuleRepository>();

// ========== SERVICIOS DEL MÓDULO DE USUARIOS ==========
builder.Services.AddScoped<IPasswordHashService, PasswordHashService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

// ========== REPOSITORIOS DEL MÓDULO DE PRODUCCIÓN ==========
// Los repositorios específicos (Category, Product, Recipe, Production, Lost, PlantProduction) han sido reemplazados por el repositorio genérico
// Solo se mantienen repositorios con lógica especial que no puede ser manejada genéricamente
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>(); // Mantener - tiene lógica compleja FIFO

// ========== REPOSITORIOS DEL MÓDULO DE ALQUILERES ==========
// Todos los repositorios específicos de Rentals han sido eliminados y reemplazados por el repositorio genérico
// La lógica de CheckOverlapAsync se implementa directamente en los UseCases usando GenericRepository

// ========== REPOSITORIOS DEL MÓDULO DE FINANZAS ==========
// Todos los repositorios específicos de Finance han sido eliminados y reemplazados por el repositorio genérico
// FinancialReportRepository, GeneralIncomeRepository, ModuleRepository -> Ahora usan GenericRepository via UnitOfWork

// ========== SERVICIOS DEL MÓDULO DE PRODUCCIÓN ==========
builder.Services.AddScoped<IUnitConversionService, UnitConversionService>();
builder.Services.AddScoped<IFileStorageService, FileStorageService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// ========== REPOSITORIOS DEL MÓDULO DE INVENTARIO ==========
builder.Services.AddScoped<Domain.Interfaces.Repositories.Inventory.IProductRepository, Infrastructure.Repositories.Inventory.ProductRepository>();
builder.Services.AddScoped<Domain.Interfaces.Repositories.Inventory.ICategoryRepository, Infrastructure.Repositories.Inventory.CategoryRepository>();
builder.Services.AddScoped<Domain.Interfaces.Repositories.Inventory.IWarehouseRepository, Infrastructure.Repositories.Inventory.WarehouseRepository>();
builder.Services.AddScoped<Domain.Interfaces.Repositories.Inventory.ISupplierRepository, Infrastructure.Repositories.Inventory.SupplierRepository>();
builder.Services.AddScoped<Domain.Interfaces.Repositories.Inventory.IResourceRepository, Infrastructure.Repositories.Inventory.ResourceRepository>();
builder.Services.AddScoped<Domain.Interfaces.Repositories.Inventory.IWarehouseProductRepository, Infrastructure.Repositories.Inventory.WarehouseProductRepository>();
builder.Services.AddScoped<Domain.Interfaces.Repositories.Inventory.IWarehouseResourceRepository, Infrastructure.Repositories.Inventory.WarehouseResourceRepository>();
builder.Services.AddScoped<Domain.Interfaces.Repositories.Inventory.IBuysProductRepository, Infrastructure.Repositories.Inventory.BuysProductRepository>();
builder.Services.AddScoped<Domain.Interfaces.Repositories.Inventory.IWarehouseMovementProductRepository, Infrastructure.Repositories.Inventory.WarehouseMovementProductRepository>();
builder.Services.AddScoped<Domain.Interfaces.Repositories.Inventory.IWarehouseMovementResourceRepository, Infrastructure.Repositories.Inventory.WarehouseMovementResourceRepository>();

// Nota: El UnitOfWork ya está registrado arriba y contiene todos los repositorios de Inventario con lazy initialization

// ========== CASOS DE USO - CATEGORÍAS ==========
builder.Services.AddScoped<CreateCategoryUseCase>();
builder.Services.AddScoped<GetAllCategoriesUseCase>();
builder.Services.AddScoped<GetCategoryByIdUseCase>();
builder.Services.AddScoped<UpdateCategoryUseCase>();
builder.Services.AddScoped<DeleteCategoryUseCase>();

// ========== CASOS DE USO - PRODUCTOS ==========
builder.Services.AddScoped<CreateProductUseCase>();
builder.Services.AddScoped<GetAllProductsUseCase>();
builder.Services.AddScoped<GetProductByIdUseCase>();
builder.Services.AddScoped<UpdateProductUseCase>();
builder.Services.AddScoped<DeleteProductUseCase>();

// ========== CASOS DE USO - RECETAS ==========
builder.Services.AddScoped<CreateRecipeUseCase>();
builder.Services.AddScoped<GetAllRecipesUseCase>();
builder.Services.AddScoped<GetRecipesByProductIdUseCase>();
builder.Services.AddScoped<UpdateRecipeUseCase>();
builder.Services.AddScoped<DeleteRecipeUseCase>();

// ========== CASOS DE USO - PRODUCCIONES ==========
builder.Services.AddScoped<CreateProductionUseCase>();
builder.Services.AddScoped<GetAllProductionsUseCase>();
builder.Services.AddScoped<GetProductionByIdUseCase>();
builder.Services.AddScoped<UpdateProductionUseCase>();
builder.Services.AddScoped<ToggleProductionStatusUseCase>();

// ========== CASOS DE USO - PÉRDIDAS ==========
builder.Services.AddScoped<CreateLostUseCase>();
builder.Services.AddScoped<GetAllLostsUseCase>();
builder.Services.AddScoped<GetLostByIdUseCase>();
builder.Services.AddScoped<UpdateLostUseCase>();
builder.Services.AddScoped<DeleteLostUseCase>();

// ========== CASOS DE USO - PLANTAS DE PRODUCCIÓN ==========
builder.Services.AddScoped<CreatePlantProductionUseCase>();
builder.Services.AddScoped<GetAllPlantProductionsUseCase>();
builder.Services.AddScoped<GetPlantProductionByIdUseCase>();
builder.Services.AddScoped<UpdatePlantProductionUseCase>();
builder.Services.AddScoped<DeletePlantProductionUseCase>();

// ========== CASOS DE USO - ALQUILERES ==========
builder.Services.AddScoped<CreateRentalUseCase>();
builder.Services.AddScoped<GetAllRentalsUseCase>();
builder.Services.AddScoped<GetRentalByIdUseCase>();
builder.Services.AddScoped<UpdateRentalUseCase>();
builder.Services.AddScoped<ToggleRentalStatusUseCase>();

// ========== CASOS DE USO - CLIENTES ==========
builder.Services.AddScoped<Application.UseCases.Rentals.Customers.CreateCustomerUseCase>();
builder.Services.AddScoped<Application.UseCases.Rentals.Customers.GetAllCustomersUseCase>();
builder.Services.AddScoped<Application.UseCases.Rentals.Customers.GetCustomerByIdUseCase>();
builder.Services.AddScoped<Application.UseCases.Rentals.Customers.UpdateCustomerUseCase>();
builder.Services.AddScoped<Application.UseCases.Rentals.Customers.DeleteCustomerUseCase>();

// ========== CASOS DE USO - LUGARES ==========
builder.Services.AddScoped<Application.UseCases.Rentals.Places.CreatePlaceUseCase>();
builder.Services.AddScoped<Application.UseCases.Rentals.Places.GetAllPlacesUseCase>();
builder.Services.AddScoped<Application.UseCases.Rentals.Places.GetPlaceByIdUseCase>();
builder.Services.AddScoped<Application.UseCases.Rentals.Places.UpdatePlaceUseCase>();
builder.Services.AddScoped<Application.UseCases.Rentals.Places.DeletePlaceUseCase>();

// ========== CASOS DE USO - UBICACIONES ==========
builder.Services.AddScoped<Application.UseCases.Rentals.Locations.CreateLocationUseCase>();
builder.Services.AddScoped<Application.UseCases.Rentals.Locations.GetAllLocationsUseCase>();
builder.Services.AddScoped<Application.UseCases.Rentals.Locations.GetLocationByIdUseCase>();
builder.Services.AddScoped<Application.UseCases.Rentals.Locations.UpdateLocationUseCase>();
builder.Services.AddScoped<Application.UseCases.Rentals.Locations.DeleteLocationUseCase>();

// ========== CASOS DE USO - GASTOS DEL MONASTERIO ==========
builder.Services.AddScoped<CreateMonasteryExpenseUseCase>();
builder.Services.AddScoped<GetAllMonasteryExpensesUseCase>();
builder.Services.AddScoped<GetMonasteryExpenseByIdUseCase>();
builder.Services.AddScoped<UpdateMonasteryExpenseUseCase>();
builder.Services.AddScoped<DeleteMonasteryExpenseUseCase>();

// ========== CASOS DE USO - OVERHEADS (MONASTERIO) ==========
builder.Services.AddScoped<CreateOverheadUseCase>();
builder.Services.AddScoped<GetAllOverheadsUseCase>();
builder.Services.AddScoped<GetOverheadByIdUseCase>();
builder.Services.AddScoped<UpdateOverheadUseCase>();
builder.Services.AddScoped<DeleteOverheadUseCase>();
builder.Services.AddScoped<GetExpensesByDateRangeUseCase>();

// ========== CASOS DE USO - MÓDULOS ==========
builder.Services.AddScoped<Application.UseCases.Modules.Queries.GetAllModulesQuery>();
builder.Services.AddScoped<Application.UseCases.Modules.Queries.GetModuleByIdQuery>();
builder.Services.AddScoped<Application.UseCases.Modules.Queries.SearchModulesByNameQuery>();
builder.Services.AddScoped<Application.UseCases.Modules.Commands.CreateModuleCommand>();
builder.Services.AddScoped<Application.UseCases.Modules.Commands.UpdateModuleCommand>();
builder.Services.AddScoped<Application.UseCases.Modules.Commands.DeleteModuleCommand>();

// ========== MÓDULO DE VENTAS - TIENDAS ==========
builder.Services.AddScoped<Application.UseCases.Sales.Stores.Commands.CreateStoreCommand>();
builder.Services.AddScoped<Application.UseCases.Sales.Stores.Commands.UpdateStoreCommand>();
builder.Services.AddScoped<Application.UseCases.Sales.Stores.Commands.DeleteStoreCommand>();
builder.Services.AddScoped<Application.UseCases.Sales.Stores.Queries.GetAllStoresQuery>();
builder.Services.AddScoped<Application.UseCases.Sales.Stores.Queries.GetStoreByIdQuery>();

// ========== MÓDULO DE VENTAS - INVENTARIO POR TIENDA ==========
builder.Services.AddScoped<Application.UseCases.Sales.WarehouseStores.Commands.CreateWarehouseStoreCommand>();
builder.Services.AddScoped<Application.UseCases.Sales.WarehouseStores.Commands.UpdateWarehouseStoreCommand>();
builder.Services.AddScoped<Application.UseCases.Sales.WarehouseStores.Commands.DeleteWarehouseStoreCommand>();
builder.Services.AddScoped<Application.UseCases.Sales.WarehouseStores.Queries.GetAllWarehouseStoresQuery>();
builder.Services.AddScoped<Application.UseCases.Sales.WarehouseStores.Queries.GetWarehouseStoreByIdQuery>();

// ========== MÓDULO DE VENTAS - SESIONES DE CAJA ==========
builder.Services.AddScoped<Application.UseCases.Sales.CashSessions.Commands.CreateCashSessionCommand>();
builder.Services.AddScoped<Application.UseCases.Sales.CashSessions.Commands.UpdateCashSessionCommand>();
builder.Services.AddScoped<Application.UseCases.Sales.CashSessions.Commands.DeleteCashSessionCommand>();
builder.Services.AddScoped<Application.UseCases.Sales.CashSessions.Queries.GetAllCashSessionsQuery>();
builder.Services.AddScoped<Application.UseCases.Sales.CashSessions.Queries.GetCashSessionByIdQuery>();

// ========== MÓDULO DE VENTAS - VENTAS ==========
builder.Services.AddScoped<Application.UseCases.Sales.Sales.Commands.CreateSaleCommand>();
builder.Services.AddScoped<Application.UseCases.Sales.Sales.Commands.UpdateSaleCommand>();
builder.Services.AddScoped<Application.UseCases.Sales.Sales.Commands.DeleteSaleCommand>();
builder.Services.AddScoped<Application.UseCases.Sales.Sales.Queries.GetAllSalesQuery>();
builder.Services.AddScoped<Application.UseCases.Sales.Sales.Queries.GetSaleByIdQuery>();

// ========== MÓDULO DE VENTAS - DETALLES DE VENTA ==========
builder.Services.AddScoped<Application.UseCases.Sales.SaleDetails.Commands.CreateSaleDetailCommand>();
builder.Services.AddScoped<Application.UseCases.Sales.SaleDetails.Commands.UpdateSaleDetailCommand>();
builder.Services.AddScoped<Application.UseCases.Sales.SaleDetails.Commands.DeleteSaleDetailCommand>();
builder.Services.AddScoped<Application.UseCases.Sales.SaleDetails.Queries.GetAllSaleDetailsQuery>();
builder.Services.AddScoped<Application.UseCases.Sales.SaleDetails.Queries.GetSaleDetailByIdQuery>();

// ========== MÓDULO DE VENTAS - DEVOLUCIONES ==========
builder.Services.AddScoped<Application.UseCases.Sales.Returns.Commands.CreateReturnCommand>();
builder.Services.AddScoped<Application.UseCases.Sales.Returns.Commands.UpdateReturnCommand>();
builder.Services.AddScoped<Application.UseCases.Sales.Returns.Commands.DeleteReturnCommand>();
builder.Services.AddScoped<Application.UseCases.Sales.Returns.Queries.GetAllReturnsQuery>();
builder.Services.AddScoped<Application.UseCases.Sales.Returns.Queries.GetReturnByIdQuery>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "ST-ERP API v1");
        options.RoutePrefix = "swagger"; // Acceder desde: http://localhost:5000/swagger
    });
}

// Middleware global de manejo de excepciones (debe ir primero)
app.UseGlobalExceptionHandler();

app.UseCors("AllowFrontend");
app.UseStaticFiles(); // Para servir archivos desde wwwroot/ (imágenes de productos)

app.UseHttpsRedirection();
app.UseAuthentication(); // IMPORTANTE: Authentication antes de Authorization
app.UseAuthorization();
app.MapControllers();

app.Run();