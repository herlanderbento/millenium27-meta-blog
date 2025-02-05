using MediatR;

namespace M27.MetaBlog.Application.UseCases.User.ListUsers;

public interface IListUsers: IRequestHandler<ListUsersInput, ListUsersOutput>
{
    
}