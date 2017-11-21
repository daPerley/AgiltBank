﻿using AgiltBank.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace AgiltBank.Library.Data
{
    public class BankData
    {
        public IList<Customer> Customers { get; }
        public IList<Account> Accounts { get; }

        public BankData(IEnumerable<Customer> customers, IEnumerable<Account> accounts)
        {
            Customers = customers.ToList();
            Accounts = accounts.ToList();
        }

        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
        }

        public Customer GetCustomer(int id)
        {
            return Customers.FirstOrDefault(c => c.Id == id);
        }

        public bool RemoveAccount(int accountId)
        {
            var account = Accounts.FirstOrDefault(a => a.Id == accountId);
            if (account != null && account.Balance != 0)
                return false;

            return Accounts.Remove(account);
        }
    }
}
