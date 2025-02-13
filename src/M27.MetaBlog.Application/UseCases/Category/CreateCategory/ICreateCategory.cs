using MediatR;
using M27.MetaBlog.Application.UseCases.Category.Common;

namespace M27.MetaBlog.Application.UseCases.Category.CreateCategory;

public interface ICreateCategory : IRequestHandler<CreateCategoryInput, CategoryOutput>
{
    
}