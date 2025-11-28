using Application.DTOs.Finance;
using Application.UseCases.Finance.MonasteryExpenses;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.Finance;

/// <summary>
/// Controlador para gestión de gastos del monasterio
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class MonasteryExpensesController : ControllerBase
{
    private readonly CreateMonasteryExpenseUseCase _createUseCase;
    private readonly GetAllMonasteryExpensesUseCase _getAllUseCase;
    private readonly GetMonasteryExpenseByIdUseCase _getByIdUseCase;
    private readonly UpdateMonasteryExpenseUseCase _updateUseCase;
    private readonly DeleteMonasteryExpenseUseCase _deleteUseCase;
    private readonly ILogger<MonasteryExpensesController> _logger;

    public MonasteryExpensesController(
        CreateMonasteryExpenseUseCase createUseCase,
        GetAllMonasteryExpensesUseCase getAllUseCase,
        GetMonasteryExpenseByIdUseCase getByIdUseCase,
        UpdateMonasteryExpenseUseCase updateUseCase,
        DeleteMonasteryExpenseUseCase deleteUseCase,
        ILogger<MonasteryExpensesController> logger)
    {
        _createUseCase = createUseCase;
        _getAllUseCase = getAllUseCase;
        _getByIdUseCase = getByIdUseCase;
        _updateUseCase = updateUseCase;
        _deleteUseCase = deleteUseCase;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene todos los gastos del monasterio
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MonasteryExpenseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MonasteryExpenseDto>>> GetAll()
    {
        try
        {
            var expenses = await _getAllUseCase.ExecuteAsync();
            return Ok(expenses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener gastos del monasterio");
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Obtiene un gasto del monasterio por ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MonasteryExpenseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MonasteryExpenseDto>> GetById(Guid id)
    {
        try
        {
            var expense = await _getByIdUseCase.ExecuteAsync(id);

            if (expense == null)
                return NotFound(new { error = $"Gasto con ID {id} no encontrado" });

            return Ok(expense);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener gasto {ExpenseId}", id);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Crea un nuevo gasto del monasterio
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(MonasteryExpenseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MonasteryExpenseDto>> Create([FromBody] CreateMonasteryExpenseDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var expense = await _createUseCase.ExecuteAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = expense.Id },
                expense);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear gasto del monasterio");
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Actualiza un gasto del monasterio
    /// </summary>
    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(MonasteryExpenseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MonasteryExpenseDto>> Update(Guid id, [FromBody] UpdateMonasteryExpenseDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var expense = await _updateUseCase.ExecuteAsync(id, dto);
            return Ok(expense);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar gasto {ExpenseId}", id);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Elimina un gasto del monasterio
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _deleteUseCase.ExecuteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar gasto {ExpenseId}", id);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }
}
