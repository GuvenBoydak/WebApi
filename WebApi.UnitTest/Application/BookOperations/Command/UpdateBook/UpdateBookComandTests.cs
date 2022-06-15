using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.UpdateBook;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.BookOperations.Command.UpdateBook
{
    public class UpdateBookComandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBookComandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenTheBookToUpdateIsNotFound_InvalidOperationException_ShoudBeReturn()
        {
            //arrange 
            int bookId = 5;

            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.id = bookId;

            //act //assert
            FluentActions
                .Invoking(() => command.Handler())
                .Should()
                .Throw<InvalidOperationException>().And.Message
                .Should()
                .Be("Güncellenecek Kitap bulunamadı!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {

            //arrange 
            int bookId = 1;

            UpdateBookCommand command = new UpdateBookCommand(_context);
            UpdateBookModal modal = new UpdateBookModal()
            {
                Title = "test",
                GenreId = 1,
                PageCount = 1,
                PublishDate = DateTime.Now.Date.AddYears(-10)
            };
            command.modal = modal;
            command.id = bookId;

            //act
            FluentActions.Invoking(() => command.Handler()).Invoke();

            //assert
            Book book = _context.Books.SingleOrDefault(x => x.Id == bookId);

            book.Should().NotBeNull();
            book.Title.Should().Be(modal.Title);
            book.PageCount.Should().Be(modal.PageCount);
            book.GenreId.Should().Be(modal.GenreId);

        }
    }

    
}
