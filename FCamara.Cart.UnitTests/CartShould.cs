using FCamara.Cart.Domain;
using FluentAssertions;
using Moq;

namespace FCamara.Cart.UnitTests
{
    public class CartShould
    {
        private readonly Mock<IProductCatalogue> _productCatalogueMock = new();

        public CartShould()
        {
        }

        [Fact]
        public void Calculate()
        {
            var productId = Guid.Parse("15a12180-958f-46cc-a8a1-e95eb82839d3");
            var items = new List<CartItem>()
            {
                new(productId, quantity: 2)
            };
            var cart = new Cart(items);
            _productCatalogueMock.Setup(x => x.GetProduct(productId))
                .ReturnsAsync(new Product()
                {
                    Id = productId,
                    Price = 5
                });

            var calculatedCart = cart.Calculate(_productCatalogueMock.Object);

            calculatedCart.TotalPrice.Should().Be(10);
        }
    }

    public class CartItem
    {
        public CartItem(Guid productId, int quantity)
        {
        }
    }

    public class Cart
    {
        public Cart(IEnumerable<CartItem> cartItems)
        {
            Items = cartItems.ToList();
        }

        public IEnumerable<CartItem> Items { get; }

        public CalculatedCart Calculate(IProductCatalogue productCatalogue)
        {
            return new CalculatedCart(10);
        }
    }

    public class CalculatedCart
    {
        public CalculatedCart(decimal totalPrice)
        {
            TotalPrice = totalPrice;
        }

        public decimal TotalPrice { get; }
    }
}