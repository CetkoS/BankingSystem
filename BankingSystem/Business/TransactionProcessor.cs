using BankingSystem.Models;

namespace BankingSystem.Business
{
    public class TransactionProcessor
    {
        private readonly SemaphoreSlim _semaphore;
        private readonly ConcurrentSet<Transaction> _processedTransactions;
        private const int MAX_CONCURENT_TRANSACTIONS = 5; // In order to test semaphore count better - increase this value and add logger for semaphore count

        public TransactionProcessor()
        {
            _semaphore = new SemaphoreSlim(MAX_CONCURENT_TRANSACTIONS);
            _processedTransactions = new ConcurrentSet<Transaction>();
        }

        public async Task ProcessTransactionsAsync(Transaction[] transactions)
        {
            var tasks = new List<Task>();

            foreach (var transaction in transactions)
            {
                if (_processedTransactions.Contains(transaction))
                {
                    Console.WriteLine($"Transaction with ID {transaction.TransactionId} already processed.");
                }

                await _semaphore.WaitAsync();

                _processedTransactions.Add(transaction);

                Task task = RunSingleTransaction(transaction);

                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }

        private Task RunSingleTransaction(Transaction transaction)
        {
            return Task.Run(async () =>
            {
                try
                {
                    await transaction.ProcessAsync();
                }
                catch
                {
                    Console.WriteLine($"Error while processing transaction with Id: {transaction.TransactionId}.");
                }
                finally
                {
                    _semaphore.Release();
                }
            });
        }
    }
}
