using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.UserOperations.Command.UpdateUser;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.UserOperations.Command.UpdateUser
{
    public class UpdateUserCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _db;

        public UpdateUserCommandTest(CommonTestFixture testFixture)
        {
            _db = testFixture.Context;
        }

        [Fact]
        public void WhenTheUserToUpdateIsNotFound_InvalidOperationException_ShoudBeReturn()
        {
            //arrange
            int id = 5;
            UpdateUserCommand command = new UpdateUserCommand(_db);
            command.UserId = 5;

            //act assert
            FluentActions.Invoking(() => command.Handler()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kulanıcı Bulunamadı");
        }

        [Fact]
        public void WhenValidInputsAreGiven_User_ShouldBeUpdated()
        {
            //arrange
            int id = 1;
            UpdateUserCommand command = new UpdateUserCommand(_db);
            UpdateUserViewModel vm = new UpdateUserViewModel()
            {
                Name = "test",
                Surname = "test",
                Email = "test@gmail.com",
                Password = "123"
            };

            command.UserId = id;
            command.Model = vm;

            //act
            FluentActions.Invoking(() => command.Handler()).Invoke();

            //assert
            User user = _db.Users.SingleOrDefault(x=>x.Id==id);

            user.Should().NotBeNull();
            user.Name.Should().Be(vm.Name);
            user.Surname.Should().Be(vm.Surname);
            user.Email.Should().Be(vm.Email);
            user.Password.Should().Be(vm.Password);


        }
    }
}
