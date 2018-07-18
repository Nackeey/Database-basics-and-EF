using System;
using System.ComponentModel.DataAnnotations;

namespace P01_BillsPaymentSystem.Data.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class XorAttribute : ValidationAttribute
    {
        private string xorTargetAttribute;

        public XorAttribute(string XorTargetAttribute) 
        {
            this.xorTargetAttribute = XorTargetAttribute;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var targetAttribute = validationContext
                .ObjectType
                .GetProperty(xorTargetAttribute)
                .GetValue(validationContext.ObjectInstance);

            if ((targetAttribute == null && value != null) || (targetAttribute != null && value == null))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("The one properties must be null!");
        }
    }
}
