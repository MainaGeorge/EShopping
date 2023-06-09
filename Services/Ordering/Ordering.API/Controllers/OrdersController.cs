﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Commands;
using Ordering.Application.Queries;
using Ordering.Application.Response;

namespace Ordering.API.Controllers
{
    public class OrdersController : ApiController
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("all/{userName}", Name = "GetOrdersByUserName")]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrdersByUserName(string userName)
        {
            var orders = await _mediator.Send(new GetOrdersByUserNameQuery(userName));
            return Ok(orders);
        }

        [HttpGet]
        [Route("{orderId:int}")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrderById(int orderId)
        {
            var command = new GetOrderByIdQuery(orderId);
            var order = await _mediator.Send(command);
            if (order == null)
                return NotFound($"order with id {orderId} not found");

            return Ok(order);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<OrderResponse>> CreateOrder([FromBody] CheckoutOrderCommand command)
        {
            var order = await _mediator.Send(command);

            return Ok(order);
        }

        [HttpPut]
        [Route("{orderId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOrder(int orderId, [FromBody] UpdateOrderCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete]
        [Route("{orderId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            await _mediator.Send(new DeleteOrderCommand(orderId));

            return NoContent();
        }
    }
}
