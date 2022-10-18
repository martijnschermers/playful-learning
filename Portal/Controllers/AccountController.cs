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
    private readonly IAllergyRepository? _allergyRepository;
    private readonly UserManager<IdentityUser>? _userManager;
    private readonly SignInManager<IdentityUser>? _signInManager;

    public AccountController(IUserRepository repository, IAllergyRepository? allergyRepository,
        UserManager<IdentityUser>? userManager, SignInManager<IdentityUser>? signInManager)
    {
        _repository = repository;
        _userManager = userManager;
        _signInManager = signInManager;
        _allergyRepository = allergyRepository;
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

        var user = await _userManager!.FindByEmailAsync(loginViewModel.Email);
        if (user == null) {
            ModelState.AddModelError("", "Invalide emailadres of wachtwoord!");
            return View();
        }

        var signinResult = await _signInManager!.PasswordSignInAsync(user, loginViewModel.Password, true, false);

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

        FillViewBag();

        return View(new RegisterViewModel { Allergies = _allergyRepository!.GetAllAllergies() });
    }

    [HttpPost]
    public async Task<IActionResult> RegisterAsync(RegisterViewModel registerViewModel)
    {
        var returnViewModel = new RegisterViewModel { Allergies = _allergyRepository!.GetAllAllergies() };

        if (!ModelState.IsValid) {
            FillViewBag();
            return View(returnViewModel);
        }

        if (await _userManager!.FindByEmailAsync(registerViewModel.Email) != null) {
            ModelState.AddModelError("", "Emailadres is al in gebruik!");
            FillViewBag();
            return View(returnViewModel);
        }

        //TODO: Maybe move this to service???
        var allergyIds = Request.Form.First(f => f.Key == "Allergy");
        var allergies = allergyIds.Value.Select(allergyId => _allergyRepository!.GetAllergyById(int.Parse(allergyId)))
            .ToList();

        var user = new User
        {
            Address = new Address
            {
                City = registerViewModel.City, Street = registerViewModel.Street,
                HouseNumber = registerViewModel.HouseNumber
            },
            Allergies = allergies, IsVegetarian = registerViewModel.IsVegetarian,
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
            FillViewBag();

            foreach (var error in registerUserResult.Errors) {
                ModelState.AddModelError("", error.Description);
            }

            return View(returnViewModel);
        }

        await _userManager.AddClaimAsync(identityUser, new Claim("UserType", registerViewModel.UserType.ToString()));

        return RedirectToAction(nameof(Login));
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager!.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }

    private void FillViewBag()
    {
        var genders = new[]
        {
            new { Answer = Gender.Male, Description = "Man" },
            new { Answer = Gender.Female, Description = "Vrouw" },
            new { Answer = Gender.Unknown, Description = "Ik zeg het liever niet" }
        };
        ViewBag.Gender = new SelectList(genders, "Answer", "Description");

        var types = new[]
        {
            new { Answer = UserType.Organizer, Description = "Organisator" },
            new { Answer = UserType.Participant, Description = "Speler" }
        };
        ViewBag.UserTypes = new SelectList(types, "Answer", "Description");
    }

    // Method for testing
    public User GetUserByEmail(string email)
    {
        return _repository.GetUserByEmail(email);
    }
}