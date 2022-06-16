using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Command.CreateToken
{
    public class CreateTokenCommand
    {
        public CreateTokenModel Model { get; set; }
        private readonly IBookStoreDbContext _db;
        private readonly IMapper _mapper;
        readonly IConfiguration _config;

        public CreateTokenCommand(IBookStoreDbContext db, IConfiguration config, IMapper mapper)
        {
            _db = db;
            _config = config;
            _mapper = mapper;
        }

        public Token Handler()
        {
            User user = _db.Users.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);
            if (user is not null)
            {
                TokenHandler handler = new TokenHandler(_config);

                Token token = handler.CreateAccessToken(user);
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);

                _db.SaveChanges();
                return token;
            }
            else
                throw new InvalidOperationException("Kulanıcı Adı _ Şifre Hatalı");
        }

    }

    public class CreateTokenModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

    }
}
