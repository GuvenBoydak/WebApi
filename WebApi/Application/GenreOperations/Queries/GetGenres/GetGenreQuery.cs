using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenreQuery
    {
        private readonly IMapper _mapper;
        private readonly IBookStoreDbContext _db;

        public GetGenreQuery(IMapper mapper, IBookStoreDbContext db)
        {
            _mapper = mapper;
            _db = db;
        }

        public List<GenreViewModel> Handler()
        {
            List<Genre> genres = _db.Genres.Where(x => x.IsActive == true).OrderBy(x=>x.Id).ToList();

            List<GenreViewModel> vm=_mapper.Map<List<GenreViewModel>>(genres);
            return vm;

        }


    }

    public class GenreViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

}
