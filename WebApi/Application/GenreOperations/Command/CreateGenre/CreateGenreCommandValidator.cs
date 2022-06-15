using FluentValidation;

namespace WebApi.Application.GenreOperations.Command.CreateGenre
{
    public class CreateGenreCommandValidator:AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(x => x.Modal.Name).NotEmpty();
            RuleFor(x => x.Modal.Name).MinimumLength(2);
        }
    }
}
