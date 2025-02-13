using M27.MetaBlog.Application.UseCases.Category.Common;
using MediatR;

namespace M27.MetaBlog.Application.UseCases.Category.GetCategory;

public interface IGetCategory : IRequestHandler<GetCategoryInput, CategoryOutput>
{
    
}