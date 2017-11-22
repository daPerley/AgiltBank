using System;
using System.IO;
using System.Linq;
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

            while (true)
            {
                PrintMenu();

                var readkey = GetParsedReadLine();

                if (readkey == null)
                {
                    Console.WriteLine("Navigera med 1-9");
                    PromptToGoBackToMenu();
                }

                if (readkey == 0)
                    break;

                if (readkey == 1)
                {
                    Console.WriteLine("Kundnummer:");
                    while (true)
                    {
                        Console.WriteLine("Kund id:");
                        var parsedReadLine = GetParsedReadLine();
                        if (parsedReadLine != null)
                        {
                            var customer = bank.GetCustomer((int)parsedReadLine);
                            if (customer != null)
                                ShowCustomerDetails(customer);
                            else
                                Console.WriteLine("Kunden kunde inte hittas");

                            PromptToGoBackToMenu();
                            break;
                        }

                        Console.WriteLine("Endast siffror är tillåtna");
                    }
                }
                else if (readkey == 2)
                {
                    Console.WriteLine("Sök:");
                }
                else if (readkey == 3)
                {
                    Console.WriteLine("Skapa Kund");
                }
                else if (readkey == 4)
                {
                    Console.WriteLine("Ta bort kund");
                }
                else if (readkey == 5)
                {
                    Console.WriteLine("Öppna konto");
                }
                else if (readkey == 6)
                {
                    Console.WriteLine("Tabort konto");
                }
                else if (readkey == 7)
                {
                    Console.WriteLine("Sätt in pengar");
                }
                else if (readkey == 8)
                {
                    Console.WriteLine("Ta ut pengar");
                }
                else if (readkey == 9)
                {
                    Console.WriteLine("överför pengar");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Tack för att du använder agilt-bank.");
        }

        private static int? GetParsedReadLine()
        {
            var sucessfullParse = int.TryParse(Console.ReadLine(), out var parsedResult);

            if (sucessfullParse)
                return parsedResult;

            return null;
        }

        private static void PromptToGoBackToMenu()
        {
            Console.WriteLine($"{Environment.NewLine}Tryck på valfri knapp för att gå tillbaka till menyn");
            Console.ReadLine();
        }

        private static void PrintMenu()
        {
            Console.WriteLine("[1] Visa Kundbild");
            Console.WriteLine("[2] Sök Kund");
            Console.WriteLine("[3] Skapa Kund");
            Console.WriteLine("[4] Ta bort Kund");
            Console.WriteLine("[5] Skapa Konto");
            Console.WriteLine("[6] Ta bort konto");
            Console.WriteLine("[7] Insättning");
            Console.WriteLine("[8] Uttag");
            Console.WriteLine("[9] Överföring");
            Console.WriteLine("[0] Avsluta");
        }

        private static void ShowCustomerDetails(Customer customer)
        {
            if (customer == null)
            {
                Console.WriteLine("Kunden kunde inte hittas");
            }
            else
            {
                Console.WriteLine($"Kundnummer: {customer.Id}");
                Console.WriteLine($"Organisationsnummer: {customer.OrganisationNumber}");
                Console.WriteLine($"Namn: {customer.Name}");
                Console.WriteLine($"Adress: {customer.StreetAddress}, {customer.PostalCode} {customer.City}");
            }
        }
    }
}
