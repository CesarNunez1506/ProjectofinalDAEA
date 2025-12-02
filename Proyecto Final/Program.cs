using Domain.Interfaces.Repositories.Production;
using Domain.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Repositories;
using Domain.Interfaces.Repositories.Users;
using Domain.Interfaces.Services.Production;
using Domain.Interfaces.Services;
using Domain.Interfaces.Services.Users;
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
using Infrastructure.Data;
using Application.UseCases.Finance.Incomes.Commands;
using Application.UseCases.Finance.Incomes.Queries;
using Application.UseCases.Finance.Expenses.Commands;
using Application.UseCases.Finance.Expenses.Queries;
using Application.UseCases.Finance.MonasteryExpenses.Commands;
using Application.UseCases.Finance.MonasteryExpenses.Queries;
using Application.UseCases.Finance.Overheads.Commands;
using Application.UseCases.Finance.Overheads.Queries;
using Application.UseCases.Finance.FinancialReports.Commands;
using Application.UseCases.Finance.FinancialReports.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Proyecto_Final.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Cargar variables de entorno desde .env (solo en desarrollo)
if (builder.Environment.IsDevelopment())
{
    var envPath = Path.Combine(Directory.GetCurrentDirectory(), "..", ".env");
    if (File.Exists(envPath))
    {
        foreach (var line in File.ReadAllLines(envPath))
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) continue;
            
            var parts = line.Split('=', 2, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2)
            {
                Environment.SetEnvironmentVariable(parts[0].Trim(), parts[1].Trim());
            }
        }
    }
}

// Construir connection string desde variables de entorno o appsettings
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

// Log para debugging en Render
Console.WriteLine($"[DEBUG] DATABASE_URL exists: {!string.IsNullOrEmpty(databaseUrl)}");
if (!string.IsNullOrEmpty(databaseUrl))
{
    Console.WriteLine($"[DEBUG] DATABASE_URL length: {databaseUrl.Length} chars");
    Console.WriteLine($"[DEBUG] DATABASE_URL starts with: {databaseUrl.Substring(0, Math.Min(20, databaseUrl.Length))}...");
}

string connectionString = null;

if (!string.IsNullOrEmpty(databaseUrl))
{
    // Convertir de formato postgresql:// a formato Npgsql (Host=...;Port=...)
    try
    {
        var uri = new Uri(databaseUrl);
        var port = uri.Port > 0 ? uri.Port : 5432; // Default PostgreSQL port
        var userInfo = uri.UserInfo.Split(':');
        var username = userInfo.Length > 0 ? userInfo[0] : "";
        var password = userInfo.Length > 1 ? userInfo[1] : "";
        
        connectionString = $"Host={uri.Host};Port={port};Database={uri.LocalPath.TrimStart('/')};Username={username};Password={password};SSL Mode=Require;Trust Server Certificate=true";
        Console.WriteLine("[DEBUG] Converted DATABASE_URL to Npgsql format");
        Console.WriteLine($"[DEBUG] Connection details - Host: {uri.Host}, Port: {port}, Database: {uri.LocalPath.TrimStart('/')}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[ERROR] Failed to parse DATABASE_URL: {ex.Message}");
    }
}

if (string.IsNullOrEmpty(connectionString))
{
    var host = Environment.GetEnvironmentVariable("DB_HOST");
    var port = Environment.GetEnvironmentVariable("DB_PORT");
    var database = Environment.GetEnvironmentVariable("DB_NAME");
    var username = Environment.GetEnvironmentVariable("DB_USER");
    var password = Environment.GetEnvironmentVariable("DB_PASSWORD");
    
    Console.WriteLine($"[DEBUG] Individual vars - Host: {host}, DB: {database}, User: {username}");
    
    if (!string.IsNullOrEmpty(host) && !string.IsNullOrEmpty(database))
    {
        connectionString = $"Host={host};Port={port ?? "5432"};Database={database};Username={username};Password={password}";
        Console.WriteLine("[DEBUG] Built connection string from individual vars");
    }
    else
    {
        // Fallback a appsettings.json para desarrollo local sin .env
        connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        Console.WriteLine("[DEBUG] Using DefaultConnection from appsettings.json");
    }
}

if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("[ERROR] No connection string available!");
    throw new InvalidOperationException("Database connection string is not configured. Please set DATABASE_URL environment variable.");
}

Console.WriteLine("[DEBUG] Final connection string configured successfully");

// Configurar DbContext (ya existente en el proyecto)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Nota: ya no usamos LocalDbContext — solo AppDbContext está en uso

// Agregar MediatR para CQRS
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Application.DTOs.Finance.ExpenseDto).Assembly));

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

// Configurar CORS para el frontend Next.js
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:3000",  // Frontend Next.js desarrollo
                "http://localhost:3001",  // Puerto alternativo
                "https://front-daea.vercel.app", // Frontend en Vercel
                "https://*.vercel.app"    // Cualquier subdominio de Vercel
              )
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// REPOSITORIOS DEL MÓDULO DE USUARIOS
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IModuleRepository, Infrastructure.Repositories.Modules.ModuleRepository>();

// SERVICIOS DEL MÓDULO DE USUARIOS
builder.Services.AddScoped<IPasswordHashService, PasswordHashService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

// REPOSITORIOS DEL MÓDULO DE PRODUCCIÓN
// Los repositorios específicos (Category, Product, Recipe, Production, Lost, PlantProduction) han sido reemplazados por el repositorio genérico
// Solo se mantienen repositorios con lógica especial que no puede ser manejada genéricamente
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>(); // Mantener - tiene lógica compleja FIFO

// SERVICIOS DEL MÓDULO DE PRODUCCIÓN
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

// CASOS DE USO - CATEGORÍAS
builder.Services.AddScoped<CreateCategoryUseCase>();
builder.Services.AddScoped<GetAllCategoriesUseCase>();
builder.Services.AddScoped<GetCategoryByIdUseCase>();
builder.Services.AddScoped<UpdateCategoryUseCase>();
builder.Services.AddScoped<DeleteCategoryUseCase>();

// CASOS DE USO - PRODUCTOS
builder.Services.AddScoped<CreateProductUseCase>();
builder.Services.AddScoped<GetAllProductsUseCase>();
builder.Services.AddScoped<GetProductByIdUseCase>();
builder.Services.AddScoped<UpdateProductUseCase>();
builder.Services.AddScoped<DeleteProductUseCase>();

// CASOS DE USO - RECETAS
builder.Services.AddScoped<CreateRecipeUseCase>();
builder.Services.AddScoped<GetAllRecipesUseCase>();
builder.Services.AddScoped<GetRecipesByProductIdUseCase>();
builder.Services.AddScoped<UpdateRecipeUseCase>();
builder.Services.AddScoped<DeleteRecipeUseCase>();

// CASOS DE USO - PRODUCCIONES
builder.Services.AddScoped<CreateProductionUseCase>();
builder.Services.AddScoped<GetAllProductionsUseCase>();
builder.Services.AddScoped<GetProductionByIdUseCase>();
builder.Services.AddScoped<UpdateProductionUseCase>();
builder.Services.AddScoped<ToggleProductionStatusUseCase>();

// CASOS DE USO - PÉRDIDAS
builder.Services.AddScoped<CreateLostUseCase>();
builder.Services.AddScoped<GetAllLostsUseCase>();
builder.Services.AddScoped<GetLostByIdUseCase>();
builder.Services.AddScoped<UpdateLostUseCase>();
builder.Services.AddScoped<DeleteLostUseCase>();

// CASOS DE USO - PLANTAS DE PRODUCCIÓN
builder.Services.AddScoped<CreatePlantProductionUseCase>();
builder.Services.AddScoped<GetAllPlantProductionsUseCase>();
builder.Services.AddScoped<GetPlantProductionByIdUseCase>();
builder.Services.AddScoped<UpdatePlantProductionUseCase>();
builder.Services.AddScoped<DeletePlantProductionUseCase>();



// UseCases - Incomes
builder.Services.AddScoped<Application.UseCases.Finance.Incomes.Commands.CreateIncomeUseCase>();
builder.Services.AddScoped<Application.UseCases.Finance.Incomes.Queries.GetIncomesByPeriodUseCase>();
builder.Services.AddScoped<Application.UseCases.Finance.Incomes.Queries.GetIncomesByPeriodQuery>();
builder.Services.AddScoped<Application.UseCases.Finance.FinancialReports.Commands.CreateIncomeUseCase>();

// UseCases - Expenses
builder.Services.AddScoped<Application.UseCases.Finance.Expenses.Commands.CreateExpenseUseCase>();
builder.Services.AddScoped<Application.UseCases.Finance.Expenses.Commands.CreateExpenseCommand>();
builder.Services.AddScoped<Application.UseCases.Finance.Expenses.Queries.GetExpensesByPeriodUseCase>();
builder.Services.AddScoped<Application.UseCases.Finance.Expenses.Queries.GetExpensesByPeriodQuery>();
builder.Services.AddScoped<Application.UseCases.Finance.FinancialReports.Commands.CreateExpenseUseCase>();

// UseCases - MonasteryExpenses
builder.Services.AddScoped<CreateMonasteryExpenseUseCase>();
builder.Services.AddScoped<GetAllMonasteryExpensesUseCase>();
builder.Services.AddScoped<GetMonasteryExpenseByIdUseCase>();

// UseCases - Overheads
builder.Services.AddScoped<Application.UseCases.Finance.Overheads.Commands.CreateOverheadUseCase>();
builder.Services.AddScoped<Application.UseCases.Finance.Overheads.Queries.GetAllOverheadsUseCase>();
builder.Services.AddScoped<Application.UseCases.Finance.Overheads.Queries.GetOverheadByIdUseCase>();
builder.Services.AddScoped<Application.UseCases.Finance.Overheads.Commands.UpdateOverheadUseCase>();
builder.Services.AddScoped<Application.UseCases.Finance.Commands.DeleteOverheadUseCase>();

// UseCases - FinancialReports
builder.Services.AddScoped<Application.UseCases.Finance.FinancialReports.Commands.GenerateFinancialReportUseCase>();
builder.Services.AddScoped<Application.UseCases.Finance.FinancialReports.Queries.GetFinancialReportByDateQuery>();
builder.Services.AddScoped<Application.UseCases.Finance.FinancialReports.Queries.GetProfitLossStatementQuery>();
builder.Services.AddScoped<Application.UseCases.Finance.FinancialReports.Commands.RecordOverheadUseCase>();

// ========== CASOS DE USO - MUSEO  ==========
// Entrances
builder.Services.AddScoped<Application.UseCases.Museum.Entrances.Commands.CreateEntranceUseCase>();
builder.Services.AddScoped<Application.UseCases.Museum.Entrances.Commands.UpdateEntranceUseCase>();
builder.Services.AddScoped<Application.UseCases.Museum.Entrances.Commands.DeleteEntranceUseCase>();
builder.Services.AddScoped<Application.UseCases.Museum.Entrances.Queries.GetAllEntrancesUseCase>();
builder.Services.AddScoped<Application.UseCases.Museum.Entrances.Queries.GetEntranceByIdUseCase>();

// TypePersons
builder.Services.AddScoped<Application.UseCases.Museum.TypePersons.Commands.CreateTypePersonUseCase>();
builder.Services.AddScoped<Application.UseCases.Museum.TypePersons.Commands.UpdateTypePersonUseCase>();
builder.Services.AddScoped<Application.UseCases.Museum.TypePersons.Commands.DeleteTypePersonUseCase>();
builder.Services.AddScoped<Application.UseCases.Museum.TypePersons.Queries.GetAllTypePersonsUseCase>();
builder.Services.AddScoped<Application.UseCases.Museum.TypePersons.Queries.GetTypePersonByIdUseCase>();

// SalesChannels
builder.Services.AddScoped<Application.UseCases.Museum.SalesChannels.Commands.CreateSalesChannelUseCase>();
builder.Services.AddScoped<Application.UseCases.Museum.SalesChannels.Commands.UpdateSalesChannelUseCase>();
builder.Services.AddScoped<Application.UseCases.Museum.SalesChannels.Commands.DeleteSalesChannelUseCase>();
builder.Services.AddScoped<Application.UseCases.Museum.SalesChannels.Queries.GetAllSalesChannelsUseCase>();
builder.Services.AddScoped<Application.UseCases.Museum.SalesChannels.Queries.GetSalesChannelByIdUseCase>();

// PaymentMethods
builder.Services.AddScoped<Application.UseCases.Museum.PaymentMethods.Commands.CreatePaymentMethodUseCase>();
builder.Services.AddScoped<Application.UseCases.Museum.PaymentMethods.Commands.UpdatePaymentMethodUseCase>();
builder.Services.AddScoped<Application.UseCases.Museum.PaymentMethods.Commands.DeletePaymentMethodUseCase>();
builder.Services.AddScoped<Application.UseCases.Museum.PaymentMethods.Queries.GetAllPaymentMethodsUseCase>();
builder.Services.AddScoped<Application.UseCases.Museum.PaymentMethods.Queries.GetPaymentMethodByIdUseCase>();

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