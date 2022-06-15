using WebApi.DbOperations;


namespace WebApi.Application.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {

        public int id { get; set; }

        private readonly IBookStoreDbContext _db;

        public DeleteBookCommand(IBookStoreDbContext db)
        {
            _db = db;
        }

        public void Handler()
        {
            var getBook = _db.Books.SingleOrDefault(x=>x.Id==id);
            if (getBook is null)
                throw new InvalidOperationException("Silinicek Kitap bulunamadı");

            _db.Books.Remove(getBook);
            _db.SaveChanges();
        }
    }
}
