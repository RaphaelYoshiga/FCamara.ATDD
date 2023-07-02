namespace FCamara.Cart.Domain;

public interface ICart
{
    Task<ICalculatedCart> Calculate(IProductCatalogue productCatalogue);
    IEnumerable<CartItem> Items { get; }
}