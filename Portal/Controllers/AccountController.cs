using System.Security.Claims;
using Core.Domain;
using Core.DomainServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Models;

namespace Portal.Controllers;

public class AccountController : Controller
{
    private readonly IUserRepository _repository; 
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IUserRepository repository)
    {
        _repository = repository;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (_signInManager.IsSignedIn(new ClaimsPrincipal())) {
            return RedirectToAction("Index", "Home"); 
        }
        
        return View(new LoginViewModel()); 
    }
    
    [HttpPost]
    public async Task<IActionResult> LoginAsync(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        
        var user =  await _userManager.FindByEmailAsync(loginViewModel.Email);
        if (user == null)
        {
            ModelState.AddModelError("", "Invalide emailadres of wachtwoord!");
            return View();
        }

        var signinResult = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, true, false);

        if (!signinResult.Succeeded)
        {
            ModelState.AddModelError("", "Invalide emailadres of wachtwoord!");
            return View();
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register()
    {
        if (_signInManager.IsSignedIn(new ClaimsPrincipal())) {
            return RedirectToAction("Index", "Home"); 
        }
        
        var choices = new[] {
            new { Answer = Gender.Male, Description = "Man" },
            new { Answer = Gender.Female, Description = "Vrouw" },
            new { Answer = Gender.Unknown, Description = "Ik zeg het liever niet" }
        };
        ViewBag.GenderChoices = new SelectList(choices, "Answer", "Description");
        
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
            ModelState.AddModelError("", "Gebruikersnaam is al in gebruik!");
            return View();
        }

        var user = new User
        {
            Email = registerViewModel.Email, Gender = registerViewModel.Gender, Name = registerViewModel.Username,
            BirthDate = registerViewModel.BirthDate
        };
        _repository.AddUser(user);

        var identityUser = new IdentityUser(registerViewModel.Username)
        {
            Email = registerViewModel.Email
        };
        var registerUserResult = await _userManager.CreateAsync(identityUser, registerViewModel.Password);
        
        if (!registerUserResult.Succeeded)
        {
            foreach (var error in registerUserResult.Errors) {
                ModelState.AddModelError("", error.Description);
            }
            return View();
        }
        
        return RedirectToAction(nameof(Login));
    }

    public IActionResult Logout()
    {
        _signInManager.SignOutAsync();
        
        return RedirectToAction("Index", "Home"); 
    }
}