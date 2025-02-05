using M27.MetaBlog.Application.UseCases.User.Common;
using MediatR;

namespace M27.MetaBlog.Application.UseCases.User.GetUser;

public class GetUserInput: IRequest<UserOutput>
{
    public Guid Id { get; set; }
    public GetUserInput(Guid id) => Id = id;
}