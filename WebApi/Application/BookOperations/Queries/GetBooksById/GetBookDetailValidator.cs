using FluentValidation;

namespace WebApi.Application.BookOperations.GetBooksById
{
    public class GetBookDetailValidator:AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailValidator()
        {
            RuleFor(x => x.id).GreaterThan(0);
        }
    }
}
