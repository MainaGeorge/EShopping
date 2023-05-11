namespace Discount.Core.Entities
{
    public class Coupon
    {
        public decimal Amount { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public string ProductName { get; set; } 
    }
}
