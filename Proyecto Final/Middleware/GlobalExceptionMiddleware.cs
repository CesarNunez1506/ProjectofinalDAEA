using Domain.Exceptions.Inventory;
using System.Net;
using System.Text.Json;

namespace Proyecto_Final.Middleware;

/// <summary>
/// Middleware global para manejo de excepciones
/// Captura todas las excepciones y devuelve respuestas HTTP apropiadas
/// </summary>
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Excepción no controlada: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        switch (exception)
        {
            // Excepciones de Inventario - Not Found
            case ProductNotFoundException productNotFound:
                code = HttpStatusCode.NotFound;
                result = JsonSerializer.Serialize(new
                {
                    error = "Producto no encontrado",
                    message = productNotFound.Message,
                    productId = productNotFound.ProductId
                });
                break;

            case CategoryNotFoundException categoryNotFound:
                code = HttpStatusCode.NotFound;
                result = JsonSerializer.Serialize(new
                {
                    error = "Categoría no encontrada",
                    message = categoryNotFound.Message,
                    categoryId = categoryNotFound.CategoryId
                });
                break;

            case WarehouseNotFoundException warehouseNotFound:
                code = HttpStatusCode.NotFound;
                result = JsonSerializer.Serialize(new
                {
                    error = "Almacén no encontrado",
                    message = warehouseNotFound.Message,
                    warehouseId = warehouseNotFound.WarehouseId
                });
                break;

            case SupplierNotFoundException supplierNotFound:
                code = HttpStatusCode.NotFound;
                result = JsonSerializer.Serialize(new
                {
                    error = "Proveedor no encontrado",
                    message = supplierNotFound.Message,
                    supplierId = supplierNotFound.SupplierId
                });
                break;

            // Excepciones de negocio - Bad Request
            case InsufficientStockException insufficientStock:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(new
                {
                    error = "Stock insuficiente",
                    message = insufficientStock.Message,
                    productId = insufficientStock.ProductId,
                    warehouseId = insufficientStock.WarehouseId,
                    requiredQuantity = insufficientStock.RequiredQuantity,
                    availableQuantity = insufficientStock.AvailableQuantity
                });
                break;

            case DuplicateSupplierException duplicateSupplier:
                code = HttpStatusCode.Conflict;
                result = JsonSerializer.Serialize(new
                {
                    error = "Proveedor duplicado",
                    message = duplicateSupplier.Message,
                    ruc = duplicateSupplier.Ruc
                });
                break;

            case WarehouseCapacityExceededException capacityExceeded:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(new
                {
                    error = "Capacidad de almacén excedida",
                    message = capacityExceeded.Message,
                    warehouseId = capacityExceeded.WarehouseId,
                    capacity = capacityExceeded.Capacity,
                    currentLoad = capacityExceeded.CurrentLoad
                });
                break;

            // Excepciones genéricas
            case InvalidOperationException invalidOperation:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(new
                {
                    error = "Operación inválida",
                    message = invalidOperation.Message
                });
                break;

            case ArgumentException argumentException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(new
                {
                    error = "Argumento inválido",
                    message = argumentException.Message
                });
                break;

            // Error desconocido
            default:
                result = JsonSerializer.Serialize(new
                {
                    error = "Error interno del servidor",
                    message = exception.Message
                });
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(result);
    }
}

/// <summary>
/// Extensión para registrar el middleware fácilmente
/// </summary>
public static class GlobalExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionMiddleware>();
    }
}
