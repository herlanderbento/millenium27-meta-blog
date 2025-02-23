using M27.MetaBlog.Application.UseCases.Post.Common;
using MediatR;

namespace M27.MetaBlog.Application.UseCases.Post.GetPostBySlug;

public interface IGetPostBySlug: IRequestHandler<GetPostBySlugInput, PostOutput>
{
    
}