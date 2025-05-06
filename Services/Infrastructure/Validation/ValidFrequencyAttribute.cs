using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Infrastructure.Validation
{
    public class ValidFrequencyAttribute : ValidationAttribute
    {
        public ValidFrequencyAttribute()
        {
            ErrorMessage =
                "Please choose a valid frequency!";
        }

        protected override ValidationResult? IsValid
            (object? value, ValidationContext validationContext)
        {
            string chosenFrequency = value.ToString();

            if (string.IsNullOrEmpty(chosenFrequency))
                return new ValidationResult(ErrorMessage);

            var frequencies = new[]{
                "Weekly", "Monthly", "AfterTransaction"
            };

            if (frequencies.Contains(chosenFrequency))
                return ValidationResult.Success;
            return
                new ValidationResult(ErrorMessage);
        }
    }

}
