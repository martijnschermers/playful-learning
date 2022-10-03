using Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portal.Models;
using SqlServer.Infrastructure;

namespace Portal.Controllers;

public class AccountController : Controller
{
    private readonly DomainDbContext _context; 
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    [HttpGet]
    public IActionResult Login()
    {
        return View(new LoginViewModel()); 
    }
    
    [HttpPost]
    public IActionResult Login(LoginViewModel loginViewModel)
    {
        Console.WriteLine(loginViewModel.Email);
        Console.WriteLine(loginViewModel.Password);
        
        return View(); 
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View(new RegisterViewModel()); 
    }
    
    [HttpPost]
    public async Task<IActionResult> RegisterAsync(RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid) {
            return View(); 
        }
        
        if (await _userManager.FindByNameAsync(registerViewModel.Username) != null)
        {
            ModelState.AddModelError("", "User with this username already exists!");
            return View();
        }

        var user = new User
        {
            Email = registerViewModel.Email, Gender = registerViewModel.Gender, Name = registerViewModel.Username,
            BirthDate = registerViewModel.BirthDate
        };

        var identityUser = new IdentityUser(registerViewModel.Username);
        var registerUserResult = await _userManager.CreateAsync(identityUser, registerViewModel.Password);
        
        
        return View(); 
    }
}