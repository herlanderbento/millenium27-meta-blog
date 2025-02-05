using M27.MetaBlog.Application.UseCases.User.Common;
using MediatR;

namespace M27.MetaBlog.Application.UseCases.User.CreateUser;

public interface ICreateUser: IRequestHandler<CreateUserInput, UserOutput>
{
    
}