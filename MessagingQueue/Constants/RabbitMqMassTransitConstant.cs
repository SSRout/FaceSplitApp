﻿using System;

namespace MessagingQueue.Constants
{
    public class RabbitMqMassTransitConstant
    {
        public const string RabbitMqUri = "rabbitmq://rabbitmq/";
        public const string UserName = "guest";
        public const string Password = "guest";
        public const string RegisterOrderCommandQueue = "register.order.command";

        public const string NotificationServiceQueue = "notification.service.queue";
        public const string OrderDispatchedServiceQueue = "order.dispatch.service.queue";
    }
}
