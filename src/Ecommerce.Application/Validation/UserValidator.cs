using Ecommerce.Application.Commands;
using FluentValidation;
using Ecommerce.Domain.Interface;

namespace Ecommerce.Application.Validation
{
    public class UserValidator
    {
        public class UserValidation : AbstractValidator<UserCommand.AddUserCommand>
        {
            private readonly IUserRepository _userRepository;

            public UserValidation(IUserRepository userRepository)
            {
                _userRepository = userRepository;

                RuleFor(u => u.UserName)
                    .NotEmpty().WithMessage("Username is required.")
                    .Length(4, 10).WithMessage("Username must be between 4 and 10 characters.")
                    .NotNull().WithMessage("Username cannot be null.")
                    .MustAsync(async (userName, cancellationToken) =>
                    {
                        var existingUser = await _userRepository.GetUserByUsername(userName);
                        return existingUser == null;
                    }).WithMessage("Username is already used.");
            }
        }
    }
}
