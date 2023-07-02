namespace FCamara.Cart.Domain;

public interface ICart
{
    ICalculatedCart Calculate(IProductCatalogue productCatalogue);
}