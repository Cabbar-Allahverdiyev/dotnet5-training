using System;
using System.Linq;
using AutoMapper;
using BookStoreWebApi.Application.BookOperations.CreateBook;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistBookTitleGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange(Hazirliq)
            Book book = new Book() { Id = 1, Title = "wWhenAlreadyExistBookTitleGiven_InvalidOperationException_ShouldBeReturn", PageCount = 100, PublishDate = new DateTime(1990, 1, 22), GenreId = 1 };
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel() { Title = book.Title };

            //act(Ise Salma) & assert(Dogrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitab movcuddur");


        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_shouldBeCreated()
        {
            //arrenge
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            CreateBookModel model = new CreateBookModel()
            {
                Title = "lLord Of The Rings",
                PageCount = 1000,
                PublishDate = new DateTime(1990, 01, 20),
                GenreId = 1
            };
            command.Model = model;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var book = _context.Books.SingleOrDefault(b => b.Title == model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);


        }
    }
}