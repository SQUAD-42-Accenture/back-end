using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SERVPRO.Data;
using SERVPRO.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SERVPRO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly ServproDBContext _dbContext;

        public ContaController (ServproDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Login([FromBody] Login login)
        {

            var usuario = _dbContext.Clientes.FirstOrDefault(x => x.ClienteCPF == login.login && x.Senha == login.senha);

            if (usuario != null && usuario.Senha == login.senha)
            {
                var token = GerarTokenJWT(usuario);
                return Ok(new { token });
            }
            /*if(login.login == "admin" && login.senha == "admin")
            {
                var token = GerarTokenJWT();
                return Ok(new { token });
            }*/

            return BadRequest(new { mensagem = "Credenciais inválidades."});
        }

        private string GerarTokenJWT(Cliente cliente)
        {
            string chaveSecreta = "3c728fbf-7290-4087-b180-7fead6e5bbe6";

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));
            var credencial = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("nome", cliente.Nome),
                new Claim("email", cliente.Email),
                new Claim("cpf", cliente.ClienteCPF)

            };

            var token = new JwtSecurityToken(
                issuer: "sua_empresa",
                audience: "sua_aplicacao",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credencial                
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
 