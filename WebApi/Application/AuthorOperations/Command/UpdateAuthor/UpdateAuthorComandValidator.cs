using FluentValidation;

namespace WebApi.Application.AuthorOperations.Command.UpdateAuthor
{
    public class UpdateAuthorCommandValidator:AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(x => x.Model.FirstName).NotEmpty();
            RuleFor(x => x.Model.FirstName).MinimumLength(2);
            RuleFor(x => x.Model.LastName).NotEmpty();
            RuleFor(x => x.Model.LastName).MinimumLength(2);
            RuleFor(x => x.Model.Birthday.Date).NotEmpty();
            RuleFor(x => x.Model.Birthday.Date).LessThan(DateTime.UtcNow.Date);
        }
    }
}
