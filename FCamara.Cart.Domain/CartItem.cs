namespace FCamara.Cart.Domain;

public class CartItem
{
    public int Quantity { get; }
    public Guid ProductId { get; set; }

    public CartItem(Guid productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }
}