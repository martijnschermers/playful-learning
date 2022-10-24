using ApplicationServices;
using Core.Domain;
using Core.DomainServices;
using Microsoft.AspNetCore.Mvc;
using Portal.Models;

namespace Portal.Controllers;

public class FoodController : Controller
{
    private readonly IGameNightRepository _repository; 
    private readonly IAllergyRepository _allergyRepository;
    private readonly IHelperService _helperService;
    
    public FoodController(IGameNightRepository repository, IAllergyRepository allergyRepository, IHelperService helperService)
    {
        _repository = repository;
        _allergyRepository = allergyRepository;
        _helperService = helperService;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var allergies = _allergyRepository.GetAllAllergies()
            .Select(allergy => new CheckboxOption(false, allergy.Description, allergy.Id))
            .ToList();

        return View(new FoodViewModel { Allergies = allergies });
    }

    [HttpPost]
    public IActionResult Index(int id, FoodViewModel foodViewModel)
    {
        var checkboxOptions = _allergyRepository.GetAllAllergies()
            .Select(allergy => new CheckboxOption(false, allergy.Description, allergy.Id))
            .ToList();

        var returnViewModel = new FoodViewModel { Allergies = checkboxOptions };
        
        if (!ModelState.IsValid) {
            return View(returnViewModel);
        }
        
        var user = _helperService.GetUser(HttpContext);

        var allergyIds = foodViewModel.Allergy ?? new List<int>();
        
        var allergies = _allergyRepository.GetAllergiesByIds(allergyIds);

        var food = new Food
        {
            Name = foodViewModel.Name,
            Allergies = allergies, 
            UserId = user.Id
        };

        var result = _repository.AddFood(id, food);

        if (!result) {
            ModelState.AddModelError("", "Spelavond niet gevonden!");
            return View(returnViewModel);
        }
        
        return Redirect($"/GameNight/Details/{id}"); 
    }
}