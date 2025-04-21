using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Infrastructure.Validation
{
    public class ValidGenderAttribute : ValidationAttribute
    {
        public ValidGenderAttribute()
        {
            ErrorMessage =
                "Please choose a valid gender!";
        }

        protected override ValidationResult? IsValid
            (object? value, ValidationContext validationContext)
        {
            string chosenGender = value.ToString();

            if (string.IsNullOrEmpty(chosenGender))
                return new ValidationResult(ErrorMessage);

            var genders = new[]{
                "Male", "Female"
            };

            if (genders.Contains(chosenGender))
                return ValidationResult.Success;
            return
                new ValidationResult(ErrorMessage);
        }
    }

}
