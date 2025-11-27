using Application.DTOs.Rentals;
using Domain.Entities;
using Domain.Interfaces.Repositories.Rentals;
using Domain.Interfaces.Repositories.Finance;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Rentals.Rentals;

public class CreateRentalUseCase
{
    private readonly IRentalRepository _rentalRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IPlaceRepository _placeRepository;
    private readonly IModuleRepository _moduleRepository;
    private readonly IFinancialReportRepository _financialReportRepository;
    private readonly IGeneralIncomeRepository _generalIncomeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateRentalUseCase> _logger;

    public CreateRentalUseCase(
        IRentalRepository rentalRepository,
        ICustomerRepository customerRepository,
        IPlaceRepository placeRepository,
        IModuleRepository moduleRepository,
        IFinancialReportRepository financialReportRepository,
        IGeneralIncomeRepository generalIncomeRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        ILogger<CreateRentalUseCase> logger)
    {
        _rentalRepository = rentalRepository;
        _customerRepository = customerRepository;
        _placeRepository = placeRepository;
        _moduleRepository = moduleRepository;
        _financialReportRepository = financialReportRepository;
        _generalIncomeRepository = generalIncomeRepository;
        _userRepository = userRepository;
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
            var customer = await _customerRepository.GetByIdAsync(dto.CustomerId);
            if (customer == null)
            {
                throw new KeyNotFoundException($"El cliente con ID {dto.CustomerId} no existe");
            }
            _logger.LogInformation("Cliente encontrado: {CustomerName}", customer.FullName);

            var place = await _placeRepository.GetByIdWithRelationsAsync(dto.PlaceId);
            if (place == null)
            {
                throw new KeyNotFoundException($"El lugar con ID {dto.PlaceId} no existe");
            }
            _logger.LogInformation("Lugar encontrado: {PlaceName}", place.Name);

            _logger.LogInformation("Verificando solapamiento de fechas para el lugar...");
            var overlap = await _rentalRepository.CheckOverlapAsync(dto.PlaceId, dto.StartDate, dto.EndDate);
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

            var createdRental = await _rentalRepository.CreateAsync(rental);
            _logger.LogInformation("Alquiler creado con ID: {RentalId}", createdRental.Id);

            _logger.LogInformation("Creando ingreso financiero asociado...");
            var incomeDto = await CreateRentalIncomeAsync(createdRental, customer, place);
            _logger.LogInformation("Ingreso financiero creado con ID: {IncomeId}", incomeDto.Id);

            await _unitOfWork.CommitTransactionAsync();
            _logger.LogInformation("Transacción confirmada exitosamente");

            return new RentalCreatedResponseDto
            {
                Id = createdRental.Id,
                Cliente = customer.FullName,
                Lugar = place.Name,
                Area = place.Area,
                FechaInicio = createdRental.StartDate,
                FechaFin = createdRental.EndDate,
                Monto = createdRental.Amount,
                Status = createdRental.Status,
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

    private async Task<GeneralIncomeCreatedDto> CreateRentalIncomeAsync(Rental rental, Customer customer, Place place)
    {
        var rentalsModule = await _moduleRepository.GetByNameAsync("Alquileres");

        if (rentalsModule == null)
        {
            throw new InvalidOperationException("Módulo 'Alquileres' no encontrado en la base de datos");
        }

        var activeReport = await _financialReportRepository.GetActiveReportAsync();

        var user = await _userRepository.GetByIdAsync(rental.UserId);
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

        var createdIncome = await _generalIncomeRepository.CreateAsync(generalIncome);

        return new GeneralIncomeCreatedDto
        {
            Id = createdIncome.Id,
            ModuleName = rentalsModule.Name,
            IncomeType = createdIncome.IncomeType,
            Amount = createdIncome.Amount,
            Date = createdIncome.Date,
            Description = createdIncome.Description ?? string.Empty
        };
    }
}
