using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Command.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreViewModel Model { get; set; }

        private readonly BookStoreDbContext _db;

        public CreateGenreCommand(BookStoreDbContext db)
        {
            _db = db;
        }


        public void Handler()
        {
            Genre genre = _db.Genres.SingleOrDefault(x => x.Name == Model.Name);
            if (genre is not null)
                throw new InvalidOperationException("Kitap Türü Mevcut");

            genre = new Genre();
            genre.Name = Model.Name;
            _db.Genres.Add(genre);
            _db.SaveChanges();
            
        }
    }

    public class CreateGenreViewModel
    {
        public string Name { get; set; }
    }
}
