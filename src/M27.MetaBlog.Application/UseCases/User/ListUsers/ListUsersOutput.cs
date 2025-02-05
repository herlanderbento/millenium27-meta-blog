using M27.MetaBlog.Application.Common;
using M27.MetaBlog.Application.UseCases.User.Common;

namespace M27.MetaBlog.Application.UseCases.User.ListUsers;

public class ListUsersOutput : PaginatedListOutput<UserOutput>
{
    public ListUsersOutput(
        int page, 
        int perPage, 
        int total, IReadOnlyList<UserOutput> items) 
        : base(page, perPage, total, items)
    {
    }
}