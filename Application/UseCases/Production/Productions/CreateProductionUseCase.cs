using Application.DTOs.Production;
using Domain.Entities;
using Domain.Interfaces.Repositories.Production;
using Domain.Interfaces.Services.Production;
using Domain.Interfaces.Services;
using Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Production.Productions;

/// <summary>
/// Caso de uso para crear un nuevo registro de producción
/// Implementa lógica compleja de consumo de recursos FIFO con transacciones
/// </summary>
public class CreateProductionUseCase
{
    private readonly IProductionRepository _productionRepository;
    private readonly IProductRepository _productRepository;
    private readonly IPlantProductionRepository _plantProductionRepository;
    private readonly IRecipeRepository _recipeRepository;
    private readonly IUnitConversionService _unitConversionService;
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateProductionUseCase> _logger;

    public CreateProductionUseCase(
        IProductionRepository productionRepository,
        IProductRepository productRepository,
        IPlantProductionRepository plantProductionRepository,
        IRecipeRepository recipeRepository,
        IUnitConversionService unitConversionService,
        IWarehouseRepository warehouseRepository,
        IUnitOfWork unitOfWork,
        ILogger<CreateProductionUseCase> logger)
    {
        _productionRepository = productionRepository;
        _productRepository = productRepository;
        _plantProductionRepository = plantProductionRepository;
        _recipeRepository = recipeRepository;
        _unitConversionService = unitConversionService;
        _warehouseRepository = warehouseRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<ProductionCreatedResponseDto> ExecuteAsync(CreateProductionDto dto)
    {
        _logger.LogInformation("🔍 Iniciando CreateProduction con ProductId: {ProductId}, Cantidad: {Quantity}", 
            dto.ProductId, dto.QuantityProduced);

        // Iniciar transacción
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            // 1. Validar producto existe
            _logger.LogInformation("🔍 Buscando producto: {ProductId}", dto.ProductId);
            var product = await _productRepository.GetByIdWithCategoryAsync(dto.ProductId);
            if (product == null)
            {
                throw new KeyNotFoundException($"El producto con ID {dto.ProductId} no existe");
            }
            _logger.LogInformation("✅ Producto encontrado: {ProductName}", product.Name);

            // 2. Validar planta existe
            _logger.LogInformation("🔍 Buscando planta: {PlantId}", dto.PlantId);
            var plant = await _plantProductionRepository.GetByIdAsync(dto.PlantId);
            if (plant == null)
            {
                throw new KeyNotFoundException($"La planta con ID {dto.PlantId} no existe");
            }
            _logger.LogInformation("✅ Planta encontrada: {PlantName}", plant.PlantName);

            if (plant.WarehouseId == Guid.Empty)
            {
                throw new InvalidOperationException("La planta de producción no tiene un almacén asociado");
            }

            // 3. Obtener recetas del producto
            _logger.LogInformation("🔍 Buscando recetas para el producto...");
            var recipes = await _recipeRepository.GetByProductIdAsync(dto.ProductId);
            var recipeList = recipes.ToList();
            _logger.LogInformation("📋 Recetas encontradas: {Count}", recipeList.Count);

            // 4. Procesar consumo de recursos según recetas
            foreach (var recipe in recipeList)
            {
                await ProcessResourceConsumptionAsync(recipe, dto.QuantityProduced, plant.WarehouseId);
            }

            // 5. Crear registro de producción
            _logger.LogInformation("🔍 Creando registro de producción...");
            var production = new Domain.Entities.Production
            {
                Id = Guid.NewGuid(),
                ProductId = dto.ProductId,
                QuantityProduced = dto.QuantityProduced,
                ProductionDate = dto.ProductionDate,
                Observation = dto.Observation ?? string.Empty,
                PlantId = dto.PlantId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var createdProduction = await _productionRepository.CreateAsync(production);
            _logger.LogInformation("✅ Producción creada con ID: {ProductionId}", createdProduction.Id);

            // 6. Crear movimiento de entrada de producto en almacén
            _logger.LogInformation("🔍 Creando movimiento de producto en almacén...");
            var warehouseMovement = new WarehouseMovementProduct
            {
                Id = Guid.NewGuid(),
                WarehouseId = plant.WarehouseId,
                ProductId = dto.ProductId,
                MovementType = "entrada",
                Quantity = dto.QuantityProduced,
                MovementDate = dto.ProductionDate,
                Observations = $"Producción de \"{product.Name}\"",
                Status = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _warehouseRepository.AddWarehouseMovementProductAsync(warehouseMovement);
            await _warehouseRepository.SaveChangesAsync();
            _logger.LogInformation("✅ Movimiento de producto creado exitosamente");

            // 7. Actualizar o crear inventario de producto en almacén
            var warehouseProduct = await _warehouseRepository.GetWarehouseProductAsync(
                dto.ProductId, plant.WarehouseId);

            if (warehouseProduct == null)
            {
                warehouseProduct = new WarehouseProduct
                {
                    Id = Guid.NewGuid(),
                    WarehouseId = plant.WarehouseId,
                    ProductId = dto.ProductId,
                    Quantity = dto.QuantityProduced,
                    EntryDate = dto.ProductionDate,
                    Status = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                await _warehouseRepository.AddWarehouseProductAsync(warehouseProduct);
            }
            else
            {
                warehouseProduct.Quantity += dto.QuantityProduced;
                warehouseProduct.UpdatedAt = DateTime.UtcNow;
                await _warehouseRepository.UpdateWarehouseProductAsync(warehouseProduct);
            }

            await _unitOfWork.SaveChangesAsync();

            // 8. Confirmar transacción
            await _unitOfWork.CommitTransactionAsync();
            _logger.LogInformation("✅ Transacción completada exitosamente");

            // 9. Retornar respuesta detallada
            return new ProductionCreatedResponseDto
            {
                Id = createdProduction.Id,
                Producto = product.Name,
                CantidadProducida = dto.QuantityProduced,
                FechaProduccion = dto.ProductionDate,
                Observacion = dto.Observation,
                Planta = plant.PlantName,
                WarehouseIdMovimiento = plant.WarehouseId,
                WarehouseProductUpdated = new WarehouseProductUpdatedDto
                {
                    Id = warehouseProduct.Id,
                    ProductId = warehouseProduct.ProductId,
                    WarehouseId = warehouseProduct.WarehouseId,
                    Quantity = warehouseProduct.Quantity,
                    EntryDate = warehouseProduct.EntryDate
                }
            };
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(ex, "❌ Error creando producción, transacción revertida");
            throw;
        }
    }

    /// <summary>
    /// Procesa el consumo de un recurso según la receta usando lógica FIFO
    /// </summary>
    private async Task ProcessResourceConsumptionAsync(Recipe recipe, int quantityProduced, Guid warehouseId)
    {
        var resourceId = recipe.ResourceId;
        var quantityPerUnit = recipe.Quantity;
        var recipeUnit = recipe.Unit ?? "unidades";

        _logger.LogInformation("🔍 Procesando recurso {ResourceId}, cantidad por unidad: {Quantity} {Unit}", 
            resourceId, quantityPerUnit, recipeUnit);

        // Calcular cantidad total requerida
        var totalRequiredInRecipeUnit = quantityPerUnit * quantityProduced;
        _logger.LogInformation("📊 Cantidad total requerida: {Total} {Unit}", 
            totalRequiredInRecipeUnit, recipeUnit);

        // Obtener compras de recursos ordenadas por fecha (FIFO)
        var buysResources = await _warehouseRepository.GetWarehouseResourcesByResourceIdAsync(
            resourceId, warehouseId);

        _logger.LogInformation("📦 Registros de compra encontrados: {Count}", buysResources.Count);

        // Calcular stock disponible con conversión de unidades
        var processedBuys = new List<(WarehouseResource buy, double convertedQuantity)>();
        double totalAvailableInRecipeUnit = 0;

        foreach (var buy in buysResources)
        {
            if (_unitConversionService.AreUnitsCompatible(buy.TypeUnit ?? "unidades", recipeUnit))
            {
                var convertedQuantity = _unitConversionService.ConvertQuantity(
                    buy.Quantity, buy.TypeUnit ?? "unidades", recipeUnit);

                if (convertedQuantity.HasValue)
                {
                    totalAvailableInRecipeUnit += convertedQuantity.Value;
                    processedBuys.Add((buy, convertedQuantity.Value));
                    _logger.LogInformation("📦 Compra ID: {BuyId}, Stock: {Quantity} {Unit} = {Converted} {RecipeUnit}",
                        buy.Id, buy.Quantity, buy.TypeUnit, convertedQuantity.Value, recipeUnit);
                }
            }
            else
            {
                _logger.LogWarning("⚠️ Unidades incompatibles: {BuyUnit} vs {RecipeUnit}", 
                    buy.TypeUnit, recipeUnit);
            }
        }

        _logger.LogInformation("📊 Cantidad total disponible: {Available} {Unit}", 
            totalAvailableInRecipeUnit, recipeUnit);

        if (totalAvailableInRecipeUnit < totalRequiredInRecipeUnit)
        {
            _logger.LogWarning("⚠️ Stock insuficiente. Disponible: {Available}, Requerido: {Required}. Continuando con stock negativo.",
                totalAvailableInRecipeUnit, totalRequiredInRecipeUnit);
        }

        // Consumir recursos con lógica FIFO
        var remainingRequired = totalRequiredInRecipeUnit;

        foreach (var (buy, convertedQuantity) in processedBuys)
        {
            if (remainingRequired <= 0) break;

            if (convertedQuantity >= remainingRequired)
            {
                // Descontar cantidad necesaria
                var quantityToDeduct = _unitConversionService.ConvertQuantity(
                    remainingRequired, recipeUnit, buy.TypeUnit ?? "unidades");

                if (quantityToDeduct.HasValue)
                {
                    _logger.LogInformation("✅ Descontando {Required} {Unit} = {Deduct} {OriginalUnit} de compra {BuyId}",
                        remainingRequired, recipeUnit, quantityToDeduct.Value, buy.TypeUnit, buy.Id);
                    
                    buy.Quantity -= quantityToDeduct.Value;
                    buy.UpdatedAt = DateTime.UtcNow;
                    await _warehouseRepository.UpdateWarehouseResourceAsync(buy);
                    remainingRequired = 0;
                }
            }
            else
            {
                // Consumir todo el registro
                _logger.LogInformation("✅ Consumiendo completamente compra {BuyId} ({Quantity} {Unit})",
                    buy.Id, convertedQuantity, recipeUnit);
                
                remainingRequired -= convertedQuantity;
                buy.Quantity = 0;
                buy.UpdatedAt = DateTime.UtcNow;
                await _warehouseRepository.UpdateWarehouseResourceAsync(buy);
            }
        }

        // Permitir valores negativos en el último registro si no hay suficiente stock
        if (remainingRequired > 0 && processedBuys.Count > 0)
        {
            var lastBuy = processedBuys[^1].buy;
            var quantityToDeduct = _unitConversionService.ConvertQuantity(
                remainingRequired, recipeUnit, lastBuy.TypeUnit ?? "unidades");

            if (quantityToDeduct.HasValue)
            {
                _logger.LogWarning("⚠️ Restando faltante {Remaining} {Unit} del último registro. Permitiendo negativo.",
                    remainingRequired, recipeUnit);
                
                lastBuy.Quantity -= quantityToDeduct.Value;
                lastBuy.UpdatedAt = DateTime.UtcNow;
                await _warehouseRepository.UpdateWarehouseResourceAsync(lastBuy);
                remainingRequired = 0;
            }
        }

        await _warehouseRepository.SaveChangesAsync();

        // Crear movimiento de salida de recurso
        var resource = await _warehouseRepository.GetResourceByIdAsync(resourceId);
        var resourceName = resource?.Name ?? "Recurso desconocido";

        var resourceMovement = new WarehouseMovementResource
        {
            Id = Guid.NewGuid(),
            WarehouseId = warehouseId,
            ResourceId = resourceId,
            MovementType = "salida",
            Quantity = (int)Math.Round(totalRequiredInRecipeUnit),
            MovementDate = DateTime.UtcNow,
            Observations = $"Consumo de recurso \"{resourceName}\" para producción",
            Status = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _warehouseRepository.AddWarehouseMovementResourceAsync(resourceMovement);
        await _warehouseRepository.SaveChangesAsync();

        _logger.LogInformation("✅ Recurso {ResourceId} procesado exitosamente", resourceId);
    }
}
