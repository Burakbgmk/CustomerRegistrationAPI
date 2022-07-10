namespace CustomerRegistration.Core.Entities
{
    public class CommercialActivity : BaseEntity
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

    }
}
