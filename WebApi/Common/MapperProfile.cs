
using AutoMapper;
using WebApi.Application.AuthorOperations.Command.CreateAuthorviewmodel;
using WebApi.Application.AuthorOperations.Command.UpdateAuthor;
using WebApi.Application.AuthorOperations.Query.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Query.GetAuthors;
using WebApi.Application.BookOperations.CreateBook;
using WebApi.Application.BookOperations.GetBooksById;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.UserOperations.Command.CreateUser;
using WebApi.Entities;
using static WebApi.Application.BookOperations.GetBooks.GetBooksQuery;


namespace WebApi.Common
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateBookModal,Book>();
            CreateMap<Book, GetBookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Book,BookViewModel>().ForMember(dest=>dest.Genre,opt=>opt.MapFrom(src=> src.Genre.Name));

            CreateMap<Genre, GenreViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();

            CreateMap<Author, GetAuthorViewModel>();
            CreateMap<Author, GetAuthorDetailViewModel>();
            CreateMap<CreateAuthorViewModel, Author>();

            CreateMap<CreateUserModel, User>();
        }
    }
}