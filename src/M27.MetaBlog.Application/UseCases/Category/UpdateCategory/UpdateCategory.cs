using M27.MetaBlog.Application.Exceptions;
using M27.MetaBlog.Application.Interfaces;
using M27.MetaBlog.Application.UseCases.Category.Common;
using M27.MetaBlog.Domain.Repository;

namespace M27.MetaBlog.Application.UseCases.Category.UpdateCategory;

public class UpdateCategory: IUpdateCategory
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategory(
        ICategoryRepository categoryRepository, 
        IUnitOfWork unitOfWork
        )
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<CategoryOutput> Handle(
        UpdateCategoryInput input,
        CancellationToken cancellationToken
        )
    {
        var category = await _categoryRepository.GetById(input.Id, cancellationToken);
        
        NotFoundException.ThrowIfNull(category, $"Category {input.Id} not found");
        
        category.Update(input.Name, input.Description);
        
        if(input.IsActive != null && input.IsActive != category.IsActive)
            if((bool)input.IsActive!) category.Activate();
            else category.Deactivate();
        
        await _categoryRepository.Update(category, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        
        return  CategoryOutput.FromCategory(category);
    }
}