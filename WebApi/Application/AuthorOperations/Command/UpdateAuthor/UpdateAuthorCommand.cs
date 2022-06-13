using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Command.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public UpdateAuthorViewModel Model { get; set; }

        public int AuthorId { get; set; }

        private readonly BookStoreDbContext _db;

        public UpdateAuthorCommand(BookStoreDbContext db)
        {
            _db = db;
        }

        public void Handler()
        {
            Author author = _db.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Güncellenecek Yazar Bulunamadı!!!");

            author.FirstName = Model.FirstName != default ? Model.FirstName : author.FirstName;
            author.LastName = Model.LastName != default ? Model.LastName : author.LastName;
            author.Birthday = Model.Birthday != default ? Model.Birthday : author.Birthday;

            _db.SaveChanges();
        }
    }

    public class UpdateAuthorViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthday { get; set; }
    }
}
