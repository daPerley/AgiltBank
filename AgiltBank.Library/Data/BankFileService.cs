using AgiltBank.Library.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace AgiltBank.Library.Data
{
    public class BankFileService
    {
        public Bank ReadBankDataFromFile(string path, string name)
        {
            var lines = File.ReadAllLines(path);
            var customers = new List<Customer>();
            var accounts = new List<Account>();

            var numberOfCustomers = int.Parse(lines[0]);

            for (var i = 1; i <= numberOfCustomers; i++)
                customers.Add(ParseToCustomer(lines[i].Split(";")));

            for (var i = numberOfCustomers + 2; i < lines.Length; i++)
                accounts.Add(ParseToAccount(lines[i].Split(";")));

            return new Bank(customers, accounts, name);
        }

        public bool SaveData(Bank bank)
        {
            try
            {
                var lines = new List<string>();

                var numbersOfCustomers = bank.Customers.Count;
                lines.Add(numbersOfCustomers.ToString());

                foreach (var customer in bank.Customers)
                {
                    lines.Add($"{customer.Id};{customer.OrganisationNumber};{customer.Name};{customer.StreetAddress};{customer.City};{customer.State ?? string.Empty};{customer.PostalCode};{customer.Country};{customer.PhoneNumber}");
                }

                var numbersOfAccounts = bank.Accounts.Count;
                lines.Add(numbersOfAccounts.ToString());

                foreach (var account in bank.Accounts)
                {
                    lines.Add($"{account.Id};{account.CustomerId};{account.Balance}");
                }

                var folder = Path.Combine(Environment.CurrentDirectory, "data");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var path = Path.Combine(folder, $"{bank.Name}.txt");

                if (!File.Exists(path))
                    File.Create(path);
                else
                    File.WriteAllLines(path, lines);
            }
            catch (System.Exception e)
            {
                return false;
            }
            return true;
        }

        private static Account ParseToAccount(IReadOnlyList<string> fields)
        {
            return new Account
            {
                Id = int.Parse(fields[0]),
                CustomerId = int.Parse(fields[1]),
                Balance = decimal.Parse(fields[2], CultureInfo.InvariantCulture)
            };
        }

        private static Customer ParseToCustomer(IReadOnlyList<string> fields)
        {
            return new Customer
            {
                Id = int.Parse(fields[0]),
                OrganisationNumber = fields[1],
                Name = fields[2],
                StreetAddress = fields[3],
                City = fields[4],
                State = fields[5],
                PostalCode = fields[6],
                Country = fields[7],
                PhoneNumber = fields[8]
            };
        }
    }
}
