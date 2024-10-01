using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SERVPRO.API.Data;
using SERVPRO.API.Helpers;
using SERVPRO.API.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SERVPRO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly WebDbContext _context;

        public AuthController(WebDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel loginModel)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Cpf == loginModel.Cpf); 

            if (user == null || !PasswordHelper.VerifyPassword(loginModel.Password, user.Password))
            {
                return Unauthorized("Usuário ou senha inválidos.");
            }

            return Ok($"Login realizado com sucesso como {user.Role}.");
        }
    }
}
