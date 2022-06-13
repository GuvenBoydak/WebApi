using FluentValidation;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailValidator:AbstractValidator<GetGenreDetailQuery>
    {
        public GetGenreDetailValidator()
        {
            RuleFor(x => x.GenreId).GreaterThan(0);

        }
    }
}
