using FCamara.Cart.Domain;

namespace FCamara.Cart.Api;

public interface ICartResponseMapper
{
    CartResponse Map(ICalculatedCart calculatedCart);
}