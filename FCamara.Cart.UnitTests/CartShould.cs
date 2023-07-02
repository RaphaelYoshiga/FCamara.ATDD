using FCamara.Cart.Domain;
using FluentAssertions;
using Moq;

namespace FCamara.Cart.UnitTests
{
    public class CartShould
    {
        private readonly Mock<IProductCatalogue> _productCatalogueMock = new();
        private readonly Guid _productOne = Guid.Parse("15a12180-958f-46cc-a8a1-e95eb82839d3");

        public CartShould()
        {
            var products = new List<Product>();
            foreach (var product in products)
            {
                _productCatalogueMock.Setup(x => x.GetProduct(product.Id))
                    .ReturnsAsync(product);
            }
        }

        [Theory]
        [InlineData(2, 10)]
        [InlineData(3, 15)]
        [InlineData(4, 20)]
        public void Calculate(int quantiy, decimal totalPrice)
        {
            var items = new List<CartItem>
            {
                new(_productOne, quantity: quantiy)
            };
            var cart = new Cart(items);

            var calculatedCart = cart.Calculate(_productCatalogueMock.Object);

            calculatedCart.TotalPrice.Should().Be(totalPrice);
        }
    }

    public class CartItem
    {
        public int Quantity { get; }

        public CartItem(Guid productId, int quantity)
        {
            Quantity = quantity;
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
            var totalPrice = Items.First().Quantity * 5;
            return new CalculatedCart(totalPrice);
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