using M27.MetaBlog.Application.Exceptions;
using M27.MetaBlog.Application.Interfaces;
using M27.MetaBlog.Application.UseCases.Category.Common;
using M27.MetaBlog.Domain.Repository;

namespace M27.MetaBlog.Application.UseCases.Category.GetCategory;

public class GetCategory : IGetCategory
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;


    public GetCategory(
        ICategoryRepository categoryRepository, 
        IUnitOfWork unitOfWork
        )
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryOutput> Handle(
        GetCategoryInput input, 
        CancellationToken cancellationToken
        )
    {
       var category = await _categoryRepository.GetById(input.Id, cancellationToken);
       
       NotFoundException.ThrowIfNull(category, $"Category {input.Id} not found");
       
       return CategoryOutput.FromCategory(category);
    }
}