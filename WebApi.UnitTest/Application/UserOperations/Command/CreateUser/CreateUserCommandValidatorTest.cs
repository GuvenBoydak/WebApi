using AutoMapper;
using FluentAssertions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.UserOperations.Command.CreateUser;
using WebApi.DbOperations;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.UserOperations.Command.CreateUser
{
    public class CreateUserCommandValidatorTest : IClassFixture<CommonTestFixture>
    {


        [Theory]
        [InlineData("", "", "", "1234")]
        [InlineData("", "", "", "")]
        public void WhenInvalidInputsAreGiven_Validator_SouldBeReturnErrors(string name, string surname, string email, string password)
        {
            //arrance
            CreateUserCommand command = new CreateUserCommand(null,null);
            command.Model = new CreateUserModel
            {
                Name = name,
                Surname = surname,
                Email = email,
                Password = password
            };

            //act
            CreateUserCommandValidator validator = new CreateUserCommandValidator();
            ValidationResult result = validator.Validate(command);

            //arrance
            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            CreateUserCommand command = new CreateUserCommand(null,null);
            command.Model=new CreateUserModel() { Name="test",Surname="test",Email="test@mail.com",Password="12345"};

            //act
            CreateUserCommandValidator validator = new CreateUserCommandValidator();
            ValidationResult result = new ValidationResult();


            //assert
            result.Errors.Count.Should().Be(0);
        }

    }
}
