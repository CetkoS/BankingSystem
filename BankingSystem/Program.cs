using BankingSystem.Business;
using BankingSystem.Models;

namespace BankingSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var random = new Random();

            Transaction[] transactionIds = Enumerable.Range(1, 1000000) // Simulate big number of transactions 1 000 000
                .Select(a => (long)random.Next(5000000)).Order() // Simulate duplicates, by random choosing values between 0-500 000 and order them
                .Select(a => new Transaction(a))
                .ToArray();

            var processor = new TransactionProcessor();
            await processor.ProcessTransactionsAsync(transactionIds);
        }
    }
}