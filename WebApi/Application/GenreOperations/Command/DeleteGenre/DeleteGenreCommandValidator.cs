using FluentValidation;

namespace WebApi.Application.GenreOperations.Command.DeleteGenre
{
    public class DeleteGenreCommandValidator:AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(x => x.GenreId).GreaterThan(0);
        }
    }
}
