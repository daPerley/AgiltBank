using AgiltBank.Library.Models;
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

        public bool RemoveCustomer(int id)
        {
            var customer = Customers.FirstOrDefault(c => c.Id == id);
            return RemoveRelatedAccounts(id) && Customers.Remove(customer);
        }

        public IList<Customer> SearchCustomers(string query)
        {
            return Customers.Where(c => c.Name.ToLower().Contains(query.ToLower()) || c.PostalCode.Contains(query)).ToList();
        }

        public void OpenAccount(int customerId)
        {
            Accounts.Add(new Account
            {
                Id = ++Accounts.Last().Id,
                CustomerId = customerId,
                Balance = 0
            });
        }

        public bool RemoveAccount(int accountId)
        {
            var account = Accounts.FirstOrDefault(a => a.Id == accountId);
            if (account != null && account.Balance != 0)
                return false;

            return Accounts.Remove(account);
        }

        private bool RemoveRelatedAccounts(int customerId)
        {
            var accountsToRemove = Accounts.Where(a => a.CustomerId == customerId).ToList();

            if (accountsToRemove.Any(a => a.Balance != 0))
                return false;

            return accountsToRemove.All(a => Accounts.Remove(a));
        }
    }
}
