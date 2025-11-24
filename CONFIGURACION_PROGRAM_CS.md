# CONFIGURACIÓN DE INYECCIÓN DE DEPENDENCIAS
## Para agregar en Program.cs

## 1. Agregar namespace al inicio del archivo
```csharp
using Domain.Interfaces.Repositories.Production;
using Domain.Interfaces.Services.Production;
using Infrastructure.Repositories.Production;
using Infrastructure.Services.Production;
using Application.UseCases.Production.Categories;
using Application.UseCases.Production.Products;
using Application.UseCases.Production.Recipes;
using Application.UseCases.Production.PlantProductions;
using Application.UseCases.Production.Productions;
using Application.UseCases.Production.Losts;
```

## 2. Registrar servicios ANTES de builder.Build()

### Repositorios (Scoped - una instancia por request HTTP)
```csharp
// ========== REPOSITORIOS DEL MÓDULO DE PRODUCCIÓN ==========
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IPlantProductionRepository, PlantProductionRepository>();
builder.Services.AddScoped<IProductionRepository, ProductionRepository>();
builder.Services.AddScoped<ILostRepository, LostRepository>();
```

### Servicios de Dominio (Scoped)
```csharp
// ========== SERVICIOS DEL MÓDULO DE PRODUCCIÓN ==========
builder.Services.AddScoped<IUnitConversionService, UnitConversionService>();
builder.Services.AddScoped<IFileStorageService, FileStorageService>();
```

### Casos de Uso - Categorías
```csharp
// ========== CASOS DE USO - CATEGORÍAS ==========
builder.Services.AddScoped<CreateCategoryUseCase>();
builder.Services.AddScoped<GetAllCategoriesUseCase>();
builder.Services.AddScoped<GetCategoryByIdUseCase>();
builder.Services.AddScoped<UpdateCategoryUseCase>();
builder.Services.AddScoped<DeleteCategoryUseCase>();
```

### Casos de Uso - Productos
```csharp
// ========== CASOS DE USO - PRODUCTOS ==========
builder.Services.AddScoped<CreateProductUseCase>();
builder.Services.AddScoped<GetAllProductsUseCase>();
builder.Services.AddScoped<GetProductByIdUseCase>();
builder.Services.AddScoped<UpdateProductUseCase>();
builder.Services.AddScoped<DeleteProductUseCase>();
```

### Casos de Uso - Recetas
```csharp
// ========== CASOS DE USO - RECETAS ==========
builder.Services.AddScoped<CreateRecipeUseCase>();
builder.Services.AddScoped<GetAllRecipesUseCase>();
builder.Services.AddScoped<GetRecipesByProductIdUseCase>();
builder.Services.AddScoped<UpdateRecipeUseCase>();
builder.Services.AddScoped<DeleteRecipeUseCase>();
```

### Casos de Uso - Plantas de Producción
```csharp
// ========== CASOS DE USO - PLANTAS ==========
builder.Services.AddScoped<CreatePlantProductionUseCase>();
builder.Services.AddScoped<GetAllPlantsUseCase>();
builder.Services.AddScoped<GetPlantByIdUseCase>();
builder.Services.AddScoped<UpdatePlantUseCase>();
builder.Services.AddScoped<DeletePlantUseCase>();
```

### Casos de Uso - Producciones
```csharp
// ========== CASOS DE USO - PRODUCCIONES ==========
builder.Services.AddScoped<CreateProductionUseCase>();
builder.Services.AddScoped<GetAllProductionsUseCase>();
builder.Services.AddScoped<GetProductionByIdUseCase>();
builder.Services.AddScoped<UpdateProductionUseCase>();
builder.Services.AddScoped<ToggleProductionStatusUseCase>();
```

### Casos de Uso - Pérdidas
```csharp
// ========== CASOS DE USO - PÉRDIDAS ==========
builder.Services.AddScoped<CreateLostUseCase>();
builder.Services.AddScoped<GetAllLostsUseCase>();
builder.Services.AddScoped<GetLostByIdUseCase>();
builder.Services.AddScoped<UpdateLostUseCase>();
builder.Services.AddScoped<DeleteLostUseCase>();
```

## 3. Configurar CORS (si es necesario para el frontend)
```csharp
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
```

## 4. Configurar Swagger con documentación XML
```csharp
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ST-ERP Production API",
        Version = "v1",
        Description = "API del módulo de producción del Sistema ERP Santa Teresa"
    });

    // Incluir comentarios XML en Swagger
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
```

## 5. Después de var app = builder.Build()

### Habilitar CORS
```csharp
app.UseCors("AllowFrontend");
```

### Habilitar archivos estáticos (para imágenes de productos)
```csharp
app.UseStaticFiles(); // Sirve archivos desde wwwroot/
```

### Configurar Swagger
```csharp
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "ST-ERP Production API v1");
        options.RoutePrefix = "swagger"; // Acceder desde: http://localhost:5000/swagger
    });
}
```

## 6. Configuración completa de ejemplo

```csharp
using Domain.Interfaces.Repositories.Production;
using Domain.Interfaces.Services.Production;
using Infrastructure.Repositories.Production;
using Infrastructure.Services.Production;
using Application.UseCases.Production.Categories;
using Application.UseCases.Production.Productions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext
builder.Services.AddDbContext<LocalDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar controladores
builder.Services.AddControllers();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// ========== MÓDULO DE PRODUCCIÓN ==========

// Repositorios
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IPlantProductionRepository, PlantProductionRepository>();
builder.Services.AddScoped<IProductionRepository, ProductionRepository>();
builder.Services.AddScoped<ILostRepository, LostRepository>();

// Servicios
builder.Services.AddScoped<IUnitConversionService, UnitConversionService>();
builder.Services.AddScoped<IFileStorageService, FileStorageService>();

// Casos de Uso - Categorías
builder.Services.AddScoped<CreateCategoryUseCase>();
builder.Services.AddScoped<GetAllCategoriesUseCase>();
builder.Services.AddScoped<GetCategoryByIdUseCase>();
builder.Services.AddScoped<UpdateCategoryUseCase>();
builder.Services.AddScoped<DeleteCategoryUseCase>();

// Casos de Uso - Producciones
builder.Services.AddScoped<CreateProductionUseCase>();
// ... agregar resto de casos de uso

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");
app.UseStaticFiles(); // Para servir imágenes

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

## 7. Configurar appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=local;Username=admin;Password=admin123"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Application.UseCases.Production": "Debug"
    }
  },
  "AllowedHosts": "*"
}
```

## 8. Habilitar documentación XML (opcional)

En el archivo `.csproj` del proyecto API:

```xml
<PropertyGroup>
  <GenerateDocumentationFile>true</GenerateDocumentationFile>
  <NoWarn>$(NoWarn);1591</NoWarn>
</PropertyGroup>
```

Esto permite que Swagger muestre los comentarios XML (///) de los controladores y DTOs.
