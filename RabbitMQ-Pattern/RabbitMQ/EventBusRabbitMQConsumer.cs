using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Application.Commands;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Common.Common;
using RabbitMQ.Common.Events;
using RabbitMQ.Common.RabbitMQConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.RabbitMQ
{
    public class EventBusRabbitMQConsumer
    {
        private readonly IRabbitMQConnection _connection;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EventBusRabbitMQConsumer(IRabbitMQConnection connection, IMediator mediator, IMapper mapper)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public void Consume()
        {
            var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: EventBusConstants.OrderCheckoutQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            //Create event when something receive
            consumer.Received += ReceivedEvent;

            channel.BasicConsume(queue: EventBusConstants.OrderCheckoutQueue, autoAck: true, consumer: consumer);
        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == EventBusConstants.OrderCheckoutQueue)
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                Console.WriteLine(message);

                // EXECUTION : Call API end point
                var orderCheckoutEvent = JsonConvert.DeserializeObject<OrderCheckoutEvent>(message);
                var command = _mapper.Map<SentEmailCommand>(orderCheckoutEvent);
                await _mediator.Send(command);
            }
        }

        public void Disconnect()
        {
            _connection.Dispose();
        }
    }
}
