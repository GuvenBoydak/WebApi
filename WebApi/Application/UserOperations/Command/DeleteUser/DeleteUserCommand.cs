using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.UserOperations.Command.DeleteUser
{
    public class DeleteUserCommand
    {
        public int UserId { get; set; }
        private readonly IBookStoreDbContext _db;

        public DeleteUserCommand(IBookStoreDbContext db)
        {
            _db = db;
        }


        public void Handler()
        {
            User user=_db.Users.SingleOrDefault(x => x.Id == UserId);
            if (user is null)
                throw new InvalidOperationException("Kulanıcı Bulunamadı");
            _db.Users.Remove(user);
            _db.SaveChanges();
        }
    }
}
