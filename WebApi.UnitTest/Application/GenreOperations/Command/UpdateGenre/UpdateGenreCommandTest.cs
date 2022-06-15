using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Command.UpdateGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.GenreOperations.Command.UpdateGenre
{
    public class UpdateGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
  

        public UpdateGenreCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
           
        }
        [Fact]
        public void WhenTheGenreToUpdateIsNotFound_InvalidOperationException_ShoudBeReturn()
        {
            //arrange
            int genreId = 0;

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.genreId = genreId;

            //act //assert
            FluentActions.Invoking(() => command.Handler()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek Kitap Türü Bulunamadi");           
        }

        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange 
            int genreId = 1;
            string name = "Romance";

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.genreId = genreId;
            command.Model = new UpdateGenreViewModel() { Name = name };

            //act //assert
            FluentActions.Invoking(() => command.Handler()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı kitap Türü Mevcut");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
        {
            //arrange
            int genreId = 1;

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            UpdateGenreViewModel vm = new UpdateGenreViewModel()
            {
                Name = "adventure",
                IsActive = true
            };

            command.genreId = genreId;
            command.Model = vm;

            //act
            FluentActions.Invoking(() => command.Handler()).Invoke();

            //assert
            Genre genre = _context.Genres.SingleOrDefault(x => x.Id == genreId);

            genre.Name.Should().Be(vm.Name);
            genre.IsActive.Should().Be(vm.IsActive);

            



        }



    }
}
