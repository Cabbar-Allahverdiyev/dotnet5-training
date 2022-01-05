using AutoMapper;
using BookStoreWebApi.Application.BookOperations.CreateBook;
using BookStoreWebApi.Application.BookOperations.DeleteBook;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSetup;
using Xunit;

namespace Webapi.UnitTests.Application.BookOperations.Commands.BookCommands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
       

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyNotExistBookTitleGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange(Hazirliq)

            DeleteBookCommand command = new(_context)
            {
                BookId = 404
            };

            //act(Ise Salma) & assert(Tesdileme)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitab tapılmadı");

        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_shouldBeDeleted()
        {
            //arrenge
            Book testBook = new Book() { Id = 100, Title = "WhenValidInputsAreGiven_Book_shouldBeDeleted", PageCount = 100, PublishDate = new DateTime(1990, 1, 22), GenreId = 1 };
            _context.Books.Add(testBook);
            _context.SaveChanges();


            DeleteBookCommand command = new(_context)
            {
                BookId = testBook.Id
            };


            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var book = _context.Books.SingleOrDefault(b => b.Id == command.BookId);
            book.Should().BeNull();

        }




    }
}
