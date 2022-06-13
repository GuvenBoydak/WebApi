using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperations.Command.CreateAuthor;
using WebApi.Application.AuthorOperations.Command.CreateAuthorviewmodel;
using WebApi.Application.AuthorOperations.Command.DeleteAuthor;
using WebApi.Application.AuthorOperations.Command.UpdateAuthor;
using WebApi.Application.AuthorOperations.Query.GetAuthorDetail;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {

        private readonly BookStoreDbContext _db;
        private readonly IMapper _mapper;

        public AuthorController(BookStoreDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAuthor()
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_db, _mapper);

            List<Author> result = _db.Authors.OrderBy(x => x.Id).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthorDetail(int id)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_db, _mapper);
            query.AuthorId = id;

            Author result = _db.Authors.SingleOrDefault(x => x.Id == id);

            GetAuthorDetailValidator validator = new GetAuthorDetailValidator();
            validator.ValidateAndThrow(query);

            query.Handler();
            return Ok();
        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorViewModel model)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_db,_mapper);
            command.Model = model;

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handler();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorViewModel model)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_db);
            command.AuthorId = id;
            command.Model = model;

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handler();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_db);
            command.AuthorId = id;

            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handler();

            return Ok();
        }

    }
}
