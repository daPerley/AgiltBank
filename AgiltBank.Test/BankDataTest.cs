using AgiltBank.Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AgiltBank.Test
{
    [TestClass]
    public class BankDataTest
    {
        private Bank _bank;

        [TestInitialize]
        public void Initialize()
        {
            var customers = new List<Customer>
            {
                new Customer
                {
                    Id = 1001,
                    OrganisationNumber = "551234 - 1234",
                    Name = "Tangooe",
                    StreetAddress = "Bankstreet 1",
                    City = "BankCity",
                    PostalCode = "S - 123 45",
                    Country = "Sweden",
                    PhoneNumber = "070 123 45 67"
                },
                new Customer
                {
                    Id = 1005,
                    OrganisationNumber = "234543 - 1234",
                    Name = "BankGuy",
                    StreetAddress = "Bankstreet 3",
                    City = "BankCity",
                    PostalCode = "S - 123 45",
                    Country = "Sweden",
                    PhoneNumber = "070 456 45 78"
                }
            };

            var accounts = new List<Account>
            {
                new Account { Id = 13001, Balance = 0, CustomerId = 1001 },
                new Account { Id = 14001, Balance = 99, CustomerId = 1005 },
                new Account { Id = 14002, Balance = 9999, CustomerId = 1005 }
            };

            _bank = new Bank(customers, accounts, "TestBank");
        }

        #region Customer
        [TestMethod]
        public void CanGetCustomerFromBankData() => Assert.IsNotNull(_bank.GetCustomer(1005));

        [TestMethod]
        public void CanCreateCustomer()
        {
            var customer = new Customer
            {
                Id = 1002,
                OrganisationNumber = "551234 - 6666",
                Name = "Emil Ekman",
                StreetAddress = "EkmanStreet 1",
                City = "EkmanCity",
                PostalCode = "S - 666 66",
                Country = "Sweden",
                PhoneNumber = "070 666 66 66"
            };

            Assert.AreEqual(true, _bank.AddCustomer(customer));
            Assert.IsNotNull(_bank.GetCustomer(1002));
        }
        #endregion
    }
}
