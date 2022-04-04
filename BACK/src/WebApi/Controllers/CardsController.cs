using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Cards.Commands;
using Application.Cards.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("cards")]
    public class CardsController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody]Create.Command command)
        {
            return await Mediator.Send(command);
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

        [HttpPut]
        public async Task<ActionResult<Unit>> Update([FromBody]Update.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            return await Mediator.Send(new Delete.Command { Id = id });
        }
    }
}
