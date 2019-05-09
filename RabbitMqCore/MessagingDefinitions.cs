using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMqCore
{
    public class MessagingDefinitions
    {
        public const string QueueName = "Worker_Queue";
        public const string ExchangeName = "Worker_Exchange";
        public const string ExchangeType = "topic";
        public const string RoutingKey = "message.*";
    }
}
