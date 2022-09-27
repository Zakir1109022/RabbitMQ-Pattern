using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMQ.Application.Commands;
using RabbitMQ.Application.Queries;
using RabbitMQ.Application.Responses;
using RabbitMQ.Common.Common;
using RabbitMQ.Common.Events;
using RabbitMQ.Common.Producer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RabbitMQ.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;
        private readonly EventBusRabbitMQProducer _eventBus;
        private readonly ILogger<OrderController> _logger;
        private readonly IMapper _mapper;
        public OrderController(IMediator mediator, EventBusRabbitMQProducer eventBus, IMapper mapper, ILogger<OrderController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllOrderQuery();
            var orderList = await _mediator.Send(query);
            return Ok(orderList);
        }

        [HttpGet]
        [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderResponse>> GetOrderById(string Id)
        {
            var query = new GetOrderByIdQuery(Id);
            var order = await _mediator.Send(query);
            return Ok(order);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CheckoutOrder([FromBody] CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);

            var eventMessage = _mapper.Map<OrderCheckoutEvent>(command);
            eventMessage.RequestId = Guid.NewGuid();
            eventMessage.UserName = command.UserName;
            eventMessage.EmailAddress = command.EmailAddress;

            try
            {
                _eventBus.PublishOrderCheckout(EventBusConstants.OrderCheckoutQueue, eventMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {EventId} from {AppName}", eventMessage.RequestId, "OrderCheckOut");
                throw;
            }

            return Ok(result);
        }
    }
}
