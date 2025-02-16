using M27.MetaBlog.Application.Common;
using M27.MetaBlog.Domain.Shared.SearchableRepository;
using MediatR;

namespace M27.MetaBlog.Application.UseCases.Post.ListPosts;

public class ListPostsInput: PaginatedListInput, IRequest<ListPostsOutput>
{
    public ListPostsInput(
        int page = 1,
        int perPage = 15,
        string search = "",
        string sort = "",
        SearchOrder dir = SearchOrder.Asc
        ):base(page, perPage, search, sort, dir)
    {}
}