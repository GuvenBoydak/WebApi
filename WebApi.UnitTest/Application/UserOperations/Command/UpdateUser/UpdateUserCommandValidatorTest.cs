using FluentAssertions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.UserOperations.Command.UpdateUser;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.UserOperations.Command.UpdateUser
{
    public  class UpdateUserCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("", "", "", "1234")]
        [InlineData("", "", "", "")]
        public void WhenInvalidInputsAreGiven_Validator_SouldBeReturnErrors(string name, string surname, string email, string password)
        {
            //arrance
            UpdateUserCommand command = new UpdateUserCommand(null);
            command.Model = new UpdateUserViewModel
            {
                Name = name,
                Surname = surname,
                Email = email,
                Password = password
            };

            //act
            UpdateUserCommandValidator validator = new UpdateUserCommandValidator();
            ValidationResult result = validator.Validate(command);

            //arrance
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            UpdateUserCommand command = new UpdateUserCommand(null);
            command.Model = new UpdateUserViewModel { Name = "test", Surname = "test", Email = "test@mail.com", Password = "12345" };

            //act
            UpdateUserCommandValidator validator = new UpdateUserCommandValidator();
            ValidationResult result = validator.Validate(command);

            //arrance
            result.Errors.Count.Should().Be(0);
        }
    }
}
