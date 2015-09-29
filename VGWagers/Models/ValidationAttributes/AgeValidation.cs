using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VGWagers.Models.ValidationAttributes
{
    public class AgeRangeValidationAttribute : ValidationAttribute, IClientValidatable
    {
        public int MinAge { get; set; }
        public int MaxAge { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            { return null; }
            int age = 0;

            age = DateTime.Now.Year - Convert.ToDateTime(value).Year;

            if (age >= MinAge && age <= MaxAge)
            {
                return null;
            }
            else
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule()
            {
                ValidationType = "agerangevalidation",
                ErrorMessage = FormatErrorMessage(metadata.DisplayName)
            };

            rule.ValidationParameters["minage"] = MinAge;
            rule.ValidationParameters["maxage"] = MaxAge;

            yield return rule;
        }
    }
}