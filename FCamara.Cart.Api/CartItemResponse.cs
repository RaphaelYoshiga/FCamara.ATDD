namespace FCamara.Cart.Api;

public class CartItemResponse
{
    public Guid ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
}