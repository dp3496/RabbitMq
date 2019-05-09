using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMqCore
{
    public class Connection
    {
        private static ConnectionFactory factory;
        private static IConnection connection;
        private static IModel channel;

        public Connection()
        {
            Receive();
        }

        public void CreateConnection()
        {
            factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();

            channel.ExchangeDeclare(MessagingDefinitions.ExchangeName, MessagingDefinitions.ExchangeType);
            channel.QueueDeclare(MessagingDefinitions.QueueName, true, false, false, null);
            channel.QueueBind(MessagingDefinitions.QueueName, MessagingDefinitions.ExchangeName, MessagingDefinitions.RoutingKey);
        }

        public void SendMessage(string msg)
        {
            channel.BasicPublish(MessagingDefinitions.ExchangeName, MessagingDefinitions.RoutingKey, null, Encoding.ASCII.GetBytes(msg));
        }

        public delegate void subs(string message);
        public event subs routedEvent;

        public void Receive()
        {
            CreateConnection();
            var factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            try
            {
                channel.BasicQos(0, 10, false);
                var subscription = new Subscription(channel, MessagingDefinitions.QueueName, false);
                Task.Run(
                    () =>
                    {
                        while (true)
                        {
                            var deliverEventArgs = subscription.Next();
                            if (routedEvent != null)
                            {
                                var message = deliverEventArgs.Body;
                                subscription.Ack(deliverEventArgs);
                                routedEvent(Encoding.ASCII.GetString(message));
                            }
                        }
                    }
                );
            }
            catch (Exception)
            {

            }
        }
    }
}
