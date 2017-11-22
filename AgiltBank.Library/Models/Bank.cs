using System.Collections.Generic;
using System.Linq;

namespace AgiltBank.Library.Models
{
    public class Bank
    {
        public IList<Customer> Customers { get; }
        public IList<Account> Accounts { get; }
        public string Name { get; }

        public Bank(IEnumerable<Customer> customers, IEnumerable<Account> accounts, string name)
        {
            Customers = customers.ToList();
            Accounts = accounts.ToList();
            Name = name;
        }

        public bool AddCustomer(Customer customer)
        {
            try
            {
                Customers.Add(customer);
            }
            catch (System.Exception)
            {

                return false;
            }

            return true;
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

        public bool OpenAccount(int customerId)
        {
            try
            {
                Accounts.Add(new Account
                {
                    Id = ++Accounts.Last().Id,
                    CustomerId = customerId,
                    Balance = 0
                });
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
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

        public bool DepositToAccount(int accountId, decimal amount)
        {
            var account = Accounts.FirstOrDefault(c => c.Id == accountId);

            if (account == null)
                return false;

            var isSuccess = account.Deposit(amount);

            if (!isSuccess)
                return false;

            return true;
        }

        public bool WithdrawalFromAccount(int accountId, decimal amount)
        {
            var account = Accounts.FirstOrDefault(c => c.Id == accountId);

            if (account == null)
                return false;

            var isSuccess = account.Withdrawal(amount);

            if (!isSuccess)
                return false;

            return true;
        }

        public bool TransferBetweenAccounts(int fromAccountId, int toAccountId, decimal amount)
        {
            if (fromAccountId != toAccountId)
            {
                var fromAccount = Accounts.FirstOrDefault(c => c.Id == fromAccountId);
                var toAccount = Accounts.FirstOrDefault(c => c.Id == toAccountId);

                if (fromAccount == null && toAccount == null)
                    return false;

                var fromIsSuccess = fromAccount.Withdrawal(amount);
                var toIsSuccess = toAccount.Deposit(amount);

                if (!fromIsSuccess && !toIsSuccess)
                    return false;
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
