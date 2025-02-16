using M27.MetaBlog.Application.UseCases.Post.Common;
using MediatR;

namespace M27.MetaBlog.Application.UseCases.Post.GetPost;

public class GetPostInput: IRequest<PostOutput>
{
    public Guid Id { get; set; }
    
    public GetPostInput(Guid id) => Id = id;
}