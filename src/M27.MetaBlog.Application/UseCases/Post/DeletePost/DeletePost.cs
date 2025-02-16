using M27.MetaBlog.Application.Exceptions;
using M27.MetaBlog.Application.Interfaces;
using M27.MetaBlog.Domain.Repository;

namespace M27.MetaBlog.Application.UseCases.Post.DeletePost;

public class DeletePost: IDeletePost
{
    private readonly IPostRepository _postRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePost(
        IPostRepository postRepository,
        IUnitOfWork unitOfWork
        )
    {
        _postRepository = postRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task Handle(
        DeletePostInput input,
        CancellationToken cancellationToken
        )
    {
        var post = await _postRepository.GetById(input.Id, cancellationToken);
        
        NotFoundException.ThrowIfNull(post, $"Post {input.Id} not found");
        
        await _postRepository.Delete(post, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
    }
}