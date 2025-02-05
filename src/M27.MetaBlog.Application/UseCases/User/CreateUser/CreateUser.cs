using M27.MetaBlog.Application.Exceptions;
using M27.MetaBlog.Application.Interfaces;
using M27.MetaBlog.Application.UseCases.User.Common;
using M27.MetaBlog.Domain.Repository;
using DomainEntity = M27.MetaBlog.Domain.Entity;

namespace M27.MetaBlog.Application.UseCases.User.CreateUser;

public class CreateUser: ICreateUser
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICryptography _cryptography;

    public CreateUser(IUserRepository userRepository, IUnitOfWork unitOfWork, ICryptography cryptography)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _cryptography = cryptography;
    }


    public async Task<UserOutput> Handle(CreateUserInput input, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(input.Email, cancellationToken);
        
        ConflictException.ThrowIfNotNull(user, "User Already Exists");

        var hashedPassword = await _cryptography.HashPassword(input.Password, cancellationToken);
        
        var entity = new DomainEntity.User(
            input.Name,
            input.Email,
            hashedPassword,
            input.Role
        );

        await _userRepository.Insert(entity, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        
        return UserOutput.FromUser(entity);
    }
}