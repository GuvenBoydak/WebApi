using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.GetBooksById
{
    public class GetBookDetailQuery
    {
        public int id { get; set; }
        public GetBookDetailViewModel modal { get; set; }

        private readonly IBookStoreDbContext _db;
        private readonly IMapper _mapper;

        public GetBookDetailQuery(IBookStoreDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public GetBookDetailViewModel Handler()
        {
            var book = _db.Books.Include(x=>x.Genre).FirstOrDefault(x => x.Id == id);
            if (book is null)
                throw new InvalidOperationException("kitap bulunamadı!!!");

            GetBookDetailViewModel vm = _mapper.Map<GetBookDetailViewModel>(book);
            return vm;
        }


    }

    public class GetBookDetailViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public int PageCount { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
