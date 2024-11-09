namespace BankingSystem.Models
{
    public class Transaction
    {
        public long TransactionId { get; set; }
        
        private static readonly Random _random = new();
        private readonly int _delayInMiliseconds;

        public Transaction(long id)
        {
            TransactionId = id;
            _delayInMiliseconds = _random.Next(100, 5000);
        }

        public async Task ProcessAsync()
        {
            Console.WriteLine($"Transaction {TransactionId} starting.");
            await Task.Delay(_delayInMiliseconds);
            Console.WriteLine($"Transaction {TransactionId} finishing.");
        }

        // Those overrided methods are needed for ConcurentSet - (Behind the scene C# uses them to compare keys in ConcurrentDictionary)
        public override bool Equals(object? obj)
        {
            if(obj is null)
            {
                return false;
            }

            return (obj as Transaction)?.TransactionId == this.TransactionId;
        }

        public override int GetHashCode()
        {
            return TransactionId.GetHashCode();
        }
    }
}
