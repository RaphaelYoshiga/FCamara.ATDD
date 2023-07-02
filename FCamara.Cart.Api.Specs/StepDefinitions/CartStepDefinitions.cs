using System;
using System.Net;
using System.Text;
using System.Text.Json;
using FCamara.Cart.Api.Controllers;
using FCamara.Cart.Domain;
using TechTalk.SpecFlow.Assist;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Moq;

namespace FCamara.Cart.Api.Specs.StepDefinitions
{
    [Binding]
    public class CartStepDefinitions : IDisposable
    {
        private readonly WebApplicationFactory<Program> _factory;
        private HttpResponseMessage _response;
        private CartResponse? _cartResponse;
        private Mock<IProductCatalogue> _catalogueMock = new();

        public void Dispose()
        {
            _factory.Dispose();
            _response.Dispose();
        }

        public CartStepDefinitions(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Given(@"I we have this catalogue of products")]
        public void GivenIWeHaveThisCatalogueOfProducts(Table table)
        {
            var productsInStock = table.CreateSet<Product>();
            foreach (var product in productsInStock)
            {
                _catalogueMock.Setup(x => x.GetProduct(product.Id))
                    .ReturnsAsync(product);
            }
        }

        [When(@"we ask for the price of this cart")]
        public async Task WhenWeAskForThePriceOfThisCart(string multilineText)
        {
            var httpClient = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton(_catalogueMock.Object);
                });
            }).CreateClient();

            var content = new StringContent(multilineText, Encoding.UTF8, "application/json");
            _response = await httpClient.PostAsync("/cart/calculate", content);
        }

        [Then(@"the response should be 200 OK")]
        public void ThenTheResponseShouldBeOK()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then(@"the cart response should contain those items")]
        public async Task ThenTheCartResponseShouldContainThoseItems(Table table)
        {
            var responseJson = await _response.Content.ReadAsStringAsync();
            _cartResponse = JsonSerializer.Deserialize<CartResponse>(responseJson, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            var expectedCartItems = table.CreateSet<CartItemResponse>().ToList();
            _cartResponse!.Items.Should().BeEquivalentTo(expectedCartItems);
        }

        [Then(@"the cart total price is (.*)")]
        public void ThenTheCartTotalPriceIs(decimal totalPrice)
        {
            _cartResponse.TotalPrice.Should().Be(totalPrice);
        }
    }
}
