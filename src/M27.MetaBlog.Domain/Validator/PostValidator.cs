using M27.MetaBlog.Domain.Shared.Validation;

namespace M27.MetaBlog.Domain.Validator;

public static class PostValidator
{
    public static void Validate(
        Guid authorId,
        Guid categoryId,
        string title, 
        string description
        )
    {
        DomainValidation.NotNullOrEmpty(authorId.ToString(), nameof(authorId));
        DomainValidation.NotNullOrEmpty(categoryId.ToString(), nameof(categoryId));

        DomainValidation.NotNullOrEmpty(title, nameof(title));
        DomainValidation.MinLength(title, 3, nameof(title));
        DomainValidation.MaxLength(title, 255, nameof(title));

        DomainValidation.NotNullOrEmpty(description, nameof(description));
        DomainValidation.MaxLength(description, 10_000, nameof(description));
    }
}