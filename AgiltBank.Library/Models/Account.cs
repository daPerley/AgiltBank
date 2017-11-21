using System;

namespace AgiltBank.Library.Models
{
    public class Account
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public bool Deposit(decimal amount)
        {
            try
            {
                if (amount > 0)
                    Balance += amount;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
