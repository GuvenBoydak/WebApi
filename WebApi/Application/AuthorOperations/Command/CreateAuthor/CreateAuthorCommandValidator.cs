using FluentValidation;
using WebApi.Application.AuthorOperations.Command.CreateAuthorviewmodel;

namespace WebApi.Application.AuthorOperations.Command.CreateAuthor
{
    public class CreateAuthorCommandValidator:AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(x => x.Model.FirstName).NotEmpty();
            RuleFor(x => x.Model.FirstName).MinimumLength(2);
            RuleFor(x => x.Model.LastName).NotEmpty();
            RuleFor(x => x.Model.LastName).MinimumLength(2);
            RuleFor(x => x.Model.Birthday.Date).NotEmpty();
            RuleFor(x => x.Model.Birthday.Date).LessThan(DateTime.UtcNow);
        }
    }
}
