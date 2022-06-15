using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DbOperations;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.GenreOperations.Query.GenreDetail
{
    public class GetGenreDetailQueryTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;


        public GetGenreDetailQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Theory]
        [InlineData(5)]
        [InlineData(8)]
        public void WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeReturnError(int genreId)
        {
            //arrange
            GetGenreDetailQuery query = new GetGenreDetailQuery(_mapper,_context);
            query.GenreId = genreId;

            //act - assert
            FluentActions
               .Invoking(() => query.Handler())
               .Should().Throw<InvalidOperationException>()
               .And.Message.Should().Be("Kitap Türü Bulunamadı.");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void WhenValidGenreIdIsGiven_InvalidOperationException_ShouldNotBeReturnError(int genreId)
        {
            //arrange
            GetGenreDetailQuery query = new GetGenreDetailQuery(_mapper,_context);
            query.GenreId=genreId;

            //act assert
            FluentActions.Invoking(() => query.Handler()).Invoke();

        }

    }
}
