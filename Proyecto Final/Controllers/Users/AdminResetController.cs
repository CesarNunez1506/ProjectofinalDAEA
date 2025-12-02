using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Repositories.Users;
using Domain.Interfaces.Services.Users;

namespace Proyecto_Final.Controllers.Users;

[ApiController]
[Route("api/[controller]")]
public class AdminResetController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashService _passwordHashService;

    public AdminResetController(
        IUserRepository userRepository,
        IPasswordHashService passwordHashService)
    {
        _userRepository = userRepository;
        _passwordHashService = passwordHashService;
    }

    [HttpPost("reset-admin-password")]
    public async Task<IActionResult> ResetAdminPassword([FromBody] ResetPasswordRequest request)
    {
        try
        {
            // Buscar usuario por email
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }

            // Hash the new password
            user.Password = _passwordHashService.HashPassword(request.NewPassword);
            
            // Save changes
            await _userRepository.UpdateAsync(user);

            return Ok(new 
            { 
                message = "Contraseña actualizada exitosamente",
                email = user.Email,
                hashedPassword = user.Password.Substring(0, 20) + "..." // Solo mostrar parte del hash
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al actualizar contraseña", error = ex.Message });
        }
    }
}

public record ResetPasswordRequest(string Email, string NewPassword);
