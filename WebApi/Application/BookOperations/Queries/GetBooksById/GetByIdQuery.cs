using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.GetBooksById
{
    public class GetByIdQuery
    {
        public int id { get; set; }

        private readonly BookStoreDbContext _db;
        private readonly IMapper _mapper;

        public GetByIdQuery(BookStoreDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public void Handler()
        {
            var book = _db.Books.Include(x=>x.Genre).FirstOrDefault(x => x.Id == id);
            if (book is null)
                throw new InvalidOperationException("kitap bulunamadı!!!");

            GetByIdViewModel vm = _mapper.Map<GetByIdViewModel>(book);
        }


    }

    public class GetByIdViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public int PageCount { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
