using AgiltBank.Library.Models;
using System.Collections.Generic;
using System.Globalization;

namespace AgiltBank.Library.Data
{
    public class BankFileService
    {
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
