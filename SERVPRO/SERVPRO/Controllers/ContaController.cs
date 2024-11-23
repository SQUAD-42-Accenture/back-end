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

        public ContaController(ServproDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Login([FromBody] Login login)
        {
            // Buscar o usuário de acordo com o CPF na tabela de usuários
            var usuario = _dbContext.Usuarios.SingleOrDefault(c => c.CPF == login.login && c.Senha == login.senha);

            // Se não encontrar, procurar em outras tabelas (Clientes, Tecnicos, Administradores)
            if (usuario == null)
            {
                var cliente = _dbContext.Clientes.SingleOrDefault(c => c.CPF == login.login && c.Senha == login.senha);
                var tecnico = _dbContext.Tecnicos.SingleOrDefault(t => t.CPF == login.login && t.Senha == login.senha);
                var administrador = _dbContext.Administradores.SingleOrDefault(a => a.CPF == login.login && a.Senha == login.senha);

                // Atribuir o primeiro usuário encontrado (Cliente, Tecnico ou Administrador)
                if (cliente != null)
                {
                    usuario = cliente;
                }
                else if (tecnico != null)
                {
                    usuario = tecnico;
                }
                else if (administrador != null)
                {
                    usuario = administrador;
                }
            }

            // Se não encontrar o usuário, retornar erro
            if (usuario == null)
            {
                return BadRequest(new { mensagem = "Credenciais inválidas." });
            }

            // Gerar o token JWT
            var token = GerarTokenJWT(usuario);

            return Ok(new { token });
        }

        /*
        [HttpPost]
        public IActionResult Login([FromBody] Login login)
        {
            var padrao = _dbContext.Usuarios.SingleOrDefault(c => c.CPF == login.login && c.Senha == login.senha);
            var cliente = _dbContext.Clientes.SingleOrDefault(c => c.CPF == login.login && c.Senha == login.senha);
            var tecnico = _dbContext.Tecnicos.SingleOrDefault(t => t.CPF == login.login && t.Senha == login.senha);
            var administrador = _dbContext.Administradores.SingleOrDefault(a => a.CPF == login.login && a.Senha == login.senha);

            Usuario usuario = padrao ?? (Usuario)cliente ?? (Usuario)tecnico ?? administrador;

            if (usuario != null)
            {

                var token = GerarTokenJWT(usuario);
                return Ok(new { token });
            }

            return BadRequest(new { mensagem = "Credenciais inválidades." });
        }*/

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
