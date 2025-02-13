using MediatR;

namespace M27.MetaBlog.Application.UseCases.Category.ListCategories;

public interface IListCategories: IRequestHandler<ListCategoriesInput, ListCategoriesOutput>
{
    
}