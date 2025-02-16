using M27.MetaBlog.Application.Exceptions;
using M27.MetaBlog.Application.UseCases.Post.Common;
using M27.MetaBlog.Domain.Repository;

namespace M27.MetaBlog.Application.UseCases.Post.GetPost;

public class GetPost: IGetPost
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICategoryRepository _categoryRepository;

    public GetPost(
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
        GetPostInput input, 
        CancellationToken cancellationToken
        )
    {
        var post = await _postRepository.GetById(input.Id, cancellationToken);
        
        NotFoundException.ThrowIfNull(post, $"Post {input.Id} not found");
        
        var user = await _userRepository.GetById(post.AuthorId, cancellationToken);
        
        NotFoundException.ThrowIfNull(user, $"User {post.AuthorId} not found");
        
        var category = await _categoryRepository.GetById(post.CategoryId, cancellationToken);
        
        NotFoundException.ThrowIfNull(category, $"Category {post.CategoryId} not found");

        
        return PostOutput.FromPost(post, user, category);
    }
}