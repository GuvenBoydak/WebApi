using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Command.CreateAuthorviewmodel
{
    public class CreateAuthorCommand
    {
        public CreateAuthorViewModel Model { get; set; }

        private readonly IBookStoreDbContext _db;
        private readonly IMapper _mapper;

        public CreateAuthorCommand(IBookStoreDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public void Handler()
        {
            Author author=_db.Authors.SingleOrDefault(x=>x.FirstName + x.LastName == Model.FirstName + Model.FirstName);
            if (author is not null)
                throw new InvalidOperationException("Bu İsim Soyisimde Yazar Mevcut!!");

             author = _mapper.Map<Author>(Model);
            _db.Authors.Add(author);
            _db.SaveChanges();
        }
    }

    public class CreateAuthorViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthday { get; set; }
    }
}
