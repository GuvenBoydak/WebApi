using FluentAssertions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.AuthorOperations.Query.GetAuthorDetail;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.AuthorOperations.Query.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidatorTest:IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void WhenInvalidInputsAreGiven_Validator_SouldBeReturnErrors(int Id)
        {
            //arrange
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null,null);
            query.AuthorId = Id;

            //act
            GetAuthorDetailValidator validator = new GetAuthorDetailValidator();
            ValidationResult result = validator.Validate(query);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError(int Id)
        {
            //arrange
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null, null);
            query.AuthorId = Id;

            //act
            GetAuthorDetailValidator validator = new GetAuthorDetailValidator();
            ValidationResult result = validator.Validate(query);

            //assert
            result.Errors.Count.Should().Be(0);
        }

    }
}
