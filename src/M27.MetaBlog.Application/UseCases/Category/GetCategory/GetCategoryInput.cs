using M27.MetaBlog.Application.UseCases.Category.Common;
using MediatR;

namespace M27.MetaBlog.Application.UseCases.Category.GetCategory;

public class GetCategoryInput : IRequest<CategoryOutput>
{
    public Guid Id { get; set; }
    
    public GetCategoryInput(Guid id) => Id = id;
}