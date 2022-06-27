using AutoMapper;
using BookStoreWebApi.Application.AuthorOperations.Commands.CreateAuthors;
using BookStoreWebApi.Application.AuthorOperations.GetAuthorDetail;
using BookStoreWebApi.Application.AuthorOperations.GetAuthors;
using BookStoreWebApi.Application.BookOperations.CreateBook;
using BookStoreWebApi.Application.BookOperations.GetBookDetail;
using BookStoreWebApi.Application.BookOperations.GetBooks;
using BookStoreWebApi.Application.GenreOperations.GetGenreDetail;
using BookStoreWebApi.Application.GenreOperations.GetGenres;
using BookStoreWebApi.Application.UserOperations.Commands.Create;
using BookStoreWebApi.Entities;

namespace BookStoreWebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.FirstName +src.Author.LastName));

            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)); 
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.FirstName+" "+src.Author.LastName)); 
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<CreateUserModel, User>();
            CreateMap<CreateAuthorModel, Author>();
            CreateMap<Author, AuthorDetailViewModel>();
            CreateMap<Author, AuthorsViewModel>();
        }
    }

}