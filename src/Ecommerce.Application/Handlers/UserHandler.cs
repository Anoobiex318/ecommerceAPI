using Ecommerce.Application.Commands;
using Ecommerce.Application.Queries;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interface;
using Ecommerce.Domain.Models;
using MediatR;
using AutoMapper;

namespace Ecommerce.Application.Handlers
{
    public class UserHandler
    {
        public class AddUserCommandHandler : IRequestHandler<UserCommand.AddUserCommand, UserModel>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public AddUserCommandHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<UserModel> Handle(UserCommand.AddUserCommand command, CancellationToken cancellationToken)
            {
                var userEntity = _mapper.Map<UserEntity>(command);
                var userModel = await _userRepository.AddUser(userEntity);
                var createdUser = _mapper.Map<UserModel>(userModel);
                return createdUser;
            }
        }

        public class GetUserByIdQueryHandler : IRequestHandler<UserQuery.GetUserByIdQuery, UserModel>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<UserModel> Handle(UserQuery.GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                var userEntity = await _userRepository.GetUserById(request.UserId);
                var userModel = _mapper.Map<UserModel>(userEntity);
                return userModel;
            }
        }
    }
}
