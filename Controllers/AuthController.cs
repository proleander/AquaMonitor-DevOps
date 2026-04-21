using AquaMonitor.Api.Security;
using Microsoft.AspNetCore.Mvc;

namespace AquaMonitor.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            // LOGIN SIMPLES PARA PROJETO
            if (username == "admin" && password == "123")
            {
                var token = TokenService.GenerateToken(username);
                return Ok(new { token });
            }

            return Unauthorized("Credenciais inválidas");
        }
    }
}
