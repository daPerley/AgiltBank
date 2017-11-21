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
    }
}
