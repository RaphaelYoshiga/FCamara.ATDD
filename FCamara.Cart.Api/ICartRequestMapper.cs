using FCamara.Cart.Domain;

namespace FCamara.Cart.Api;

public interface ICartRequestMapper
{
    ICart Map(CalculateCartRequest request);
}

public class CartRequestMapper : ICartRequestMapper
{
    public ICart Map(CalculateCartRequest request)
    {
        return new Domain.Cart(request.Items.Select(x => new CartItem(x.ProductId, x.Quantity)));
    }
}