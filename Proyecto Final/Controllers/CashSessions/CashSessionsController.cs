using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Repositories;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CashSessionsController : ControllerBase
    {
        private readonly ICashSessionRepository _repo;

        public CashSessionsController(ICashSessionRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _repo.GetByIdAsync(id));
        }
    }
}
