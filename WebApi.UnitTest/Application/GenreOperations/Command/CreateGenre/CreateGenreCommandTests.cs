using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Command.CreateGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.GenreOperations.Command.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistBookGenreIsGiven_InvalidOperationException_ShoudBeReturn()
        {
            //arrange
            Genre genre = new Genre()
            {
                IsActive = false,
                Name = "WhenAlreadyExistBookGenreTitleIsGiven_InvalidOperationException_ShoudBeReturn"
            };

            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Modal = new CreateGenreViewModel() { Name = genre.Name };


            //act - assert 
            FluentActions.Invoking(() => command.Handler()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Mevcut");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            //arrange
            CreateGenreCommand command = new CreateGenreCommand(_context);
            CreateGenreViewModel vm = new CreateGenreViewModel()
            {
                Name = "test"
            };

            command.Modal = vm;

            //act
            FluentActions.Invoking(() => command.Handler()).Invoke();

            //assert
            var genre = _context.Genres.SingleOrDefault(x => x.Name == vm.Name);

            genre.Name.Should().NotBeNull();
            genre.Name.Should().Be(vm.Name);

        }
    }
}
