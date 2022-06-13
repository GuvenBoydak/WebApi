using FluentValidation;

namespace WebApi.Application.AuthorOperations.Query.GetAuthorDetail
{
    public class GetAuthorDetailValidator:AbstractValidator<GetAuthorDetailQuery>
    {
        public GetAuthorDetailValidator()
        {
            RuleFor(x => x.AuthorId).GreaterThan(0);
        }
    }
}
