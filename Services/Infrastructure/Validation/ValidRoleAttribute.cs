using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Infrastructure.Validation
{
    public class ValidRoleAttribute : ValidationAttribute
    {
        public ValidRoleAttribute()
        {
            ErrorMessage =
                "Please choose a valid role!";
        }

        protected override ValidationResult? IsValid
            (object? value, ValidationContext validationContext)
        {
            string chosenRole = value.ToString();

            if (string.IsNullOrEmpty(chosenRole))
                return new ValidationResult(ErrorMessage);

            var roles = new[]{
                "Admin", "Cashier"
            };

            if (roles.Contains(chosenRole))
                return ValidationResult.Success;
            return
                new ValidationResult(ErrorMessage);
        }
    }

}
