using FCamara.Cart.Domain;

namespace FCamara.Cart.Api;

public interface ICartResponseMapper
{
    CartResponse Map(ICalculatedCart calculatedCart);
}

public class CartResponseMapper : ICartResponseMapper
{
    public CartResponse Map(ICalculatedCart calculatedCart)
    {
        return new CartResponse()
        {
            TotalPrice = calculatedCart.TotalPrice,
            Items = calculatedCart.Items.Select(x=> new CartItemResponse()
            {
                Description = x.Description,
                Name = x.Name,
                Quantity = x.Quantity,
                TotalPrice = x.TotalPrice,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice,

            }).ToList()
        };
    }
}