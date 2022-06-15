using FluentAssertions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.GenreOperations.Query.GenreDetail
{
    public class GetGenreDetailQueryValidatorTest:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void WhenInvalidInputsAreGiven_Validator_SouldBeReturnErrors(int Id)
        {
            //arrange
            GetGenreDetailQuery query = new GetGenreDetailQuery(null,null);
            query.GenreId = Id;

            //act
            GetGenreDetailValidator validator = new GetGenreDetailValidator();
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
            GetGenreDetailQuery query = new GetGenreDetailQuery(null,null);
            query.GenreId = Id;

            //act
            GetGenreDetailValidator validator = new GetGenreDetailValidator();
            ValidationResult result = validator.Validate(query);

            //assert
            result.Errors.Count.Should().Be(0);


        }
    }
}
