namespace FCamara.Cart.Domain;

public interface ICalculatedCart
{
    decimal TotalPrice { get; }
    IEnumerable<CalculatedCartItem> Items { get; }
}