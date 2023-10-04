using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_crud.CustomValidations
{
    public class MoreThan5Words : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string dataInput = (string)value;
            if(dataInput == null || dataInput.Split(' ').Length < 5)
            {
                return new ValidationResult("Minimo 5 parole!");
            }

            return ValidationResult.Success;
        }
    }
}
