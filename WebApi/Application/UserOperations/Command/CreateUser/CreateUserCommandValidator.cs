using FluentValidation;

namespace WebApi.Application.UserOperations.Command.CreateUser
{
    public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty();
            RuleFor(x => x.Model.Surname).NotEmpty();
            RuleFor(x => x.Model.Email).NotEmpty();
            RuleFor(x => x.Model.Email).EmailAddress();
            RuleFor(x => x.Model.Password).NotEmpty();
            RuleFor(x => x.Model.Password).MinimumLength(5);
            
            


        }
    }
}
