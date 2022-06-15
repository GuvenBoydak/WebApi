using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.AuthorOperations.Command.CreateAuthorviewmodel;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.AuthorOperations.Command.CreateAuthor
{
    public class CreateAuthorCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistAuthorFistNameAndLastNameIsGiven_InvalidOperationException_ShoudBeReturn()
        {
            //arrange
            
            Author author = new Author()
            {
                FirstName = "test",
                LastName = "test",
                Birthday = DateTime.Now.Date.AddYears(-2),
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorViewModel()
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                Birthday = author.Birthday,
            };

            //act assert
            FluentActions.Invoking(() => command.Handler()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Bu İsim Soyisimde Yazar Mevcut!!");
        }


        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            CreateAuthorViewModel modal= new CreateAuthorViewModel()
            {
                FirstName = "test",
                LastName = "test",
                Birthday = DateTime.Now.Date.AddYears(-2),
            };
            command.Model = modal;

            //act
            FluentActions.Invoking(() => command.Handler()).Invoke();

            //arrange
            Author author = _context.Authors.SingleOrDefault(x => x.FirstName + x.LastName == modal.FirstName + modal.FirstName);

            author.Should().NotBeNull();
            author.FirstName.Should().Be(modal.FirstName);
            author.LastName.Should().Be(modal.LastName);
            author.Birthday.Should().Be(modal.Birthday);

            

        }
    }
}
