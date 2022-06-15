using FluentAssertions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.AuthorOperations.Command.CreateAuthor;
using WebApi.Application.AuthorOperations.Command.CreateAuthorviewmodel;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.AuthorOperations.Command.CreateAuthor
{
    public class CreateAuthorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData("a", "")]
        [InlineData("", "l")]
        [InlineData("", "")]
        public void WhenInvalidInputsAreGiven_Validator_SouldBeReturnErrors(string firstName, string lastName)
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorViewModel
            {
                FirstName = firstName,
                LastName = lastName
            };

            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            ValidationResult result = validator.Validate(command);

            //arrange
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsgiven_Validator_ShouldBeReturnError()
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorViewModel()
            {
                FirstName = "test",
                LastName = "test",
                Birthday = DateTime.UtcNow.Date
            };

            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            ValidationResult result = validator.Validate(command);

            //arrange
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorViewModel()
            {
                FirstName = "test",
                LastName = "test",
                Birthday = DateTime.UtcNow.Date.AddYears(-10)
            };

            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            ValidationResult result = validator.Validate(command);

            //arrange
            result.Errors.Count.Should().Be(0);
        }
    }
}