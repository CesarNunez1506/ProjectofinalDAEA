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
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext (ya existente en el proyecto)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar LocalDbContext para módulo de producción
builder.Services.AddDbContext<LocalDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar MediatR para CQRS
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Application.DTOs.Finance.ExpenseDto).Assembly));

// Agregar AutoMapper
builder.Services.AddAutoMapper(typeof(Application.Mappings.UserMappingProfile).Assembly);

// Agregar FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Application.Validators.Users.CreateUserDtoValidator>();

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
        Description = "API del Sistema ERP Santa Teresa - Módulos de Producción y Usuarios"
    });

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
builder.Services.AddScoped<IModuleRepository, ModuleRepository>();

// ========== SERVICIOS DEL MÓDULO DE USUARIOS ==========
builder.Services.AddScoped<IPasswordHashService, PasswordHashService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

// ========== REPOSITORIOS DEL MÓDULO DE PRODUCCIÓN ==========


// ======= FINANZAS - Repositorios =======
//builder.Services.AddScoped(typeof(Domain.Interfaces.Repositories.IGenericRepository<>), typeof(Infrastructure.Repositories.GenericRepository<>));
builder.Services.AddScoped<Domain.Interfaces.Repositories.Finanzas.IGeneralIncomeRepository, Infrastructure.Repositories.GeneralIncomeRepository>();
builder.Services.AddScoped<Domain.Interfaces.Repositories.Finanzas.IGeneralExpenseRepository, Infrastructure.Repositories.GeneralExpenseRepository>();
builder.Services.AddScoped<Domain.Interfaces.Repositories.IOverheadRepository, Infrastructure.Repositories.OverheadRepository>();
builder.Services.AddScoped<Domain.Interfaces.Repositories.Finanzas.IFinancialReportRepository, Infrastructure.Repositories.FinancialReportRepository>();

// Registrar UnitOfWork para que los casos de uso puedan inyectarlo
builder.Services.AddScoped<Domain.Interfaces.Services.IUnitOfWork, Infrastructure.Services.UnitOfWork>();

// Servicios del módulo de Producción (utilizados por los UseCases)
builder.Services.AddScoped<Domain.Interfaces.Services.Production.IFileStorageService, Infrastructure.Services.Production.FileStorageService>();
builder.Services.AddScoped<Domain.Interfaces.Services.Production.IUnitConversionService, Infrastructure.Services.Production.UnitConversionService>();
// Repositorios necesarios para casos de uso de Producción
builder.Services.AddScoped<Domain.Interfaces.Repositories.IWarehouseRepository, Infrastructure.Repositories.WarehouseRepository>();

// FINANZAS: Se eliminó el registro directo de adaptadores individuales.
// Restaurando registro de adaptadores/handlers en la estructura Commands/Queries
// Registrar adaptadores (Commands / Queries) para inyección en controladores
builder.Services.AddScoped<Application.UseCases.Finance.Commands.FinancialReports.CreateIncomeCommand>();
builder.Services.AddScoped<Application.UseCases.Finance.Commands.FinancialReports.CreateExpenseCommand>();
builder.Services.AddScoped<Application.UseCases.Finance.Commands.FinancialReports.RecordOverheadCommand>();
builder.Services.AddScoped<Application.UseCases.Finance.Commands.FinancialReports.GenerateFinancialReportCommand>();

builder.Services.AddScoped<Application.UseCases.Finance.Queries.FinancialReports.GetIncomesByPeriodQuery>();
builder.Services.AddScoped<Application.UseCases.Finance.Queries.FinancialReports.GetExpensesByPeriodQuery>();
builder.Services.AddScoped<Application.UseCases.Finance.Queries.FinancialReports.GetFinancialReportByDateQuery>();
builder.Services.AddScoped<Application.UseCases.Finance.Queries.FinancialReports.GetProfitLossStatementQuery>();

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

app.UseCors("AllowFrontend");
app.UseStaticFiles(); // Para servir archivos desde wwwroot/ (imágenes de productos)

app.UseHttpsRedirection();
app.UseAuthentication(); // IMPORTANTE: Authentication antes de Authorization
app.UseAuthorization();
app.MapControllers();

app.Run();