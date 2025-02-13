using M27.MetaBlog.Application.Exceptions;
using M27.MetaBlog.Application.Interfaces;
using M27.MetaBlog.Application.UseCases.Category.Common;
using M27.MetaBlog.Domain.Repository;
using DomainEntity = M27.MetaBlog.Domain.Entity;

namespace M27.MetaBlog.Application.UseCases.Category.CreateCategory;

public class CreateCategory: ICreateCategory
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategory(
        ICategoryRepository categoryRepository, 
        IUnitOfWork unitOfWork
        )
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async  Task<CategoryOutput> Handle(
        CreateCategoryInput input, 
        CancellationToken cancellationToken
        )
    {
        var category = await _categoryRepository.GetByName(input.Name, cancellationToken);
        
        ConflictException.ThrowIfNotNull(category, $"Category {input.Name} Already Exists.");
        
        var entity = new DomainEntity.Category(
            input.Name,
            input.Description,
            input.IsActive
            );
        
        await _categoryRepository.Insert(entity, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        
        return CategoryOutput.FromCategory(entity);
    }
}
