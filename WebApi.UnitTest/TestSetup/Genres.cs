using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTest.TestSetup
{
    public static class Genres
    {
        public static void AddUsers(this BookStoreDbContext context)
        {

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
        }
    }
}
