using M27.MetaBlog.Application.UseCases.User.Common;
using M27.MetaBlog.Domain.Repository;

namespace M27.MetaBlog.Application.UseCases.User.ListUsers;

public class ListUsers : IListUsers
{
    private readonly IUserRepository _userRepository;

    public ListUsers(IUserRepository userRepository)
        => _userRepository = userRepository;

    public async Task<ListUsersOutput> Handle(
        ListUsersInput input, 
        CancellationToken cancellationToken
        )
    {
        var searchOutput = await _userRepository.Search(
            new(
                input.Page, 
                input.PerPage, 
                input.Search, 
                input.Sort, 
                input.Dir
                ),
            cancellationToken
        );
        
        return new ListUsersOutput(
            searchOutput.CurrentPage,
            searchOutput.PerPage,
            searchOutput.Total,
            searchOutput.Items
                .Select(UserOutput.FromUser)
                .ToList()
        );
    }
}