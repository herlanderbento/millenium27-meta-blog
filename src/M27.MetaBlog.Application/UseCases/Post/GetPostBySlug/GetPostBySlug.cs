using M27.MetaBlog.Application.Exceptions;
using M27.MetaBlog.Application.UseCases.Post.Common;
using M27.MetaBlog.Domain.Repository;
using M27.MetaBlog.Domain.ValueObject;

namespace M27.MetaBlog.Application.UseCases.Post.GetPostBySlug;

public class GetPostBySlug: IGetPostBySlug
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICategoryRepository _categoryRepository;


    public GetPostBySlug(
        IPostRepository postRepository, 
        IUserRepository userRepository, 
        ICategoryRepository categoryRepository
        )
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _categoryRepository = categoryRepository;
    }
    
    public async Task<PostOutput> Handle(
        GetPostBySlugInput input, 
        CancellationToken cancellationToken
        )
    {
        var slug = Slug.Create(input.Slug);
        var post = await _postRepository.GetBySlug(slug, cancellationToken);
        
        NotFoundException.ThrowIfNull(post, $"Post {input.Slug} not found");

        var author = await _userRepository.GetById(post.AuthorId, cancellationToken);
        var category = await _categoryRepository.GetById(post.CategoryId, cancellationToken);
        
        return PostOutput.FromPost(post, author, category);
    }
}