using MediatR;

namespace M27.MetaBlog.Application.UseCases.User.DeleteUser;

public class DeleteUserInput : IRequest
{
    public Guid Id { get; set; }
    public DeleteUserInput(Guid id) 
        => Id = id;
}