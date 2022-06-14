using FluentAssertions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.GetBooksById;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.BookOperations.Query.GetBookDetail
{
    public class GetBookDetailQueryValidator:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void WhenInvalidInputsAreGiven_Validator_SouldBeReturnErrors(int Id)
        {
            //arrange
            GetBookDetailQuery query = new GetBookDetailQuery(null,null);
            query.id = Id;

            //act
            GetBookDetailValidator validator = new GetBookDetailValidator();
            ValidationResult result = validator.Validate(query);

            //assert
            result.Errors.Count().Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError(int Id)
        {
            //arrange
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            query.id = Id;

            //act
            GetBookDetailValidator validator = new GetBookDetailValidator();
            ValidationResult result = validator.Validate(query);

            //assert
            result.Errors.Count().Should().Be(0);
        }



    }
}
