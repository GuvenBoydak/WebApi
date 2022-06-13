using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {

        public int id { get; set; }

        private readonly BookStoreDbContext _db;

        public DeleteBookCommand(BookStoreDbContext db)
        {
            _db = db;
        }

        public void Handler()
        {
            var getBook = _db.Books.SingleOrDefault(x=>x.Id==id);
            if (getBook is null)
                throw new InvalidOperationException("Silinicek veri bulunamadı");

            _db.Books.Remove(getBook);
            _db.SaveChanges();
        }
    }
}
