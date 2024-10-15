using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SERVPRO.Data;
using SERVPRO.Models;
using StackExchange.Redis;
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

            //var cliente = _dbContext.Clientes.SingleOrDefault(c => c.CPF == login.login && c.Senha == login.senha);
            //var tecnico = _dbContext.Tecnicos.SingleOrDefault(t => t.CPF == login.login && t.Senha == login.senha);
            //var administrador = _dbContext.Administradores.SingleOrDefault(a => a.CPF == login.login && a.Senha == login.senha);

            //Usuario usuario = cliente ?? (Usuario)tecnico ?? administrador;

            //var usuario = _dbContext.Clientes.SingleOrDefault(x => x.CPF == login.login && x.Senha == login.senha);

            var usuario = _dbContext.Set<Usuario>()
                .FirstOrDefault(x => x.CPF == login.login && x.Senha == login.senha);

            if (usuario != null && usuario.Senha == login.senha)
            {
                /*var tipoUsuario = usuario is Cliente ? "Cliente" :
                      usuario is Tecnico ? "Tecnico" :
                      "Administrador";*/
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

        private string GerarTokenJWT(Usuario usuario)
        {
            string chaveSecreta = "3c728fbf-7290-4087-b180-7fead6e5bbe6";

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));
            var credencial = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim("cpf", usuario.CPF),
                new Claim("tipoUsuario", usuario.TipoUsuario.ToString())

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
 