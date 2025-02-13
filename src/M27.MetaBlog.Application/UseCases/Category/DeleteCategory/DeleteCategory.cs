using M27.MetaBlog.Application.Exceptions;
using M27.MetaBlog.Application.Interfaces;
using M27.MetaBlog.Domain.Repository;

namespace M27.MetaBlog.Application.UseCases.Category.DeleteCategory;

public class DeleteCategory : IDeleteCategory
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategory(
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork
        )
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        DeleteCategoryInput input, 
        CancellationToken cancellationToken
        )
    {
        var category = await _categoryRepository.GetById(input.Id, cancellationToken);
        
        NotFoundException.ThrowIfNull(category, $"Category {input.Id} not found");
        
        await _categoryRepository.Delete(category, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
    }
}

