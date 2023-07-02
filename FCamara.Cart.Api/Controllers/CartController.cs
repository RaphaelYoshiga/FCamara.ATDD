using FCamara.Cart.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FCamara.Cart.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CartController : ControllerBase
{
    private readonly ICartRequestMapper _requestMapper;
    private readonly IProductCatalogue _catalogue;
    private readonly ICartResponseMapper _responseMapper;

    public CartController(ICartRequestMapper requestMapper,
        IProductCatalogue catalogue,
        ICartResponseMapper responseMapper)
    {
        _requestMapper = requestMapper;
        _catalogue = catalogue;
        _responseMapper = responseMapper;
    }

    [HttpPost(Name = "CalculateCart")]
    public IActionResult Calculate(CalculateCartRequest request)
    {
        var cart = _requestMapper.Map(request);
        var cartResponse = _responseMapper.Map(cart.Calculate(_catalogue));
        return Ok(cartResponse);
    }
}