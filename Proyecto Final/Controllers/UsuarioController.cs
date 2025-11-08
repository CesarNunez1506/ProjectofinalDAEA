using Microsoft.AspNetCore.Mvc;
using Application.DTOs;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpPost("registrar")]
        public IActionResult Registrar(UsuarioDto dto)
        {
            // TODO: implementar caso de uso en Application
            return Accepted();
        }
    }
}
