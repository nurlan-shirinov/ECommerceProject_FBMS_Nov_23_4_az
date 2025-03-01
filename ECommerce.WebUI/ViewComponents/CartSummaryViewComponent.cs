﻿using ECommerce.WebUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace ECommerce.WebUI.ViewComponents;

public class CartSummaryViewComponent(ICartSessionService cartSessionService) : ViewComponent
{
    private readonly ICartSessionService _cartSessionService = cartSessionService;

    public ViewViewComponentResult Invoke()
    {
        var model = new CartSummaryViewModel
        {
            Cart = _cartSessionService.GetCart()
        };
        return View(model);
    }

}
