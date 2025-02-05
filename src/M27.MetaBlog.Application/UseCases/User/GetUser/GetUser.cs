using M27.MetaBlog.Application.Exceptions;
using M27.MetaBlog.Application.UseCases.User.Common;
using M27.MetaBlog.Domain.Repository;

namespace M27.MetaBlog.Application.UseCases.User.GetUser;

public class GetUser: IGetUser
{
    private readonly IUserRepository _userRepository;
    
    public GetUser(IUserRepository userRepository)
        => _userRepository = userRepository;

    public async Task<UserOutput> Handle(
        GetUserInput input, 
        CancellationToken cancellationToken
        )
    {
        var user = await  _userRepository.GetById(input.Id, cancellationToken);
        
        NotFoundException.ThrowIfNull(user, $"User '{input.Id}' not found");
        
        return UserOutput.FromUser(user);
    }
}