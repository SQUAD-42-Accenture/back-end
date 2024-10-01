using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SERVPRO.API.Data;
using SERVPRO.API.Helpers;
using SERVPRO.API.Models;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SERVPRO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly WebDbContext _context;

        public RegisterController(WebDbContext context)
        {
            _context = context;
        }

        [HttpPost("register-user")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterUser([FromBody] UserModel registerModel)
        {
            // Verifica se o tipo de usuário é válido
            if (registerModel.Role != "Technician" && registerModel.Role != "Customer" && registerModel.Role != "Admin")
            {
                return BadRequest("Tipo de usuário inválido.");
            }

            var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Cpf == registerModel.Cpf);
            if (existingUser != null)
            {
                return Conflict("CPF já cadastrado.");
            }

            string passwordHash = PasswordHelper.HashPassword(registerModel.Password);

            var newUser = new UserModel
            {
                Name = registerModel.Name,
                Email = registerModel.Email, 
                Cpf = registerModel.Cpf,
                ContactNumber = registerModel.ContactNumber, 
                Password = passwordHash,
                Role = registerModel.Role
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok("Usuário registrado com sucesso.");
        }
    }
}
