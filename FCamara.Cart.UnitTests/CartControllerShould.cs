using FCamara.Cart.Api;
using FCamara.Cart.Api.Controllers;
using FCamara.Cart.Domain;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FCamara.Cart.UnitTests
{
    public class CartControllerShould
    {
        private readonly CartController _controller;
        private readonly Mock<ICartRequestMapper> _requestMapper = new();
        private readonly Mock<ICartResponseMapper> _responseMapper = new();
        private readonly Mock<IProductCatalogue> _catalogueMock = new();

        public CartControllerShould()
        {
            _controller = new CartController(_requestMapper.Object, _catalogueMock.Object, _responseMapper.Object);
        }

        [Fact]
        public async Task CalculateCart()
        {
            var request = new CalculateCartRequest();
            var cartMock = new Mock<ICart>();
            _requestMapper.Setup(x => x.Map(request)).Returns(cartMock.Object);
            var calculatedCart = new Mock<ICalculatedCart>().Object;
            cartMock.Setup(x => x.Calculate(_catalogueMock.Object))
                .ReturnsAsync(calculatedCart);
            var response = new CartResponse();
            _responseMapper.Setup(x => x.Map(calculatedCart))
                .Returns(response);
            
            var actionResult = await _controller.Calculate(request);

            actionResult.Should().BeOfType<OkObjectResult>();
            var okObjectResult = ((OkObjectResult)actionResult);
            okObjectResult.Value.Should().Be(response);
        }

        [Fact]
        public async Task HandleUnkownProducts()
        {
            var request = new CalculateCartRequest();
            var cartMock = new Mock<ICart>();
            _requestMapper.Setup(x => x.Map(request)).Returns(cartMock.Object);
            var calculatedCart = new Mock<ICalculatedCart>().Object;
            cartMock.Setup(x => x.Calculate(_catalogueMock.Object))
                .ThrowsAsync(new UnknownProductException());
            
            var actionResult = await _controller.Calculate(request);

            actionResult.Should().BeOfType<BadRequestResult>();
        }
    }
}
