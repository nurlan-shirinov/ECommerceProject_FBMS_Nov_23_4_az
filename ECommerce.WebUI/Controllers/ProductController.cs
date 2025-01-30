﻿using ECommerce.Application.Abstract;
using ECommerce.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers;

public class ProductController(IProductService productService, ICategoryService categoryService) : Controller
{
    private readonly IProductService _productService = productService;
    private readonly ICategoryService _categoryService = categoryService;

    public IActionResult Index(int page = 1, int categoryId = 0)
    {
        int pageSize = 10;
        var products = _productService.GetAllByCategory(categoryId);
        var paginatedProducts = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        var model = new ProductListViewModel
        {
            Products = paginatedProducts,
            CurrentCategory = categoryId,
            PageCount = (int)Math.Ceiling(products.Count / (double)pageSize),
            CurrentPage = page,
        };
        return View(model);
    }

    [HttpGet]
    public IActionResult Add()
    {
        var model = new ProductAddViewModel();
        model.Product = new Domain.Entities.Product();
        model.Categories = _categoryService.GetAll();
        return View(model);

    }

    [HttpPost]
    public IActionResult Add(ProductAddViewModel model)
    {
        _productService.Add(model.Product);
        return RedirectToAction("index");
    }
}