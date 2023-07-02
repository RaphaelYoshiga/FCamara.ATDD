namespace FCamara.Cart.Api;

public class CartResponse
{
    public List<CartItemResponse> Items { get; set; }
    public decimal TotalPrice { get; set; }
}