using Microsoft.AspNetCore.Mvc;
using SERVPRO.API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SERVPRO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<UserModel>> SearchForCpf(string Cpf)
        {
            return Ok();
        }

        [HttpGet]
        public ActionResult<List<UserModel>> SearchAllUser(string Cpf)
        {
            return Ok();
        }

        //[HttpPut]
        //public ActionResult Update(UserModel user, string cpf)
        //{ 
        //}
    }
}
