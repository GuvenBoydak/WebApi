using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.UserOperations.Command.CreateUser;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.UserOperations.Command.CreateUser
{
    public class CreateUserCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _db;
        private readonly IMapper _mapper;

        public CreateUserCommandTest(CommonTestFixture testFixture)
        {
            _db = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistUserTitleIsGiven_InvalidOperationException_ShoudBeReturn()
        {
            //arrange
            User user = new User()
            {
                Name = "test",
                Surname = "test",
                Email = "test@mail.test",
                Password = "test",
                RefreshToken = "asda-dasd-1233",
                RefreshTokenExpireDate = DateTime.Now.AddMinutes(15)
            };

            _db.Users.Add(user);
            _db.SaveChanges();

            CreateUserCommand command = new CreateUserCommand(_db,_mapper);
            command.Model = new CreateUserModel()
            {
                Email = user.Email,
            };

            //act assert
            FluentActions.Invoking(() => command.Handler()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kulanıcı Zaten Mevcut");
        }

        [Fact]
        public void WhenValidInputsAreGiven_USer_ShouldBeCreated()
        {
            //arrange
            CreateUserCommand command = new CreateUserCommand(_db,_mapper);
            CreateUserModel vm = new CreateUserModel()
            {
                Name = "test",
                Surname = "test",
                Email = "test@mail.test",
                Password = "test",
            };

            command.Model = vm;

            //act
            FluentActions.Invoking(() => command.Handler()).Invoke();

            //assert
            var user=_db.Users.FirstOrDefault(x => x.Email == vm.Email);

            user.Should().NotBeNull();
            user.Name.Should().Be(vm.Name);
            user.Surname.Should().Be(vm.Surname);
            user.Email.Should().Be(vm.Email);
            user.Password.Should().Be(vm.Password);
        }


    }
}
