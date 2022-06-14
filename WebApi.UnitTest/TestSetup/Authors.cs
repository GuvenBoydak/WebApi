using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTest.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
                  new Author
                  {FirstName = "Eric",LastName = "Ries",Birthday = new DateTime(1978, 09, 22), },
                  new Author
                  {FirstName = "Charlotte Perkins", LastName = "Gilman",Birthday = new DateTime(1960, 07, 03), },
                   new Author
                   { FirstName = "Frank", LastName = "Herbert", Birthday = new DateTime(1920, 10, 08),});
        }
    }
}
