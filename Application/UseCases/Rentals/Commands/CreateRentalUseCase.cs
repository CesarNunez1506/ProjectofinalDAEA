using Application.DTOs.Rentals;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Rentals;

public class CreateRentalUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateRentalUseCase> _logger;

    public CreateRentalUseCase(
        IUnitOfWork unitOfWork,
        ILogger<CreateRentalUseCase> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<RentalCreatedResponseDto> ExecuteAsync(CreateRentalDto dto)
    {
        _logger.LogInformation("Iniciando creación de alquiler para PlaceId: {PlaceId}, Fechas: {StartDate} - {EndDate}",
            dto.PlaceId, dto.StartDate, dto.EndDate);

        if (dto.StartDate >= dto.EndDate)
        {
            throw new ArgumentException("La fecha de inicio debe ser anterior a la fecha de fin");
        }

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var customerRepo = _unitOfWork.GetRepository<Customer>();
            var placeRepo = _unitOfWork.GetRepository<Place>();
            var moduleRepo = _unitOfWork.GetRepository<Module>();
            var financialReportRepo = _unitOfWork.GetRepository<FinancialReport>();
            var generalIncomeRepo = _unitOfWork.GetRepository<GeneralIncome>();
            var userRepo = _unitOfWork.GetRepository<User>();

            var customer = await customerRepo.GetByIdAsync(dto.CustomerId);
            if (customer == null)
            {
                throw new KeyNotFoundException($"El cliente con ID {dto.CustomerId} no existe");
            }
            _logger.LogInformation("Cliente encontrado: {CustomerName}", customer.FullName);

            var place = await placeRepo.GetAsync(
                filter: p => p.Id == dto.PlaceId,
                includeProperties: "Location");
            
            var placeEntity = place.FirstOrDefault();
            if (placeEntity == null)
            {
                throw new KeyNotFoundException($"El lugar con ID {dto.PlaceId} no existe");
            }
            _logger.LogInformation("Lugar encontrado: {PlaceName}", placeEntity.Name);

            _logger.LogInformation("Verificando solapamiento de fechas para el lugar...");
            var rentalRepo = _unitOfWork.GetRepository<Rental>();
            var overlaps = await rentalRepo.FindAsync(r => 
                r.PlaceId == dto.PlaceId && 
                r.Status == true &&
                r.StartDate <= dto.EndDate && 
                r.EndDate >= dto.StartDate);
            
            var overlap = overlaps.FirstOrDefault();
            if (overlap != null)
            {
                throw new InvalidOperationException(
                    $"No se puede crear el alquiler: ya existe un alquiler activo para este lugar en el período indicado. " +
                    $"Alquiler existente desde {overlap.StartDate:yyyy-MM-dd} hasta {overlap.EndDate:yyyy-MM-dd}");
            }
            _logger.LogInformation("No hay solapamiento de fechas");

            var rental = new Rental
            {
                Id = Guid.NewGuid(),
                CustomerId = dto.CustomerId,
                PlaceId = dto.PlaceId,
                UserId = dto.UserId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Amount = dto.Amount,
                Status = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var rentalRepoForAdd = _unitOfWork.GetRepository<Rental>();
            await rentalRepoForAdd.AddAsync(rental);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Alquiler creado con ID: {RentalId}", rental.Id);

            _logger.LogInformation("Creando ingreso financiero asociado...");
            var incomeDto = await CreateRentalIncomeAsync(
                rental, customer, placeEntity, 
                moduleRepo, financialReportRepo, generalIncomeRepo, userRepo);
            _logger.LogInformation("Ingreso financiero creado con ID: {IncomeId}", incomeDto.Id);

            await _unitOfWork.CommitTransactionAsync();
            _logger.LogInformation("Transacción confirmada exitosamente");

            return new RentalCreatedResponseDto
            {
                Id = rental.Id,
                Cliente = customer.FullName,
                Lugar = placeEntity.Name,
                Area = placeEntity.Area,
                FechaInicio = rental.StartDate,
                FechaFin = rental.EndDate,
                Monto = rental.Amount,
                Status = rental.Status,
                IngresoGenerado = incomeDto
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear el alquiler, realizando rollback");
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    private async Task<GeneralIncomeCreatedDto> CreateRentalIncomeAsync(
        Rental rental, 
        Customer customer, 
        Place place,
        Domain.Interfaces.Repositories.IRepository<Module> moduleRepo,
        Domain.Interfaces.Repositories.IRepository<FinancialReport> financialReportRepo,
        Domain.Interfaces.Repositories.IRepository<GeneralIncome> generalIncomeRepo,
        Domain.Interfaces.Repositories.IRepository<User> userRepo)
    {
        var rentalsModule = await moduleRepo.FirstOrDefaultAsync(m => m.Name == "Alquileres");

        if (rentalsModule == null)
        {
            throw new InvalidOperationException("Módulo 'Alquileres' no encontrado en la base de datos");
        }

        // El reporte activo es el que no tiene fecha de fin (EndDate == null)
        var activeReport = await financialReportRepo.FirstOrDefaultAsync(r => r.EndDate == null);

        var user = await userRepo.GetByIdAsync(rental.UserId);
        var userName = user?.Name ?? "Usuario";

        var description = $"Ingreso por alquiler: {place.Name} ({place.Area}) - " +
                         $"Cliente: {customer.FullName} - Vendedor: {userName} - " +
                         $"Desde: {rental.StartDate:yyyy-MM-dd} - Hasta: {rental.EndDate:yyyy-MM-dd}";

        var generalIncome = new GeneralIncome
        {
            Id = Guid.NewGuid(),
            ModuleId = rentalsModule.Id,
            IncomeType = "Alquiler",
            Amount = rental.Amount,
            Date = DateTime.UtcNow,
            Description = description,
            ReportId = activeReport?.Id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await generalIncomeRepo.AddAsync(generalIncome);
        await _unitOfWork.SaveChangesAsync();

        return new GeneralIncomeCreatedDto
        {
            Id = generalIncome.Id,
            ModuleName = rentalsModule.Name,
            IncomeType = generalIncome.IncomeType,
            Amount = generalIncome.Amount,
            Date = generalIncome.Date,
            Description = generalIncome.Description ?? string.Empty
        };
    }
}
