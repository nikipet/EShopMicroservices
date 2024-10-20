namespace Basket.API.Models
{
    public class ShoppingCart
    {
        public string Username { get; set; } = default!;
        public List<ShoppingCartItem> Items { get; set; } = [];
        public decimal Total => Items.Sum(x => x.Price * x.Quantity);

        public ShoppingCart(string userName)
        {
            Username = userName;
        }
        
        // Needed for mapping
        public ShoppingCart()
        {
            
        }
    }
}
