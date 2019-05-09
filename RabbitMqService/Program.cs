using RabbitMQ.Client;
using RabbitMqCore;
using System;
using System.Text;

namespace RabbitMqService
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var accountHandler = new AccountHandler(new Account {Balance = 500 });
            accountHandler.Withdraw(100);
            Console.ReadLine();
        }
    }
}
