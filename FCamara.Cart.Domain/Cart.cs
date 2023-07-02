namespace FCamara.Cart.Domain;

public class Cart
{
    public Cart(IEnumerable<CartItem> cartItems)
    {
        Items = cartItems.ToList();
    }

    public IEnumerable<CartItem> Items { get; }

    public async Task<CalculatedCart> Calculate(IProductCatalogue productCatalogue)
    {
        var calculatedCartItems = new List<CalculatedCartItem>();

        var totalPrice = 0m;
        foreach (var item in Items)
        {
            var product = await productCatalogue.GetProduct(item.ProductId);
            calculatedCartItems.Add(new CalculatedCartItem()
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = product.Price,
                Description = product.Description,
                TotalPrice = item.Quantity * product.Price,
                Name = product.Name
            });
            totalPrice += item.Quantity * product.Price;
        }

            
        return new CalculatedCart(calculatedCartItems);
    }
}