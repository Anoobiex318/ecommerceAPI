using Ecommerce.Domain.Models;
using MediatR;

namespace Ecommerce.Application.Commands
{
    public class UserCommand
    {
        public class AddUserCommand : IRequest<UserModel>
        {

            public string UserName { get; set; }
        }
    }
}
