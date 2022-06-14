using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        public UpdateBookModal modal { get; set; }
        public int id { get; set; }

        
        private readonly IBookStoreDbContext _db;

        public UpdateBookCommand(IBookStoreDbContext db)
        {
            _db = db;
        }


        public void Handler()
        {
            var getBook=_db.Books.SingleOrDefault(x=>x.Id==id);
             if(getBook is null)
                throw new InvalidOperationException("Güncellenecek Kitap bulunamadı!");

            getBook.PageCount = modal.PageCount != default ? modal.PageCount : getBook.PageCount;
            getBook.PublishDate = modal.PublishDate != default ? modal.PublishDate : getBook.PublishDate;
            getBook.Title = modal.Title != default ? modal.Title : getBook.Title;
            _db.SaveChanges();  

        }
    }

    public class UpdateBookModal
    {

        public string Title { get; set; }

        public int PageCount { get; set; }

        public DateTime PublishDate { get; set; }

        public int GenreId { get; set; }

    }

}