using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.AuthorOperations.Command.UpdateAuthor;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.AuthorOperations.Command.UpdateAuthor
{
    public class UpdateAuthorCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }


        [Fact]
        public void WhenTheAuthorToUpdateIsNotFound_InvalidOperationException_ShoudBeReturn()
        {
            //arrange 
            int id = 5;
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = id;

            //act - assert
            FluentActions.Invoking(() => command.Handler()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Bulunamadı!!!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeUpdated()
        {

            //arrange 
            int id = 1;
            UpdateAuthorViewModel vm = new UpdateAuthorViewModel()
            {
                FirstName = "test",
                LastName = "test",
                Birthday = DateTime.Now.Date.AddYears(-1)
            };

            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = id;
            command.Model = vm;

            //act
            FluentActions.Invoking(() => command.Handler()).Invoke();

            //assert
            Author auther = _context.Authors.SingleOrDefault(x=>x.Id==id);

            auther.Should().NotBeNull();
            auther.FirstName.Should().Be(vm.FirstName);
            auther.LastName.Should().Be(vm.LastName);
            auther.Birthday.Should().Be(vm.Birthday);

        }
    }
}
