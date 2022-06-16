using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Command.RefleshToken
{
    public class RefreshTokenCommand
    {
        public string RefreshToken { get; set; }
        private readonly IBookStoreDbContext _db;
        readonly IConfiguration _config;

        public RefreshTokenCommand(IBookStoreDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public Token Handler()
        {
            User user = _db.Users.FirstOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);
            if (user is not null)
            {
                TokenHandler handler = new TokenHandler(_config);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);

                return token;
            }
            else
                throw new InvalidOperationException("Valid bir Refresh Token Bulunamadı");
        }
    }
}
