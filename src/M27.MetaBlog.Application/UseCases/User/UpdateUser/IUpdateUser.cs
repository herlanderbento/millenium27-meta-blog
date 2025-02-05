using M27.MetaBlog.Application.UseCases.User.Common;
using MediatR;

namespace M27.MetaBlog.Application.UseCases.User.UpdateUser;

public interface IUpdateUser : IRequestHandler<UpdateUserInput, UserOutput>
{
    
}