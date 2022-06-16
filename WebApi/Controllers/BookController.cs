using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.BookOperations.CreateBook;
using WebApi.Application.BookOperations.DeleteBook;
using WebApi.Application.BookOperations.GetBooks;
using WebApi.Application.BookOperations.GetBooksById;
using WebApi.Application.BookOperations.UpdateBook;
using WebApi.DbOperations;

namespace WebApi.controllers
{
    [Authorize]
    [ApiController]
    [Route("{controller}s")]
    public class BookController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;


        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handler();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.id = id;

            GetBookDetailViewModel result;
            
            GetBookDetailValidator validator = new GetBookDetailValidator();
            validator.ValidateAndThrow(query);

            result = query.Handler();

            return Ok(result);
        }

        // [HttpGet]
        // public Book GetByIdQuery([FromQuery] string id)
        // {
        //     var book=bookList.Where(x=>x.Id==Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }


        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModal newBooks)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);

            command.model = newBooks;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handler();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModal book)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);

            command.id = id;
            command.modal = book;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handler();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command =new DeleteBookCommand(_context);

            command.id = id;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handler();

            return Ok();
        }
    }
}
