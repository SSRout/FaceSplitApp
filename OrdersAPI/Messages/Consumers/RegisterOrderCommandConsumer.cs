using MassTransit;
using MessagingQueue.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersAPI.Messages.Consumers
{
    public class RegisterOrderCommandConsumer : IConsumer<IRegisterOrderCommand>
    {
        public Task Consume(ConsumeContext<IRegisterOrderCommand> context)
        {
            throw new NotImplementedException();
        }
    }
}
