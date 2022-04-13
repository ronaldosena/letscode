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
        public async Task<ActionResult<string>> Login([FromBody]Login.Command command)
        {
            var res=  await Mediator.Send(command);
            return res.Token;
        }
    }
}
