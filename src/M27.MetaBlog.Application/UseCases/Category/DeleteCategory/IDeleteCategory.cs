using MediatR;

namespace M27.MetaBlog.Application.UseCases.Category.DeleteCategory;

public interface IDeleteCategory : IRequestHandler<DeleteCategoryInput>
{
    
}