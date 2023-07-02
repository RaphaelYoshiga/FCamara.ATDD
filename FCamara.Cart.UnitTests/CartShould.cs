using System.Formats.Asn1;
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
                    Price = 5,
                    Name="Watch",
                    Description = "Nice Watch"
                },
                new()
                {
                    Id = _productTwo,
                    Price = 3.41m,
                    Name = "Pen",
                    Description = "Blue Pen"
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
            var cart = new Domain.Cart(items);

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
            var cart = new Domain.Cart(items);

            var calculatedCart = await cart.Calculate(_productCatalogueMock.Object);

            calculatedCart.TotalPrice.Should().Be(totalPrice);
        }

        [Fact]
        public async Task ThrowForUnkwonProductId()
        {
            var items = new List<CartItem>
            {
                new(Guid.NewGuid(), quantity: 1)
            };
            var cart = new Domain.Cart(items);

            await Assert.ThrowsAsync<UnknownProductException>(async () => await cart.Calculate(_productCatalogueMock.Object));
        }

        [Fact]
        public async Task EnhanceItemProperties()
        {
            var items = new List<CartItem>
            {
                new(_productOne, quantity: 2),
                new(_productTwo, quantity: 3)
            };
            var cart = new Domain.Cart(items);

            var calculatedCart = await cart.Calculate(_productCatalogueMock.Object);

            calculatedCart.TotalPrice.Should().Be(20.23m);
            calculatedCart.Items.Should().BeEquivalentTo(new List<CalculatedCartItem>()
            {
                new()
                {
                    ProductId = _productOne,
                    Quantity = 2,
                    UnitPrice = 5,
                    TotalPrice = 10,
                    Name = "Watch",
                    Description = "Nice Watch"
                },
                new()
                {
                    ProductId = _productTwo,
                    Quantity = 3,
                    UnitPrice = 3.41m,
                    TotalPrice = 10.23m,
                    Name = "Pen",
                    Description = "Blue Pen"
                }
            });
        }
    }
}