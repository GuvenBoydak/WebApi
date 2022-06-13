using FluentValidation;
using WebApi.Application.BookOperations.GetBooksById;

namespace WebApi.Application.BookOperations.DeleteBook
{
    public class DeleteBookCommandValidator:AbstractValidator<GetByIdQuery>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty();
            RuleFor(x => x.id).GreaterThan(0);
        }
    }
}
