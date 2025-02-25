using M27.MetaBlog.Application.Common;
using M27.MetaBlog.Domain.Shared.SearchableRepository;
using MediatR;

namespace M27.MetaBlog.Application.UseCases.Category.ListCategories;

public class ListCategoriesInput: PaginatedListInput<string>, IRequest<ListCategoriesOutput>
{
    public ListCategoriesInput(
        int page = 1,
        int perPage = 15,
        string search = "",
        string sort = "",
        SearchOrder dir = SearchOrder.Asc) : 
        base(page, perPage, search, sort, dir)
    {
    }
    
    public ListCategoriesInput() 
        : base(1, 15, "", "", SearchOrder.Asc)
    { }
}