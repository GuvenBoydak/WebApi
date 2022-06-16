using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.UserOperations.Command.DeleteUser;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.UserOperations.Command.DeleteUser
{
    public  class DeleteUserCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _db;

        public DeleteUserCommandTest(CommonTestFixture testFixture)
        {
            _db = testFixture.Context;
        }

        [Fact]
        public void WhenTheUserToDeleteIsNotFound_InvalidOperationException_ShoudBeReturn()
        {
            //arrange
            int id = 5;
            DeleteUserCommand command = new DeleteUserCommand(_db);
            command.UserId = id;

            command.Handler();

            //act assert
            FluentActions
              .Invoking(() => command.Handler())
              .Should()
              .Throw<InvalidOperationException>().And.Message
              .Should()
              .Be("Kulanıcı Bulunamadı");
        }

        [Fact]
        public void WhenValidInputsAreGiven_User_ShouldBeDeleted()
        {
            //arrange
            int id = 1;
            DeleteUserCommand command = new DeleteUserCommand(_db);
            command.UserId = id;

            command.Handler();

            //act 
            FluentActions
              .Invoking(() => command.Handler()).Invoke();

            //assert
            User user = _db.Users.SingleOrDefault(x => x.Id == id);

            user.Should().Be(null);
        }
    }
}
