using M27.MetaBlog.Application.Exceptions;
using M27.MetaBlog.Application.Interfaces;
using M27.MetaBlog.Application.UseCases.Post.Common;
using M27.MetaBlog.Domain.Repository;

namespace M27.MetaBlog.Application.UseCases.Post.UpdatePost;

public class UpdatePost: IUpdatePost
{
    private readonly IPostRepository _postRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePost(
        IPostRepository postRepository,
        ICategoryRepository categoryRepository, 
        IUnitOfWork unitOfWork
        )
    {
        _postRepository = postRepository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PostOutput> Handle(
        UpdatePostInput input, 
        CancellationToken cancellationToken
        )
    {
        var post = await _postRepository.GetById(input.Id, cancellationToken);
        
        NotFoundException.ThrowIfNull(post, $"Post {input.Id} not found");

        if (input.CategoryId.HasValue && input.CategoryId != post.CategoryId)
        {
            var category = await _categoryRepository.GetById(input.CategoryId.Value, cancellationToken);
            NotFoundException.ThrowIfNull(category, $"Category {input.CategoryId} not found");
        }

        post.Update(
            input.CategoryId,
            input.Title,
            input.Description,
            input.Published
            );
        
        await _postRepository.Update(post, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        
        return PostOutput.FromPost(post);
            
    }
}