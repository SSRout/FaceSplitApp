using MassTransit;
using MessagingQueue.Events;
using OrdersApi.Persistence;
using System;
using OrdersApi.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using OrdersAPI.Hubs;

namespace OrdersAPI.Messages.Consumers
{
    public class OrderDispatchedEventConsumer : IConsumer<IOrderDispatchedEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IHubContext<OrderHub> _hubContext;

        public OrderDispatchedEventConsumer(IOrderRepository orderRepository,IHubContext<OrderHub> hubContext)
        {
            _orderRepository = orderRepository;
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<IOrderDispatchedEvent> context)
        {
            var message = context.Message;
            Guid orderId = message.OrderId;
            UpdateDatabase(orderId);
            await _hubContext.Clients.All.SendAsync("UpdatedOrders", new object[] {"Order Dispatched",orderId });
        }

        private void UpdateDatabase(Guid orderId)
        {
            var order = _orderRepository.GetOrder(orderId);
            if (order != null)
            {
                order.Status = Status.Sent;
                _orderRepository.UpdateOrder(order);


            }
        }
    }
}
