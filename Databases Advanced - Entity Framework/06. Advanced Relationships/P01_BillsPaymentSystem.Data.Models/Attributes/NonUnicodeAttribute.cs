using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P01_BillsPaymentSystem.Data.Models.Attributes
{
    public class NonUnicodeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string nullErrorMessage = "Value cannot be null!";

            if (value == null)
            {
                return new ValidationResult(nullErrorMessage);
            }

            string text = (string)value;

            for (int i = 0; i < text.Length; i++)
            {
                string errorMessage = "Value cannot contain unicode characters!";

                if (text[i] > 255)
                {
                    return new ValidationResult(errorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
