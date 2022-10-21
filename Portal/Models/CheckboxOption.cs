namespace Portal.Models;

public class CheckboxOption
{
    public CheckboxOption(bool isChecked, string description, object value)
    {
        IsChecked = isChecked;
        Description = description;
        Value = value;
    }

    public bool IsChecked { get; set; }
    public string Description { get; set; }
    public object Value { get; set; }
}