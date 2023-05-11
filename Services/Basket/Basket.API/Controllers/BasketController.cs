using Basket.Application.Commands;
using Basket.Application.Queries;
using Basket.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    public class BasketController : BaseController
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("[action]/{userName}", Name = "GetBasketByUserName")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> GetBasket(string userName)
        {
            var query = new GetBasketByUserName(userName);
            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        [Route("CreateBasket")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<ShoppingCartResponse>> CreateBasket([FromBody] CreateBasketByUserNameCommand command)
        {
            var basket = await _mediator.Send(command);
            return CreatedAtRoute("GetBasketByUserName", new { userName = basket.UserName }, basket);
        }

        [HttpDelete]
        [Route("[action]/{userName}", Name = "DeleteBasketByUserName")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            var deleteCommand = new DeleteBasketByUserNameCommand(userName);
            await _mediator.Send(deleteCommand);
            return NoContent();
        }
    }
}
