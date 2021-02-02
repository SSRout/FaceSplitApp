using System;

namespace MessagingQueue
{
    public class RabbitMqMassTransitConstant
    {
        public const string RabbitMqUri = "rabbitmq://rabbitmq:5672";
        public const string UserName = "guest";
        public const string Password = "guest";
        public const string RegisterOrderServiceQueue = "register.order.command";
    }
}
