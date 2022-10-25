using System.ComponentModel.DataAnnotations;

namespace ApplicationServices;

public class FutureDateTime : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        var dateTime = Convert.ToDateTime(value);
        return dateTime >= DateTime.Now; //Dates Greater than or equal to today are valid (true)
    }
}

public class PastDateTime : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        var dateTime = Convert.ToDateTime(value);
        return dateTime < DateTime.Now; //Dates Greater than or equal to today are valid (true)
    }
}