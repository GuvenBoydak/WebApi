using FluentAssertions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.AuthorOperations.Command.DeleteAuthor;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.AuthorOperations.Command.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTest:IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData(0)]
        public void WhenInvalidInputsAreGiven_Validator_SouldBeReturnErrors(int id)
        {
            //arrange
            DeleteAuthorCommand command = new DeleteAuthorCommand(null);
            command.AuthorId = id;

            //act
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            ValidationResult result= validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError(int id)
        {
            //arrange
            DeleteAuthorCommand command = new DeleteAuthorCommand(null);
            command.AuthorId = id;

            //act
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            ValidationResult result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
