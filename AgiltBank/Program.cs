using System;
using System.IO;
using AgiltBank.Library.Data;
using AgiltBank.Library.Models;

namespace AgiltBank
{
    class Program
    {
        static void Main(string[] args)
        {
            var bankFileService = new BankFileService();
            var path = Path.Combine(Environment.CurrentDirectory, "SeedData/bankdata-small.txt");
            var bank = bankFileService.ReadBankDataFromFile(path, "SuperBank");
        }
    }
}
