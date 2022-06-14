using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.GetBooksById;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTest.TestSetup;

namespace WebApi.UnitTest.Application.BookOperations.Query.GetBookDetail
{
    public class GetBookDetailQueryTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }


        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(10)]
        public void  WhenInvalidBookIdIsGiven_InvalidOperationException_ShouldBeReturnError(int bookId)
        {
            //arrange
            GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
            query.id = bookId;


            //Act - Assert
            FluentActions
                .Invoking(() => query.Handler()).Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("kitap bulunamadı!!!");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void WhenValidBookIdIsGiven_InvalidOperationException_ShouldNotBeReturnError(int bookId)
        {
            //arrange
            GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
            query.id = bookId;

            GetBookDetailViewModel vm = new GetBookDetailViewModel();
            //Act -//Assert
            FluentActions.Invoking(() => query.Handler()).Invoke();
        }
    }
}
