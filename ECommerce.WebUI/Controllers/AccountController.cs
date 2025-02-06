using ECommerce.WebUI.Entities;
using ECommerce.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers;

public class AccountController(
    UserManager<CustomIdentityUser> userManager,
    RoleManager<CustomIdentityRole> roleManager,
    SignInManager<CustomIdentityUser> signInManager) : Controller
{
    private readonly UserManager<CustomIdentityUser> _userManager = userManager;
    private readonly RoleManager<CustomIdentityRole> _roleManager = roleManager;
    private readonly SignInManager<CustomIdentityUser> _signinManager = signInManager;


    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            CustomIdentityUser user = new()
            {
                UserName = model.Username,
                Email = model.Email
            };

            IdentityResult result = _userManager.CreateAsync(user, model.Password).Result;

            if (result.Succeeded)
            {
                if (!_roleManager.RoleExistsAsync("Admin").Result)
                {
                    CustomIdentityRole role = new()
                    {
                        Name = "Admin"
                    };

                    IdentityResult roleResult = _roleManager.CreateAsync(role).Result;

                    if (!roleResult.Succeeded)
                    {
                        ModelState.AddModelError("" , "We cannot create the role");
                        return View(model);
                    }
                }
                _userManager.AddToRoleAsync(user , "Admin"); //wait
                return RedirectToAction("Login" , "Account");
            }
        }
        return View();
    }


    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = _signinManager.PasswordSignInAsync(model.UserName,model.Password , model.RememberMe , false).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Product");
            }
            ModelState.AddModelError("", "Invalid login");
        }
        return View(model);
    }

}