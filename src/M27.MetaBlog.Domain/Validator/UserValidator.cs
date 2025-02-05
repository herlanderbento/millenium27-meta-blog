using M27.MetaBlog.Domain.Entity;
using M27.MetaBlog.Domain.Shared.Validation;
using System.Text.RegularExpressions;

namespace M27.MetaBlog.Domain.Validator;

public class UserValidator: Shared.Validation.Validator
{
    private readonly User _user;

    private const int NameMaxLength = 255;
    private const int EmailMaxLength = 255;
    private const int PasswordMaxLength = 255;
    
    private static readonly Regex EmailRegex = 
        new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);


    public UserValidator(User user, ValidationHandler handler) : base(handler) => _user = user;

    public override void Validate()
    {
        ValidateName();
        ValidateEmail();
        ValidatePassword();
    }
    
    private void ValidateName()
    {
        if (string.IsNullOrWhiteSpace(_user.Name))
            _handler.HandleError($"'{nameof(_user.Name)}' is required");

        if (_user.Name.Length > 255)
            _handler.HandleError($"'{nameof(_user.Name)}' should be less or equal {NameMaxLength} characters long");
    }
    
    private void ValidateEmail()
    {
        if (string.IsNullOrWhiteSpace(_user.Email))
            _handler.HandleError($"'{nameof(_user.Email)}' is required");

        if (_user.Email.Length > 255)
            _handler.HandleError($"'{nameof(_user.Email)}' should be less or equal {EmailMaxLength} characters long");
        
        if (!EmailRegex.IsMatch(_user.Email))
            _handler.HandleError($"'{nameof(_user.Email)}' is not in a valid format");
    }
    
    private void ValidatePassword()
    {
        if (string.IsNullOrWhiteSpace(_user.Password))
            _handler.HandleError($"'{nameof(_user.Password)}' is required");

        if (_user.Password.Length > 255)
            _handler.HandleError($"'{nameof(_user.Password)}' should be less or equal {PasswordMaxLength} characters long");
    }
}