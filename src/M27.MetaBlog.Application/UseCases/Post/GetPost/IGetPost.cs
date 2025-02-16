using M27.MetaBlog.Application.UseCases.Post.Common;
using MediatR;

namespace M27.MetaBlog.Application.UseCases.Post.GetPost;

public interface IGetPost: IRequestHandler<GetPostInput, PostOutput>
{
    
}