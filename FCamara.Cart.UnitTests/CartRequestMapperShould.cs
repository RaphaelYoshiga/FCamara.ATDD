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
    public class CartRequestMapperShould
    {
        [Fact]
        public void MapItems()
        {
            var mapper = new CartRequestMapper();

            var productIdOne = Guid.NewGuid();
            var productIdTwo = Guid.NewGuid();
            var cart = mapper.Map(new CalculateCartRequest()
            {
                Items = new List<CartItemRequest>()
                {
                    new() { ProductId = productIdOne, Quantity = 3 },
                    new() { ProductId = productIdTwo, Quantity = 5 },
                }
            });

            var cartItems = cart.Items.ToList();
            cartItems.Should().BeEquivalentTo(new List<CartItem>()
            {
                new CartItem(productIdOne, 3),
                new CartItem(productIdTwo, 5)
            });
        }
    }
}
