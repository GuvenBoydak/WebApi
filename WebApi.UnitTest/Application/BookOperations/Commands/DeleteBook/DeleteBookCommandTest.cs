using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.DeleteBook;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenTheBookToDeleteIsNotFound_InvalidOperationException_ShoudBeReturn()
        {
            //arrange 
            int bookId = 5;

            
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.id = bookId;

            //Act //assert
            FluentActions
               .Invoking(() => command.Handler())
               .Should()
               .Throw<InvalidOperationException>().And.Message
               .Should()
               .Be("Silinicek Kitap bulunamadı");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeDeleted()
        {
            //arrange
            int bookId = 1;

            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.id = bookId;

            //act
            FluentActions.Invoking(() => command.Handler()).Invoke();

            //assert
            Book book = _context.Books.SingleOrDefault(x => x.Id == bookId);

            book.Should().Be(null);
        }

    }
}
