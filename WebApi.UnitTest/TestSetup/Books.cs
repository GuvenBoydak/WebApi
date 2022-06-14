using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTest.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                    new Book
                    {Title = "Lean Startup", GenreId = 1,PageCount = 200,PublishDate = new DateTime(2001, 05, 12),AuthorId = 1 },
                    new Book
                    {Title = "Herland", GenreId = 2,PageCount = 350,PublishDate = new DateTime(2011, 11, 21),AuthorId = 2},
                    new Book
                    {Title = "Dune", GenreId = 3,PageCount = 333,PublishDate = new DateTime(2002, 04, 19), AuthorId = 3 });
        }
    }
}
