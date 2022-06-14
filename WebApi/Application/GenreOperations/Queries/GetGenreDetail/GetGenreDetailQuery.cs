using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail

{
    public class GetGenreDetailQuery
    {
        public int GenreId { get; set; }

        private readonly IMapper _mapper;
        private readonly IBookStoreDbContext _db;

        public GetGenreDetailQuery(IMapper mapper, IBookStoreDbContext db)
        {
            _mapper = mapper;
            _db = db;
        }

        public GenreDetailViewModel Handler()
        {
            Genre genreDetail=_db.Genres.SingleOrDefault(x=>x.Id==GenreId && x.IsActive == true);
            if (genreDetail is null)
                throw new InvalidOperationException("Kitap Türü Bulunamadı.");

            return _mapper.Map<GenreDetailViewModel>(genreDetail);
            
        }

    }

    public class GenreDetailViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
