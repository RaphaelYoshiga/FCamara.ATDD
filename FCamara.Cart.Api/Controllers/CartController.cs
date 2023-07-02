using Microsoft.AspNetCore.Mvc;

namespace FCamara.Cart.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CartController : ControllerBase
{

    [HttpPost(Name = "CalculateCart")]
    public IActionResult Calculate(CalculateCartRequest request)
    {
        throw new NotImplementedException();
    }
}