using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Portal.Models;

public class FoodViewModel
{
    [Display(Name = "Naam: ")]
    public string Name { get; set; }
    
    [ValidateNever]
    [Display(Name = "Dieetwensen/Allergieën: ")]
    public List<CheckboxOption> Allergies { get; set; }
    
    [ValidateNever]
    public List<int> Allergy { get; set; }
}