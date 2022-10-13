using System.Security.Claims;
using Core.Domain;
using Core.DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Models;

namespace Portal.Controllers;

[AllowAnonymous]
public class AccountController : Controller
{
    private readonly IUserRepository _repository;
    private readonly UserManager<IdentityUser>? _userManager;
    private readonly SignInManager<IdentityUser>? _signInManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
        IUserRepository repository)
    {
        _repository = repository;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl)
    {
        var identity = HttpContext.User.Identity;
        if (identity!.IsAuthenticated) {
            return RedirectToAction("Index", "Home");
        }

        return View(new LoginViewModel
        {
            ReturnUrl = returnUrl ?? "/"
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginAsync(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid) {
            return View();
        }

        var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
        if (user == null) {
            ModelState.AddModelError("", "Invalide emailadres of wachtwoord!");
            return View();
        }

        var signinResult = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, true, false);

        if (!signinResult.Succeeded) {
            ModelState.AddModelError("", "Invalide emailadres of wachtwoord!");
            return View();
        }

        return Redirect(loginViewModel.ReturnUrl);
    }

    [HttpGet]
    public IActionResult Register()
    {
        var identity = HttpContext.User.Identity;
        if (identity!.IsAuthenticated) {
            return RedirectToAction("Index", "Home");
        }

        var genders = new[]
        {
            new { Answer = Gender.Male, Description = "Man" },
            new { Answer = Gender.Female, Description = "Vrouw" },
            new { Answer = Gender.Unknown, Description = "Ik zeg het liever niet" }
        };
        ViewBag.Gender = new SelectList(genders, "Answer", "Description");

        var allergies = new[]
        {
            new { Answer = AllergyEnum.Lactose, Description = "Lactose" },
            new { Answer = AllergyEnum.Nuts, Description = "Noten" },
            new { Answer = AllergyEnum.Soya, Description = "Soja" },
            new { Answer = AllergyEnum.Wheat, Description = "Tarwe" },
            new { Answer = AllergyEnum.Gluten, Description = "Gluten" },
        };
        ViewBag.Allergies = allergies;

        var types = new[]
        {
            new { Answer = UserType.Organizer, Description = "Organisator" },
            new { Answer = UserType.Participant, Description = "Speler" }
        };
        ViewBag.UserTypes = new SelectList(types, "Answer", "Description");

        return View(new RegisterViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> RegisterAsync(RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid) {
            return View();
        }

        if (await _userManager.FindByEmailAsync(registerViewModel.Email) != null) {
            ModelState.AddModelError("", "Emailadres is al in gebruik!");
            return View();
        }

        var user = new User
        {
            Address = new Address
            {
                City = registerViewModel.City, Street = registerViewModel.Street,
                HouseNumber = registerViewModel.HouseNumber
            },
            Email = registerViewModel.Email, Gender = registerViewModel.Gender, Name = registerViewModel.Username,
            BirthDate = registerViewModel.BirthDate
        };
        _repository.AddUser(user);

        var identityUser = new IdentityUser(registerViewModel.Email)
        {
            Email = registerViewModel.Email
        };
        var registerUserResult = await _userManager.CreateAsync(identityUser, registerViewModel.Password);

        if (!registerUserResult.Succeeded) {
            var genders = new[]
            {
                new { Answer = Gender.Male, Description = "Man" },
                new { Answer = Gender.Female, Description = "Vrouw" },
                new { Answer = Gender.Unknown, Description = "Ik zeg het liever niet" }
            };
            ViewBag.Gender = new SelectList(genders, "Answer", "Description");

            foreach (var error in registerUserResult.Errors) {
                ModelState.AddModelError("", error.Description);
            }

            return View();
        }

        await _userManager.AddClaimAsync(identityUser, new Claim("UserType", registerViewModel.UserType.ToString()));

        return RedirectToAction(nameof(Login));
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
    
    // Method for testing
    public User GetUserByEmail(string email)
    {
        return _repository.GetUserByEmail(email); 
    }
}