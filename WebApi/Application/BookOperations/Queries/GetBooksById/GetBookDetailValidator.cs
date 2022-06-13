using FluentValidation;
using WebApi.Application.BookOperations.GetBooksById;

namespace WebApi.Application.BookOperations.GetBooksById
{
    public class GetBookDetailValidator:AbstractValidator<GetByIdQuery>
    {
        public GetBookDetailValidator()
        {
            RuleFor(x => x.id).GreaterThan(0);
        }
    }
}
