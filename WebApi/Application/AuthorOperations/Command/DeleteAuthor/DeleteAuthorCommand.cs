using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Command.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }

        private readonly IBookStoreDbContext _db;

        public DeleteAuthorCommand(IBookStoreDbContext db)
        {
            _db = db;
        }

        public void Handler()
        {
            Author author = _db.Authors.SingleOrDefault(x=>x.Id==AuthorId);
            if (author is null)
                throw new InvalidOperationException("Yazar Bulunamadı!!!");

            _db.Authors.Remove(author);
            _db.SaveChanges();
        }

    }
}
