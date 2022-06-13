using FluentValidation;
using WebApi.Application.BookOperations.UpdateBook;

namespace WebApi.Application.BookOperations.UpdateBook
{
    public class UpdateBookCommandValidator:AbstractValidator<UpdateBookCommand>

    {
        public UpdateBookCommandValidator()
        {
            RuleFor(x => x.id).GreaterThan(0);
            RuleFor(x => x.modal.PublishDate.Date).NotEmpty();
            RuleFor(x => x.modal.PublishDate.Date).LessThan(DateTime.Now);
            RuleFor(x=>x.modal.Title).NotEmpty();
            RuleFor(x => x.modal.Title).MinimumLength(4);
            RuleFor(x => x.modal.PageCount).GreaterThan(0);
            RuleFor(x=>x.modal.GenreId).GreaterThan(0);
        }
    }
}
