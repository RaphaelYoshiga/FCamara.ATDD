using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCamara.Cart.Api;
using FCamara.Cart.Domain;
using FluentAssertions;

namespace FCamara.Cart.UnitTests
{
    public class CartResponseMapperShould
    {
        [Fact]
        public void MapResponse()
        {
            var mapper = new CartResponseMapper();
            var productId = Guid.NewGuid();

            var cartResponse = mapper.Map(new CalculatedCart(new List<CalculatedCartItem>()
            {
                new()
                {
                    Quantity = 1,
                    Description = "Test description",
                    TotalPrice = 5,
                    Name = "Test",
                    ProductId = productId,
                    UnitPrice = 5
                }
            }));

            cartResponse.Items.Should().BeEquivalentTo(new List<CartItemResponse>
            {
                new CartItemResponse()
                {
                    Quantity = 1,
                    Description = "Test description",
                    TotalPrice = 5,
                    Name = "Test",
                    ProductId = productId,
                    UnitPrice = 5
                }
            });
            cartResponse.TotalPrice.Should().Be(5);
        }


    }
}
