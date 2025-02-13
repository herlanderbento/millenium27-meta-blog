using FluentValidation;
using M27.MetaBlog.Domain.Shared.Exceptions;

namespace M27.MetaBlog.Api.Validators;

public class RequestValidator(IServiceProvider serviceProvider)
{
    public void Validate<T>(T request, CancellationToken cancellationToken)
    {
        var validator = serviceProvider.GetRequiredService<IValidator<T>>();
        var validationResult = validator.ValidateAsync(request, cancellationToken).Result;

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors
                .Select(e => $"{e.PropertyName}: {e.ErrorMessage}")
                .ToList();

            throw new EntityValidationException(string.Join(" | ", errorMessages));
        }
    }
}