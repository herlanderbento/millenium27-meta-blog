using M27.MetaBlog.Application.Common;
using M27.MetaBlog.Application.UseCases.Category.Common;

namespace M27.MetaBlog.Application.UseCases.Category.ListCategories;

public class ListCategoriesOutput: PaginatedListOutput<CategoryOutput>
{
    public ListCategoriesOutput(
        int page, 
        int perPage, 
        int total, IReadOnlyList<CategoryOutput> items) 
        : base(page, perPage, total, items)
    {
    }
}


