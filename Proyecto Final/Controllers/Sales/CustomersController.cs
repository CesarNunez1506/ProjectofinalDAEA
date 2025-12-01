using Application.DTOs.Sales;
using Application.Features.Customers.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly GetCustomerByIdQuery _getCustomer;

    public CustomersController(GetCustomerByIdQuery getCustomer)
    {
        _getCustomer = getCustomer;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomer(Guid id)
    {
        var result = await _getCustomer.ExecuteAsync(id);
        return Ok(result);
    }
}
