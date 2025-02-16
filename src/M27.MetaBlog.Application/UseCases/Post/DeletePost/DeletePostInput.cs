using MediatR;

namespace M27.MetaBlog.Application.UseCases.Post.DeletePost;

public class DeletePostInput: IRequest
{
    public Guid Id { get; set; }
    
    public DeletePostInput(Guid id) => Id = id;
}