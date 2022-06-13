using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Query.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorId { get; set; }
        private readonly BookStoreDbContext _db;
        private readonly IMapper _mapper;

        public GetAuthorDetailQuery(BookStoreDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public GetAuthorDetailViewModel Handler()
        {
            Author author = _db.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Yazar Bulunamadı!!!");

            return _mapper.Map<GetAuthorDetailViewModel>(author);


        }

    }

    public class GetAuthorDetailViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthday { get; set; }
    }

}
