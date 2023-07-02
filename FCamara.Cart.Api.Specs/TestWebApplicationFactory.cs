using Microsoft.AspNetCore.Mvc.Testing;

namespace FCamara.Cart.Api.Specs
{
    public class CustomWebApplicationFactory<TProgram>
        : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
            });

            builder.UseEnvironment("Development");
        }

    }
}
