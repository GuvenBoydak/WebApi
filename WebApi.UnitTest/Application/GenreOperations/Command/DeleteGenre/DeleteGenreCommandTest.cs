using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Command.DeleteGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.GenreOperations.Command.DeleteGenre
{
    public class DeleteGenreCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        
        public DeleteGenreCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        public void WhenTheGenreToDeleteIsNotFound_InvalidOperationException_ShoudBeReturn(int genreId)
        {
            //arrnge
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = genreId;

            //act -  assert
            FluentActions.Invoking(() => command.Handler()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadı.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeDeleted()
        {
            //arrange
            int genreId = 1;
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = genreId;

            //act
            FluentActions.Invoking(() => command.Handler()).Invoke();

            //assert
            Genre genre = _context.Genres.SingleOrDefault(x => x.Id == genreId);

            genre.Should().Be(null);
        }
    }
}
