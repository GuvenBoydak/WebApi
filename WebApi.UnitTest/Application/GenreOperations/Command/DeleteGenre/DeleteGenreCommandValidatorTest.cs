using FluentAssertions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Command.DeleteGenre;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.GenreOperations.Command.DeleteGenre
{
    public class DeleteGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidInputsAreGiven_Validator_SouldBeReturnErrors(int genreId)
        {
            //arrange
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = genreId;


            //act
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            ValidationResult result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            int genreId = 1;
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = genreId;


            //act
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            ValidationResult result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }

    }
}
