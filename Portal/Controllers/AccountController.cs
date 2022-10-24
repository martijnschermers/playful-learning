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
    private readonly IAllergyRepository _allergyRepository;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(IUserRepository repository, IAllergyRepository allergyRepository,
        UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _repository = repository;
        _userManager = userManager;
        _signInManager = signInManager;
        _allergyRepository = allergyRepository;

        IdentitySeedData.SeedUsers(userManager, repository).Wait(); 
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

        FillViewBag();

        var allergies = _allergyRepository.GetAllAllergies()
            .Select(allergy => new CheckboxOption(false, allergy.Description, allergy.Id))
            .ToList();
        
        return View(new RegisterViewModel { Allergies = allergies });
    }

    [HttpPost]
    public async Task<IActionResult> RegisterAsync(RegisterViewModel registerViewModel)
    {
        var incomingAllergies = _allergyRepository.GetAllAllergies()
            .Select(allergy => new CheckboxOption(false, allergy.Description, allergy.Id))
            .ToList();

        var returnViewModel = new RegisterViewModel { Allergies = incomingAllergies };

        if (!ModelState.IsValid) {
            FillViewBag();
            return View(returnViewModel);
        }
        
        if (await _userManager.FindByEmailAsync(registerViewModel.Email) != null) {
            ModelState.AddModelError("", "Emailadres is al in gebruik!");
            FillViewBag();
            return View(returnViewModel);
        }

        var allergyIds = registerViewModel.Allergy ?? new List<int>();

        var allergies = _allergyRepository.GetAllergiesByIds(allergyIds);

        User? user; 
        try {
            user = new User
            {
                Address = new Address
                {
                    City = registerViewModel.City, Street = registerViewModel.Street,
                    HouseNumber = registerViewModel.HouseNumber
                },
                Allergies = allergies,
                Email = registerViewModel.Email, Gender = registerViewModel.Gender, Name = registerViewModel.Username,
                BirthDate = registerViewModel.BirthDate
            };
        } catch (InvalidOperationException e) {
            ModelState.AddModelError("", e.Message);
            FillViewBag();
            return View(returnViewModel);
        }
        
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
        await _signInManager.SignOutAsync();

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
    public User? GetUserByEmail(string email)
    {
        return _repository.GetUserByEmail(email);
    }
}