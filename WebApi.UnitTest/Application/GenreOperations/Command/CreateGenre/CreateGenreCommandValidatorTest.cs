using FluentAssertions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Command.CreateGenre;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.GenreOperations.Command.CreateGenre
{
    public class CreateGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("")]
        [InlineData("t")]
        public void WhenInvalidInputsAreGiven_Validator_SouldBeReturnErrors(string name)
        {
            //arrange
            CreateGenreCommand command = new CreateGenreCommand(null);
            command.Modal = new CreateGenreViewModel()
            {
                Name = name
            };


            //act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            ValidationResult result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            CreateGenreCommand command = new CreateGenreCommand(null);
            command.Modal = new CreateGenreViewModel()
            {
                Name = "test"
            };


            //act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            ValidationResult result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
