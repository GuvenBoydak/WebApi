using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DbOperations
{

    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                    return;

                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personel Growth"
                    }, new Genre
                    {
                        Name = "Science Fiction"
                    }, new Genre
                    {
                        Name = "Romance"
                    });

                context.Books.AddRange(
                    new Book
                    {
                        //Id = 1,
                        Title = "Lean Startup",
                        GenreId = 1,
                        PageCount = 200,
                        PublishDate = new DateTime(2001, 05, 12),
                        AuthorId = 1
                    },
                    new Book
                    {
                        //Id = 2,
                        Title = "Herland",
                        GenreId = 2,
                        PageCount = 350,
                        PublishDate = new DateTime(2011, 11, 21),
                        AuthorId = 2
                    },
                    new Book
                    {
                        // Id = 3,
                        Title = "Dune",
                        GenreId = 3,
                        PageCount = 333,
                        PublishDate = new DateTime(2002, 04, 19),
                        AuthorId = 3
                    });

                context.Authors.AddRange(
                    new Author
                    {
                        FirstName = "Eric",
                        LastName = "Ries",
                        Birthday = new DateTime(1978, 09, 22),
                    },
                    new Author
                    {
                        FirstName = "Charlotte Perkins",
                        LastName = "Gilman",
                        Birthday = new DateTime(1960, 07, 03),
                    },
                     new Author
                     {
                         FirstName = "Frank",
                         LastName = "Herbert",
                         Birthday = new DateTime(1920, 10, 08),
                     });

                context.Users.AddRange(
                  new User()
                  {
                      Name = "Güven",
                      Surname = "Boydak",
                      Email = "güven@mail.test",
                      Password = "123",
                  },
                  new User()
                  {
                      Name = "Aylin",
                      Surname = "Boydak",
                      Email = "aylin@mail.test",
                      Password = "123",
                  },
                  new User()
                  {
                      Name = "Ali",
                      Surname = "Boydak",
                      Email = "ali@mail.test",
                      Password = "123",
                  });

                context.SaveChanges();
            }
        }
    }
}