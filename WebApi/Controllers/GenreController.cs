using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.GenreOperations.Command.CreateGenre;
using WebApi.Application.GenreOperations.Command.DeleteGenre;
using WebApi.Application.GenreOperations.Command.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IBookStoreDbContext _db;
        private readonly IMapper _mapper;

        public GenreController(IBookStoreDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Getgenre()
        {
            GetGenreQuery query = new GetGenreQuery(_mapper,_db);
            List<Application.GenreOperations.Queries.GetGenres.GenreViewModel> result = query.Handler();
            return Ok(result);
            
        }

        [HttpGet("{id}")]
        public IActionResult GetGenreDetail(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_mapper, _db);
            query.GenreId = id;
            Application.GenreOperations.Queries.GetGenreDetail.GenreDetailViewModel result = query.Handler();

            GetGenreDetailValidator validator = new GetGenreDetailValidator();
            validator.ValidateAndThrow(query);


            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreViewModel model)
        {
            CreateGenreCommand command = new CreateGenreCommand(_db);
            command.Modal = model;

            CreateGenreCommandValidator validator= new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handler();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreViewModel model)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_db);
            command.genreId = id;
            command.Model = model;

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handler();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command= new DeleteGenreCommand(_db);
            command.GenreId = id;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handler();
            return Ok();
        }

    }
}
