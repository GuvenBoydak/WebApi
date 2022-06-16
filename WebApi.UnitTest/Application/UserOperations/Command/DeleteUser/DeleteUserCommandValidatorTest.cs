using FluentAssertions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.DeleteBook;
using WebApi.Application.UserOperations.Command.DeleteUser;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.UserOperations.Command.DeleteUser
{
    public class DeleteUserCommandValidatorTest : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData(0)]
        public void WhenInvalidInputsAreGiven_Validator_SouldBeReturnErrors(int id)
        {
            //arrange
            DeleteUserCommand command = new DeleteUserCommand(null);
            command.UserId = id;

            //act
            DeleteUserCommandValidator validator = new DeleteUserCommandValidator();
            ValidationResult result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError(int id)
        {
            //arrange
            DeleteUserCommand command = new DeleteUserCommand(null);
            command.UserId = id;

            //act
            DeleteUserCommandValidator validator = new DeleteUserCommandValidator();
            ValidationResult result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().Be(0);
        }


    }
}
