using BookStoreWebApi.Application.GenreOperations.CreateGenre;
using BookStoreWebApi.Application.GenreOperations.DeleteGenre;
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

namespace Webapi.UnitTests.Application.BookOperations.Commands.GenreCommands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyNoExistsGenreTitleGiven_InvalidOperationException_ShouldBeReturn()
        {
            Genre genre = new() {Id=2000, Name= "WhenAlreadyNoExistsGenreTitleGiven_InvalidOperationException_ShouldBeReturn" };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            DeleteGenreCommand command = new(_context);
            command.GenreId = 404;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitab növü tapılmadı");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeDeleted()
        {
            //arrange
            Genre genreTest = new() { Id = 2001, Name = "WhenValidInputsAreGiven_Genre_ShouldBeDeleted" };
            _context.Genres.Add(genreTest);
            _context.SaveChanges();

            DeleteGenreCommand command = new(_context) { GenreId = genreTest.Id };
            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            //assert
            Genre genre = _context.Genres.SingleOrDefault(g => g.Id == genreTest.Id);
            genre.Should().BeNull();


        }
    }
}
