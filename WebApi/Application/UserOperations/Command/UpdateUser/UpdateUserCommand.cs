using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.UserOperations.Command.UpdateUser
{
    public  class UpdateUserCommand
    {
        public UpdateUserViewModel Model { get; set; }
        public int UserId { get; set; }
        private readonly IBookStoreDbContext _db;


        public UpdateUserCommand(IBookStoreDbContext db)
        {
            _db = db;
        }

        public void Handler()
        {
            User user = _db.Users.SingleOrDefault(x=>x.Id==UserId);
            if (user is null)
                throw new InvalidOperationException("Kulanıcı Bulunamadı");

            user.Name = Model.Name != default ? Model.Name : user.Name;
            user.Surname = Model.Surname != default ? Model.Surname : user.Surname;
            user.Email = Model.Email != default ? Model.Email : user.Email;
            user.Password = Model.Password != default ? Model.Password : user.Password;

            _db.SaveChanges();
            
        }
    }

    public class UpdateUserViewModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
