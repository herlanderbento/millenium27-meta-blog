using M27.MetaBlog.Application.Exceptions;
using M27.MetaBlog.Application.Interfaces;
using M27.MetaBlog.Application.UseCases.User.Common;
using M27.MetaBlog.Domain.Repository;

namespace M27.MetaBlog.Application.UseCases.User.UpdateUser;

public class UpdateUser : IUpdateUser
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICryptography _cryptography;

    public UpdateUser(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork, 
        ICryptography cryptography)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _cryptography = cryptography;
    }

    public async Task<UserOutput> Handle(
        UpdateUserInput input, 
        CancellationToken cancellationToken
        )
    {
        var user = await _userRepository.GetById(input.Id, cancellationToken);
        
        NotFoundException.ThrowIfNull(user, $"User '{input.Id}' not found");

        user.Update(input.Name, input.Email, input.Role, input.IsActive);
        
        if (input.Password != null)
        {
            var hashedPassword = await _cryptography.HashPassword(input.Password, cancellationToken);
            
            user.ChangePassword(hashedPassword);
        }
        
        await _userRepository.Update(user, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        
        return UserOutput.FromUser(user);
    }
}  