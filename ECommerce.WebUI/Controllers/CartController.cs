using ECommerce.Application.Abstract;
using ECommerce.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers;

public class CartController(ICartSessionService sessionService , IProductService productService , ICartService cartService) : Controller
{
    private readonly ICartSessionService _sessionService = sessionService;
    private readonly IProductService _productService = productService;
    private readonly ICartService _cartService = cartService;
    
    public IActionResult AddToCart(int productId)
    {
        var productToBeAdded = _productService.GetById(productId);
        var cart = _sessionService.GetCart();
        _cartService.AddToCart(cart , productToBeAdded);

        _sessionService.SetCart(cart);
        TempData["message"] = string.Format("Your Product {0} , was added succefully to cart" , productToBeAdded.ProductName);
    
        return RedirectToAction("Index","Product");
    }

    public IActionResult List()
    {
        var cart = _sessionService.GetCart();
        var model = new CartListViewModel
        {
            Cart = cart
        };
        return View(model); 
    }

    public IActionResult Remove(int productId)
    {
        var cart = _sessionService.GetCart();
        _cartService.RemoveFromCart(cart , productId);
        _sessionService.SetCart(cart);
        TempData.Add("message", "Your product was removed succefuly from cart" );
        return RedirectToAction("List");
    }
}