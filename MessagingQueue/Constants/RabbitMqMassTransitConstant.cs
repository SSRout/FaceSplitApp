using System;

namespace MessagingQueue
{
    public class RabbitMqMassTransitConstant
    {
        public const string RabbitMqUri = "rabbitmq://rabbitmq/";
        public const string UserName = "guest";
        public const string Password = "guest";
        public const string RegisterOrderCommandQueue = "register.order.command";
    }
}
