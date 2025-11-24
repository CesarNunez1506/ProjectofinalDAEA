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
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext (ya existente en el proyecto)
builder.Services.AddDbContext<LocalDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar controladores
builder.Services.AddControllers();

// Configurar Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ST-ERP Production API",
        Version = "v1",
        Description = "API del módulo de producción del Sistema ERP Santa Teresa"
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

// ========== REPOSITORIOS DEL MÓDULO DE PRODUCCIÓN ==========
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IPlantProductionRepository, PlantProductionRepository>();
builder.Services.AddScoped<IProductionRepository, ProductionRepository>();
builder.Services.AddScoped<ILostRepository, LostRepository>();

// ========== SERVICIOS DEL MÓDULO DE PRODUCCIÓN ==========
builder.Services.AddScoped<IUnitConversionService, UnitConversionService>();
builder.Services.AddScoped<IFileStorageService, FileStorageService>();

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

// ========== CASOS DE USO - PLANTAS DE PRODUCCIÓN ==========
builder.Services.AddScoped<CreatePlantProductionUseCase>();
builder.Services.AddScoped<GetAllPlantProductionsUseCase>();
builder.Services.AddScoped<GetPlantProductionByIdUseCase>();
builder.Services.AddScoped<UpdatePlantProductionUseCase>();
builder.Services.AddScoped<DeletePlantProductionUseCase>();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "ST-ERP Production API v1");
        options.RoutePrefix = "swagger"; // Acceder desde: http://localhost:5000/swagger
    });
}

app.UseCors("AllowFrontend");
app.UseStaticFiles(); // Para servir archivos desde wwwroot/ (imágenes de productos)

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();