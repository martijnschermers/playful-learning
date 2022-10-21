using Core.Domain;
using Core.DomainServices;
using Microsoft.AspNetCore.Mvc;
using Portal.Models;

namespace Portal.Controllers;

public class FoodController : Controller
{
    private readonly IGameNightRepository _repository; 
    private readonly IAllergyRepository _allergyRepository; 
    
    public FoodController(IGameNightRepository repository, IAllergyRepository allergyRepository)
    {
        _repository = repository;
        _allergyRepository = allergyRepository;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var allergies = _allergyRepository!.GetAllAllergies()
            .Select(allergy => new CheckboxOption(false, allergy.Description, allergy.Id))
            .ToList();

        return View(new FoodViewModel { Allergies = allergies });
    }

    [HttpPost]
    public IActionResult Index(FoodViewModel foodViewModel)
    {
        var checkboxOptions = _allergyRepository!.GetAllAllergies()
            .Select(allergy => new CheckboxOption(false, allergy.Description, allergy.Id))
            .ToList();

        var returnViewModel = new FoodViewModel { Allergies = checkboxOptions };
        
        if (!ModelState.IsValid) {
            return View(returnViewModel);
        }

        var id = GetId(); 
        
        var allergies = foodViewModel.Allergy
            .Select(allergyId => _allergyRepository!.GetAllergyById(allergyId))
            .ToList();

        var food = new Food
        {
            Name = foodViewModel.Name,
            Allergies = allergies
        };

        var result = _repository.AddFood(id, food);

        if (!result) {
            ModelState.AddModelError("", "Spelavond niet gevonden!");
            return View(returnViewModel);
        }
        
        return Redirect($"/GameNight/Details/{id}"); 
    }
    
    public int GetId()
    {
        return int.Parse(Url.ActionContext.RouteData.Values["id"]!.ToString()!);
    }
}