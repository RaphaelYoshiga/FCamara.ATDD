namespace FCamara.Cart.Domain;

public interface IProductCatalogue
{
    Task<Product> GetProduct(Guid productId);
}