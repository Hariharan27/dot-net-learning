using System;
using System.ComponentModel.DataAnnotations;

namespace SampleWebAPI.Validators
{
	public class NoBadWordsAttribute: ValidationAttribute
    {
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is string){
                string? str = value as string;
               if(str != null && str.Contains("badword", StringComparison.OrdinalIgnoreCase))
                {
                    return  new ValidationResult("Containes Prohibeted word");
                }
            }

            return ValidationResult.Success;
        }

    }
}

