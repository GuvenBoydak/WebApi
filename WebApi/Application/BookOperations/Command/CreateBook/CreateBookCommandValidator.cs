using FluentValidation;


namespace WebApi.Application.BookOperations.CreateBook
{
    public class CreateBookCommandValidator:AbstractValidator<CreateBookCommand>
    {

        public CreateBookCommandValidator()
        {
            RuleFor(x => x.model.GenreId).GreaterThan(0);
            RuleFor(x => x.model.PublishDate.Date).NotEmpty();
            RuleFor(x => x.model.PublishDate.Date).LessThan(DateTime.Now);
            RuleFor(x => x.model.PageCount).GreaterThan(0);
            RuleFor(x => x.model.Title).NotEmpty();
            RuleFor(x => x.model.Title).MinimumLength(4);
        }
    }
}
