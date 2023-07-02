namespace FCamara.Cart.Domain;

public class CalculatedCartItem
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal TotalPrice { get; set; }
}