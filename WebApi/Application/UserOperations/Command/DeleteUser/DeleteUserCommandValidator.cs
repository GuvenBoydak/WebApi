using FluentValidation;

namespace WebApi.Application.UserOperations.Command.DeleteUser
{
    public class DeleteUserCommandValidator:AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.UserId).GreaterThan(0);
           
        }
    }
}
