using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Query.GetAuthors
{
    public class GetAuthorQuery
    {
        private readonly IBookStoreDbContext _db;
        private readonly IMapper _mapper;

        public GetAuthorQuery(IBookStoreDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public List<GetAuthorViewModel> Handler()
        {
            List<Author> authors = _db.Authors.OrderBy(x => x.Id).ToList();

            List<GetAuthorViewModel> vm = _mapper.Map<List<GetAuthorViewModel>>(authors);
            return vm;

        }
    }

    public class GetAuthorViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthday { get; set; }
    }
}
