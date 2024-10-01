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
    public class UserController : ControllerBase
    {
        private readonly WebDbContext _context;

        public UserController(WebDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> Get()
        {
            return await _context.Users.ToListAsync();
        }

        // POST api/User
        [HttpPost]
        public async Task<ActionResult<UserModel>> Post([FromBody] UserModel user)
        {
            if (user == null)
            {
                return BadRequest("Usuário inválido.");
            }

            // Hash da senha
            user.Password = PasswordHelper.HashPassword(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }
    }
}
