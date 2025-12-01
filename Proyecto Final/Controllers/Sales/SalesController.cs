using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Sales.Commands.CreateSale;
using Application.Features.Sales.Commands.UpdateSale;
using Application.Features.Sales.Commands.CancelSale;
using Application.Features.Sales.Commands.ProcessRefund;
using Application.Features.Sales.Queries.GetSaleById;
using Application.Features.Sales.Queries.GetSalesByStore;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale(CreateSaleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSale(Guid id, UpdateSaleCommand command)
        {
            command.SaleId = id;
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelSale(Guid id)
        {
            return Ok(await _mediator.Send(new CancelSaleCommand(id)));
        }

        [HttpPost("refund/{id}")]
        public async Task<IActionResult> Refund(Guid id)
        {
            return Ok(await _mediator.Send(new ProcessRefundCommand(id)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSale(Guid id)
        {
            return Ok(await _mediator.Send(new GetSaleByIdQuery(id)));
        }

        [HttpGet("store/{storeId}")]
        public async Task<IActionResult> GetSalesByStore(Guid storeId)
        {
            return Ok(await _mediator.Send(new GetSalesByStoreQuery(storeId)));
        }
    }
}
