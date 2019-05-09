using RabbitMqCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMqService
{
    public class AccountHandler
    {
        private readonly Account _account;
        private readonly Connection _connection;
        public AccountHandler(Account account)
        {
            _account = account;
            _connection = new Connection();
            _connection.CreateConnection();
        }

        public double GetBalance()
        {
            return _account.Balance;
        }

        public void Withdraw(double amount)
        {
            if(amount <= 0)
            {
                return;
            }
            _account.Balance -= amount;
            _connection.SendMessage("Account is debited and Updated balance is :" + _account.Balance);
        }

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                return;
            }
            _account.Balance += amount;
            _connection.SendMessage("Account is credited and Updated balance is :" + _account.Balance);
        }
    }
}
