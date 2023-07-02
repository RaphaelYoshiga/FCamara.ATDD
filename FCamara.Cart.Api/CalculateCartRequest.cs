namespace FCamara.Cart.Api;

public class CalculateCartRequest
{
    public List<CartItemRequest> Items { get; set; }
}

public class CartItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}