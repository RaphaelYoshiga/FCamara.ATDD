using FCamara.Cart.Domain;

namespace FCamara.Cart.Api;

public interface ICartRequestMapper
{
    ICart Map(CalculateCartRequest request);
}