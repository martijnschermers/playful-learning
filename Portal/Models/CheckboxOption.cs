using Core.Domain;

namespace Portal.Models;

public class CheckboxOption<T> where T : ICheckboxOption 
{
    public CheckboxOption(bool isChecked, string description, T value)
    {
        IsChecked = isChecked;
        Description = description;
        Value = value;
    }

    public bool IsChecked { get; set; }
    public string Description { get; set; }
    public T Value { get; set; }
}