using FluentAssertions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.DeleteBook;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidInputsAreGiven_Validator_SouldBeReturnErrors(int bookId)
        {
            //arrange
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.id = bookId;

            //act
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            ValidationResult result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError(int bookId)
        {
            //arrange
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.id = bookId;

            //act
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            ValidationResult result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().Be(0);
        }

    }
}
