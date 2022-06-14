using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Command.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }

        private readonly IBookStoreDbContext _db;

        public DeleteGenreCommand(IBookStoreDbContext db)
        {
            _db = db;
        }

        public void Handler()
        {
            Genre genre = _db.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genre is null)
                throw new InvalidOperationException("Kitap Türü Bulunamadı.");

            _db.Genres.Remove(genre);
            _db.SaveChanges();
        }
    }

}
