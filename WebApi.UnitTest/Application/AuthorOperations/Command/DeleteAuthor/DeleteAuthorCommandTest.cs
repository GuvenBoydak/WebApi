using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.AuthorOperations.Command.DeleteAuthor;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.AuthorOperations.Command.DeleteAuthor
{
    public class DeleteAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteAuthorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenTheAuthoreIsNotFound_InvalidOperationException_ShoudBeReturn()
        {
            //arrange
            int id = 6;
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = id;

            //act assert
            FluentActions.Invoking(() => command.Handler()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Bulunamadı!!!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeDeleted()
        {
            //arrange
            int id = 1;
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = id;

            //act 
            FluentActions.Invoking(() => command.Handler()).Invoke();

            //assert
            Author author = _context.Authors.SingleOrDefault(x => x.Id == id);

            author.Should().Be(null);
        }

    }

}
