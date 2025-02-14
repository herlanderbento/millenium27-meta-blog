using M27.MetaBlog.Domain.Shared.Validation;

namespace M27.MetaBlog.Domain.Validator;

public static class CategoryValidator
{
    public static void Validate(string name, string description)
    {
        DomainValidation.NotNullOrEmpty(name, nameof(name));
        DomainValidation.MinLength(name, 3, nameof(name));
        DomainValidation.MaxLength(name, 255, nameof(name));

        DomainValidation.NotNull(description, nameof(description));
        DomainValidation.MaxLength(description, 4_000, nameof(description));
    }
}