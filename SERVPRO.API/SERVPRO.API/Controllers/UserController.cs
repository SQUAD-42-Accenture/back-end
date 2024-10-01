using Microsoft.AspNetCore.Mvc;
using SERVPRO.API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SERVPRO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<UserModel> Get()
        {
            List<UserModel> userModels = new List<UserModel>();
            userModels.Add(new UserModel() { Id = 1, Name = "Ellen Peixoto", Email = "ellen_SERVPRO@gmail.com" });
            return userModels;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public UserModel Get(int id)
        {
           UserModel user = new UserModel() { Id = 1, Name = "Ellen Peixoto", Email = "ellen_SERVPRO@gmail.com" };
            return user;
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] UserModel user)
        {
        }

        // POST api/<LoginController>
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginModel loginModel)
        {
            var users = new List<UserLoginModel>
    {
        new UserLoginModel { Username = "AdmSERVPRO", Password = "Senha@123" }
    };
            var user = users.SingleOrDefault(u => u.Username == loginModel.Username && u.Password == loginModel.Password);
            if (user == null)
            {
                return Unauthorized("Usuário ou senha inválidos.");
            }
            return Ok("Login realizado com sucesso.");
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UserModel user)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
