using FluentAssertions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.UpdateBook;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.BookOperations.Command.UpdateBook
{
    public class UpdateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Lord Of The", 0, 0)]
        [InlineData("Lord Of The", 0, 1)]
        [InlineData("Lord Of The", 1, 0)]
        [InlineData("", 0, 0)]
        [InlineData("", 10, 0)]
        [InlineData("", 0, 1)]
        [InlineData("", 1, 1)]
        [InlineData("lor", 0, 0)]
        [InlineData("lor", 0, 1)]
        [InlineData("lor", 1, 0)]
        [InlineData("lor", 1, 1)]
        [InlineData("lord", 0, 0)]
        [InlineData("lord", 1, 0)]
        [InlineData("lord", 0, 1)]
        public void WhenInvalidInputsAreGiven_Validator_SouldBeReturnErrors(string title, int pageCount, int genreId)
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.modal = new UpdateBookModal()
            {
                GenreId = genreId,
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddMonths(-5)
            };

            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            ValidationResult result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenDateTimeEqualNowIsgiven_Validator_ShouldBeReturnError()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.modal = new UpdateBookModal()
            {
                GenreId = 1,
                Title = "Hobbit",
                PageCount = 1000,
                PublishDate = DateTime.UtcNow.Date,
            };

            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            ValidationResult result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.modal = new UpdateBookModal()
            {
                GenreId = 1,
                Title = "Hobbit",
                PageCount = 1000,
                PublishDate = DateTime.Now.Date.AddDays(-2),
            };

            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            ValidationResult result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
