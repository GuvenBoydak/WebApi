using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTest.TestSetup
{
    public static class Users
    {
        public static void AddUser(this BookStoreDbContext context)
        {
            context.Users.AddRange(new User()
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
        }
    }
}
