using M27.MetaBlog.Application.Exceptions;
using M27.MetaBlog.Application.Interfaces;
using M27.MetaBlog.Domain.Repository;

namespace M27.MetaBlog.Application.UseCases.User.DeleteUser;

public class DeleteUser : IDeleteUser
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUser(IUserRepository userRepository, IUnitOfWork unitOfWork)
        => (_userRepository, _unitOfWork) = (userRepository, unitOfWork);
    
    public async Task Handle(
        DeleteUserInput input, 
        CancellationToken cancellationToken
    )
    {
        var user = await  _userRepository.GetById(input.Id, cancellationToken);
        
        NotFoundException.ThrowIfNull(user, $"User '{input.Id}' not found");
        
        await _userRepository.Delete(user, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
    }
}