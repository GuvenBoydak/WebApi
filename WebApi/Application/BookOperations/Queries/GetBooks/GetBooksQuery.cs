using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _db;

        private readonly IMapper _mapper;
        public GetBooksQuery(BookStoreDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public List<BookViewModel> Handler()
        {

            List<Book> bookList = _db.Books.Include(x=>x.Genre).OrderBy(x=>x.Id).ToList();
            List<BookViewModel> vm = _mapper.Map<List<BookViewModel>>(bookList);
            return vm;
        }

        public class BookViewModel
        {   
            public string Title { get; set; }

            public int PageCount { get; set; }

            public string PublishDate { get; set; }

            public string Genre { get; set; }
        }

        
    }
}
