using System.Threading.Tasks;
using Application.Auth.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("login")]
    public class AuthController : BaseController
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Application.Auth.Dtos.Login>> Login([FromBody]Login.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}
