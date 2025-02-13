using M27.MetaBlog.Application.UseCases.Category.Common;
using MediatR;

namespace M27.MetaBlog.Application.UseCases.Category.UpdateCategory;

public interface IUpdateCategory: IRequestHandler<UpdateCategoryInput, CategoryOutput>
{
    
}