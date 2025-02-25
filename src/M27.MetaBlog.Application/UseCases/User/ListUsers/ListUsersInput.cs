using M27.MetaBlog.Application.Common;
using M27.MetaBlog.Domain.Shared.SearchableRepository;
using MediatR;

namespace M27.MetaBlog.Application.UseCases.User.ListUsers;

public class ListUsersInput : PaginatedListInput<string>, IRequest<ListUsersOutput>
{
    public ListUsersInput(
        int page = 1,
        int perPage = 15,
        string search = "",
        string sort = "",
        SearchOrder dir = SearchOrder.Asc) : 
        base(page, perPage, search, sort, dir)
    {
    }
    
    public ListUsersInput() 
        : base(1, 15, "", "", SearchOrder.Asc)
    { }
}