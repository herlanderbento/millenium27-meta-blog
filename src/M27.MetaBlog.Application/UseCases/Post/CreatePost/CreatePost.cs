using M27.MetaBlog.Application.Common;
using M27.MetaBlog.Application.Exceptions;
using M27.MetaBlog.Application.Interfaces;
using M27.MetaBlog.Application.UseCases.Post.Common;
using DomainEntity = M27.MetaBlog.Domain.Entity;
using M27.MetaBlog.Domain.Repository;

namespace M27.MetaBlog.Application.UseCases.Post.CreatePost;

public class CreatePost : ICreatePost
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStorageService _storageService;

    public CreatePost(
        IPostRepository postRepository, 
        IUserRepository userRepository, 
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork,
        IStorageService storageService
        )
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
        _storageService = storageService;
    }

    public async Task<PostOutput> Handle(
        CreatePostInput input, 
        CancellationToken cancellationToken
        )
    {
        var user = await _userRepository.GetById(input.AuthorId, cancellationToken);
        
        NotFoundException.ThrowIfNull(user, $"User {input.AuthorId} not found");
        
        var category = await _categoryRepository.GetById(input.CategoryId, cancellationToken);
        
        NotFoundException.ThrowIfNull(category, $"Category {input.CategoryId} not found");

        var entity = new DomainEntity.Post(
            input.AuthorId,
            input.CategoryId,
            input.Title,
            input.Description,
            input.Published
        );

        await _postRepository.Insert(entity, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        
        return PostOutput.FromPost(entity, user, category);
    }

    private async Task ClearStorage(
        CreatePostInput input, 
        DomainEntity.Post post, 
        CancellationToken cancellationToken
        )
    {
        await _storageService.Delete(post.Image!.Path, cancellationToken);
    }

    private async Task UploadImage(
        CreatePostInput input, 
        DomainEntity.Post post, 
        CancellationToken cancellationToken
        )
    {
        var fileName = StorageFileName.Create(
            post.Id,
            nameof(post.Image), 
            input.Image!.Extension
            );
        
        var uploadFilePath = await _storageService.Upload(
            fileName,
            input.Image.FileStream,
            input.Image.ContentType,
            cancellationToken
        );
        post.UpdateImage(uploadFilePath);
    }
}