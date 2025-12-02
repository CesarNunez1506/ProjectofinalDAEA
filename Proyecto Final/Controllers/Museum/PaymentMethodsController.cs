using Application.DTOs.Museum;
using Application.UseCases.Museum.PaymentMethods.Commands;
using Application.UseCases.Museum.PaymentMethods.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.Museum;

// Controlador para gestionar los métodos de pago del museo
[ApiController]
[Route("api/museum/payment-methods")]
[Tags("Museum")]
public class PaymentMethodsController : ControllerBase
{
    private readonly CreatePaymentMethodUseCase _createPaymentMethod;
    private readonly UpdatePaymentMethodUseCase _updatePaymentMethod;
    private readonly DeletePaymentMethodUseCase _deletePaymentMethod;
    private readonly GetAllPaymentMethodsUseCase _getAllPaymentMethods;
    private readonly GetPaymentMethodByIdUseCase _getPaymentMethodById;

    public PaymentMethodsController(
        CreatePaymentMethodUseCase createPaymentMethod,
        UpdatePaymentMethodUseCase updatePaymentMethod,
        DeletePaymentMethodUseCase deletePaymentMethod,
        GetAllPaymentMethodsUseCase getAllPaymentMethods,
        GetPaymentMethodByIdUseCase getPaymentMethodById)
    {
        _createPaymentMethod = createPaymentMethod;
        _updatePaymentMethod = updatePaymentMethod;
        _deletePaymentMethod = deletePaymentMethod;
        _getAllPaymentMethods = getAllPaymentMethods;
        _getPaymentMethodById = getPaymentMethodById;
    }

    // Controlador para gestionar los métodos de pago del museo
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var paymentMethods = await _getAllPaymentMethods.ExecuteAsync();
            return Ok(paymentMethods);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
    
    // Controlador para gestionar los métodos de pago del museo
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var paymentMethod = await _getPaymentMethodById.ExecuteAsync(id);
            return Ok(paymentMethod);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    // Controlador para gestionar los métodos de pago del museo
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePaymentMethodDto dto)
    {
        try
        {
            var paymentMethod = await _createPaymentMethod.ExecuteAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = paymentMethod.Id }, paymentMethod);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    // Controlador para gestionar los métodos de pago del museo
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePaymentMethodDto dto)
    {
        try
        {
            var paymentMethod = await _updatePaymentMethod.ExecuteAsync(id, dto);
            return Ok(paymentMethod);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    // Controlador para gestionar los métodos de pago del museo
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _deletePaymentMethod.ExecuteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}
