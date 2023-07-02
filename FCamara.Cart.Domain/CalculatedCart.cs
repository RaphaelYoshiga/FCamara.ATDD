namespace FCamara.Cart.Domain;

public class CalculatedCart
{
    public CalculatedCart(IEnumerable<CalculatedCartItem> items)
    {
        Items = items.ToList();
    }

    public decimal TotalPrice => Items.Sum(x => x.TotalPrice);
    public IEnumerable<CalculatedCartItem> Items { get; }
}