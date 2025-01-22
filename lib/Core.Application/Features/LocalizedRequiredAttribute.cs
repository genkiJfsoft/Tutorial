//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations;
//using Microsoft.Extensions.Localization;

//namespace ExpenseTracker.Core.Application.Features;
//public class LocalizedRequiredAttribute : ValidationAttribute
//{
//    private readonly string _localizationKey;

//    public LocalizedRequiredAttribute(string localizationKey)
//    {
//        _localizationKey = localizationKey;
//    }

//    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
//    {
//        // Fetch the IStringLocalizer from the ValidationContext
//        var localizer = validationContext.GetService(typeof(IStringLocalizer)) as IStringLocalizer;

//        if (localizer == null)
//        {
//            throw new InvalidOperationException("IStringLocalizer is not registered in the validation context.");
//        }

//        // Fetch the localized error message
//        var localizedMessage = localizer[_localizationKey];

//        // If value is null or whitespace, return validation error
//        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
//        {
//            return new ValidationResult(localizedMessage);
//        }

//        return ValidationResult.Success;
//    }
//}

//public static class ValidationHelper
//{
//    public static ValidationContext CreateValidationContext(object model, IServiceProvider serviceProvider)
//    {
//        var validationContext = new ValidationContext(model, serviceProvider, null);
//        return validationContext;
//    }
//}
