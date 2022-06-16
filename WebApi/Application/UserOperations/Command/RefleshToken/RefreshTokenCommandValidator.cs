using FluentValidation;

namespace WebApi.Application.UserOperations.Command.RefleshToken
{
    public class RefreshTokenCommandValidator:AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x=>x.RefreshToken).NotEmpty();
        }
    }
}
