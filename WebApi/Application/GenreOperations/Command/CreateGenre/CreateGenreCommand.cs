using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Command.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreViewModel Modal { get; set; }

        private readonly IBookStoreDbContext _db;

        public CreateGenreCommand(IBookStoreDbContext db)
        {
            _db = db;
        }


        public void Handler()
        {
            Genre genre = _db.Genres.SingleOrDefault(x => x.Name == Modal.Name);
            if (genre is not null)
                throw new InvalidOperationException("Kitap Türü Mevcut");

            genre = new Genre();
            genre.Name = Modal.Name;
            _db.Genres.Add(genre);
            _db.SaveChanges();
            
        }
    }

    public class CreateGenreViewModel
    {
        public string Name { get; set; }
    }
}
