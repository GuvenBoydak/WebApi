using FluentAssertions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Command.UpdateGenre;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.GenreOperations.Command.UpdateGenre
{
    public class UpdateGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_SouldBeReturnErrors()
        {
            //arrange
            string name = "a";
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.Model = new UpdateGenreViewModel()
            {
                Name = name,
                IsActive = true
            };

            //act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            ValidationResult result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            string name = "test";
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.Model = new UpdateGenreViewModel()
            {
                IsActive = true,
                Name = name
            };

            //act 
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            ValidationResult result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
