using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Shop.Models;
using Shop.Repositories;
using Shop.Services;

namespace Shop.Controllers
{
    [Route("v1/account")]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody]User model)
        {

            var user = UserRepository.Get(model.Username, model.Password);

            if(user == null)
            {
                return NotFound(new { Message = "usuario não encontrado"});
            }            

            var token = TokenService.GenerateToken(user);
            user.Password = "";

            return await Task.FromResult(new {
                user = user,
                token = token
            });    
        }


        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous] 
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => $"Autenticado - {User.Identity.Name}";
        
        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "manager,employee")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("manager")]
        [Authorize]
        public string Manager() => "Gerente";
    }
}