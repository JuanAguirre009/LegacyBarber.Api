using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LegacyBarber.App.Api.Filters
{
    /// <summary>
    /// Runs registered FluentValidation validators for action arguments automatically.
    /// </summary>
    public sealed class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (object? argument in context.ActionArguments.Values)
            {
                if (argument is null)
                    continue;

                Type validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());

                if (context.HttpContext.RequestServices.GetService(validatorType) is not IValidator validator)
                    continue;

                IValidationContext validationContext = new ValidationContext<object>(argument);
                FluentValidation.Results.ValidationResult result = await validator.ValidateAsync(validationContext, context.HttpContext.RequestAborted);

                if (!result.IsValid)
                {
                    foreach (FluentValidation.Results.ValidationFailure error in result.Errors)
                    {
                        context.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(new ValidationProblemDetails(context.ModelState));
                return;
            }

            await next();
        }
    }
}
