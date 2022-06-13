using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel model { get; set; }


        private readonly BookStoreDbContext _db;
        private readonly IMapper _mapper;

        public CreateBookCommand(BookStoreDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public void Handler()
        {
            var book= _db.Books.SingleOrDefault(x=>x.Title==model.Title);
            if(book is not null)
                throw new InvalidOperationException("Kitap zaten Mevcut");

            book = _mapper.Map<Book>(model);

            _db.Books.Add(book);
            _db.SaveChanges();

        }
    }

    public class CreateBookModel
    {
        public string Title { get; set; }

        public int GenreId { get; set; }
        
        public int PageCount { get; set; }

        public DateTime  PublishDate { get; set; }
    }
}