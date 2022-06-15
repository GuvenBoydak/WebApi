using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.CreateBook;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.BookOperations.Command.CreateBook
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Lord Of The",0,0)]
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

        public void WhenInvalidInputsAreGiven_Validator_SouldBeReturnErrors(string title,int pageCount,int genreId)
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.model = new CreateBookModal()
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.UtcNow.Date.AddYears(-1),
                GenreId = genreId
            };

            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            ValidationResult result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsgiven_Validator_ShouldBeReturnError()
        {
            //arrange (Hazırlık)
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.model = new CreateBookModal()
            {
                Title = "testi",
                PageCount = 1,
                PublishDate = DateTime.Now.Date,
                GenreId = 1
            };

            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            ValidationResult result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {  
            //arrange 
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.model = new CreateBookModal()
            {
                Title = "testi",
                PageCount = 1,
                PublishDate = DateTime.UtcNow.Date.AddYears(-2),
                GenreId = 1
            };

            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            ValidationResult result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
