using MediatR;
using M27.MetaBlog.Application.UseCases.Post.Common;

namespace M27.MetaBlog.Application.UseCases.Post.CreatePost;

public interface ICreatePost: IRequestHandler<CreatePostInput, PostOutput>
{
    
}