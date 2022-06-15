using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.AuthorOperations.Query.GetAuthorDetail;
using WebApi.DbOperations;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.AuthorOperations.Query.GetAuthorDetail
{
    public class GetAuthorDetailCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        public void WhenInvalidAuthorIdIsGiven_InvalidOperationException_ShouldBeReturnError(int authorId)
        {
            //arrange
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = authorId;


            //act assert
            FluentActions.Invoking(() => query.Handler()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Bulunamadı!!!");
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void WhenValidAuthorIdIsGiven_InvalidOperationException_ShouldNotBeReturnError(int authorId)
        {
            //arrange
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = authorId;

            //act assert
            FluentActions.Invoking(() => query.Handler()).Invoke();
        }
    }
}
