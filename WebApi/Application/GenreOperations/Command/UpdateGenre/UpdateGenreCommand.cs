using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Command.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int genreId { get; set; }

        public UpdateGenreViewModel Model { get; set; }

        private readonly BookStoreDbContext _db;

        public UpdateGenreCommand( BookStoreDbContext db)
        {
            _db = db;
        }

        public void Handler()
        {
            Genre genre = _db.Genres.SingleOrDefault(x => x.Id == genreId);
            if (genre is null)
                throw new InvalidOperationException("Güncellenecek Kitap Türü Bulunamadi");

            if (_db.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != genreId))
                throw new InvalidOperationException("Aynı kitap Türü Mevcut");

            genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
            genre.IsActive=Model.IsActive;

            _db.SaveChanges();

        }


    }
    public class UpdateGenreViewModel
    {
        public string Name { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
