using M27.MetaBlog.Domain.Shared.Validation;

namespace M27.MetaBlog.Domain.Shared.Exceptions;

public class EntityValidationException : Exception
{
    public IReadOnlyCollection<ValidationError>? Errors { get; }
    public EntityValidationException(
        string? message, 
        IReadOnlyCollection<ValidationError>? errors = null
    ) : base(message) 
        => Errors = errors;
}