using M27.MetaBlog.Application.UseCases.Post.Common;
using MediatR;

namespace M27.MetaBlog.Application.UseCases.Post.UpdatePost;

public interface IUpdatePost: IRequestHandler<UpdatePostInput, PostOutput>
{
    
}