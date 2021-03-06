using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.CreateBook;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.BookOperations.Command.CreateBook
{
    public class CreateBookCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _db;
        private readonly IMapper _mapper;

        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _db = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShoudBeReturn()
        {
            //arrange (Hazırlık)
            Book book = new Book() { Title = "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShoudBeReturn", PageCount = 1000, PublishDate = new DateTime(1990, 01, 10), GenreId = 1 };
            _db.Books.Add(book);
            _db.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_db,_mapper);
            command.model = new CreateBookModal() { Title = book.Title };

            //act - assert (Çalıştırma - Dogrulama)

            FluentActions
                .Invoking(() => command.Handler())
                .Should()
                .Throw<InvalidOperationException>().And.Message
                .Should()
                .Be("Kitap zaten Mevcut.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            //arrange 
            CreateBookCommand command = new CreateBookCommand(_db, _mapper);
            CreateBookModal model = new CreateBookModal()
            {
                Title = "Hobbit",
                PageCount = 1000,
                PublishDate = DateTime.Now.AddYears(-3),
                GenreId = 1
            };
            command.model = model;

            //Act
            FluentActions.Invoking(() => command.Handler()).Invoke();

            //assert
            var book = _db.Books.SingleOrDefault(x => x.Title == model.Title);

            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
        }
    }
}
