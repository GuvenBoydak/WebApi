using FluentValidation;

namespace WebApi.Application.GenreOperations.Command.UpdateGenre
{
    public class UpdateGenreCommandValidator:AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(x => x.Model.Name).MinimumLength(2).When(x => x.Model.Name != String.Empty);
        }
    }
}
