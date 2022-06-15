using FluentAssertions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.AuthorOperations.Command.UpdateAuthor;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.AuthorOperations.Command.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTest:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("","")]
        [InlineData("","t")]
        [InlineData("t","")]
        
        public void WhenInvalidInputsAreGiven_Validator_SouldBeReturnErrors(string firtName, string lastName)
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorViewModel()
            {
                FirstName = firtName,
                LastName = lastName
            };

            //act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            ValidationResult result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsgiven_Validator_ShouldBeReturnError()
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorViewModel()
            {
                FirstName = "test",
                LastName = "test",
                Birthday = DateTime.UtcNow.Date
            };

            //act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            ValidationResult result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorViewModel()
            {
                FirstName = "test",
                LastName = "test",
                Birthday = DateTime.UtcNow.Date.AddYears(-11)
            };

            //act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            ValidationResult result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
