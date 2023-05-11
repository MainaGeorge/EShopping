namespace Basket.Application.Response
{
    public class ShoppingCartResponse
    {
        public string UserName { get; set; }
        public List<ShoppingCartItemResponse> Items { get; set; }
        public decimal TotalPrice => Items?.Sum(c => c.Quantity * c.Price) ?? 0M;

        public ShoppingCartResponse(string userName)
        {
            UserName = userName;
        }

        public ShoppingCartResponse()
        {
        }
    }
}
