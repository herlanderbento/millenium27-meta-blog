using M27.MetaBlog.Application.UseCases.User.Common;
using MediatR;

namespace M27.MetaBlog.Application.UseCases.User.GetUser;

public interface IGetUser : IRequestHandler<GetUserInput, UserOutput>
{
    
}