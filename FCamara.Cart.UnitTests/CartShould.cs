using FCamara.Cart.Domain;
using FluentAssertions;
using Moq;

namespace FCamara.Cart.UnitTests
{
    public class CartShould
    {
        private readonly Mock<IProductCatalogue> _productCatalogueMock = new();
        private readonly Guid _productOne = Guid.Parse("15a12180-958f-46cc-a8a1-e95eb82839d3");
        private readonly Guid _productTwo = Guid.Parse("6f6e4459-e1e7-44e6-8f23-983941939697");

        public CartShould()
        {
            var products = new List<Product>()
            {
                new()
                {
                    Id = _productOne,
                    Price = 5
                },
                new()
                {
                    Id = _productTwo,
                    Price = 3.41m
                }
            };
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
        public async Task CalculateProductOne(int quantiy, decimal totalPrice)
        {
            var items = new List<CartItem>
            {
                new(_productOne, quantity: quantiy)
            };
            var cart = new Cart(items);

            var calculatedCart = await cart.Calculate(_productCatalogueMock.Object);

            calculatedCart.TotalPrice.Should().Be(totalPrice);
        }

        [Theory]
        [InlineData(1, 3.41)]
        [InlineData(2, 6.82)]
        [InlineData(3, 10.23)]
        public async Task CalculateProductTwo(int quantiy, decimal totalPrice)
        {
            var items = new List<CartItem>
            {
                new(_productTwo, quantity: quantiy)
            };
            var cart = new Cart(items);

            var calculatedCart = await cart.Calculate(_productCatalogueMock.Object);

            calculatedCart.TotalPrice.Should().Be(totalPrice);
        }
    }

    public class CartItem
    {
        public int Quantity { get; }
        public Guid ProductId { get; set; }

        public CartItem(Guid productId, int quantity)
        {
            ProductId = productId;
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

        public async Task<CalculatedCart> Calculate(IProductCatalogue productCatalogue)
        {
            var totalPrice = 0m;
            foreach (var item in Items)
            {
                var product = await productCatalogue.GetProduct(item.ProductId);
                totalPrice += item.Quantity * product.Price;
            }
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