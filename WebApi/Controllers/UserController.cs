using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.UserOperations.Command.CreateToken;
using WebApi.Application.UserOperations.Command.CreateUser;
using WebApi.Application.UserOperations.Command.DeleteUser;
using WebApi.Application.UserOperations.Command.RefleshToken;
using WebApi.Application.UserOperations.Command.UpdateUser;
using WebApi.DbOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IBookStoreDbContext _db;
        private readonly IMapper _mapper;
        readonly IConfiguration _config;

        public UserController(IBookStoreDbContext db, IMapper mapper, IConfiguration config)
        {
            _db = db;
            _mapper = mapper;
            _config = config;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserModel newUser)
        {
            CreateUserCommand command = new CreateUserCommand(_db,_mapper);
            command.Model = newUser;

            CreateUserCommandValidator validator = new CreateUserCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handler();


            return Ok();
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_db, _config, _mapper);
            command.Model = login;

            CreateTokenCommandValidator validator = new CreateTokenCommandValidator();
            validator.ValidateAndThrow(command);

            Token token = command.Handler();

            return token;

        }
        [HttpGet("refreshToken")]
        public ActionResult<Token> RefreshToken([FromQuery] string token)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_db, _config);
            command.RefreshToken = token;

            RefreshTokenCommandValidator validator = new RefreshTokenCommandValidator();
            validator.ValidateAndThrow(command);

            Token resultToken=command.Handler();
            return resultToken;

        }

        [HttpPut]
        public IActionResult UpdateUser(int id, [FromBody] UpdateUserViewModel model)
        {
            UpdateUserCommand command = new UpdateUserCommand(_db);
            command.UserId = id;
            command.Model = model;

            UpdateUserCommandValidator validator = new UpdateUserCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handler();

            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteUser([FromBody] int id)
        {
            DeleteUserCommand command = new DeleteUserCommand(_db);
            command.UserId = id;

            DeleteUserCommandValidator validator = new DeleteUserCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handler();

            return Ok();
        }


    }
}
