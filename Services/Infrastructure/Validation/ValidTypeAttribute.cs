using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Infrastructure.Validation
{
    public class ValidTypeAttribute : ValidationAttribute
    {
        public ValidTypeAttribute()
        {
            ErrorMessage =
                "Please choose a valid type!";
        }

        protected override ValidationResult? IsValid
            (object? value, ValidationContext validationContext)
        {
            string chosenType = value.ToString();

            if (string.IsNullOrEmpty(chosenType))
                return new ValidationResult(ErrorMessage);

            var types = new[]{
                "Credit", "Debit"
            };

            if (types.Contains(chosenType))
                return ValidationResult.Success;
            return
                new ValidationResult(ErrorMessage);
        }
    }

}
