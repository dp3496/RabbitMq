using Moq;
using RabbitMQ.Client;
using RabbitMqCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMqConsumer
{
    class Consumer
    {
        static void Main(string[] args)
        {
            var connection = new Connection();
            connection.routedEvent += Connection_routedEvent;
            Console.ReadLine();
        }
        private static void Connection_routedEvent(string message)
        {
            Console.WriteLine(message);
        }
    }
}
