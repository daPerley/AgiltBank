namespace AgiltBank.Library.Models
{
    public class Account
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
