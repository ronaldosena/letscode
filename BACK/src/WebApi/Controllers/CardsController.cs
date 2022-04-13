using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Cards.Commands;
using Application.Cards.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Middleware;

namespace WebApi.Controllers
{
    [Route("cards")]
    public class CardsController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<Application.Cards.Dtos.CardDto>> Create([FromBody]Create.Command command)
        {
            var res = await Mediator.Send(command);
            return Created("/", res);
        }

        [HttpGet]
        public async Task<IEnumerable<Application.Cards.Dtos.CardDto>> Read([FromQuery]List.ListParams listParams)
        {
            return await Mediator.Send(new List.Query { Params = listParams });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Application.Cards.Dtos.CardDto>> Get(int id)
        {
            return await Mediator.Send(new Details.Query { Id = id });
        }

        [HttpPut("{id}")]
        [RequestLoggingAttribute]
        public async Task<ActionResult<Application.Cards.Dtos.CardDto>> Update(string id, [FromBody]Update.Command command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        [RequestLoggingAttribute]
        public async Task<IEnumerable<Application.Cards.Dtos.CardDto>> Delete(string id)
        {
            return await Mediator.Send(new Delete.Command { Id = id });
        }
    }
}
