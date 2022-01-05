using BookStoreWebApi.Application.GenreOperations.UpdateGenre;
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

namespace Webapi.UnitTests.Application.BookOperations.Commands.GenreCommands.UpdateBook
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyNotExistGenreIdGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            Genre testGenre = new() { Id = 600, Name = "WhenAlreadyNotExistGenreIdGiven_InvalidOperationException_ShouldBeReturn" };
            _context.Genres.Add(testGenre);
            _context.SaveChanges();

            UpdateGenreCommand command = new(_context) { GenreId =404 }; //bele bir id  movcud olmamalidir!
            command.Model = new() { Name = "Testten Kecti mi" };

            //act assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Belə bir kitab növü tapılmadı");
        }

        [Fact]
        public void WhenAlreadyExistGenreIDAndGenreNameGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            Genre testGenre1 = new() { Id = 700, Name = "testten kecdi mi?" };
            _context.Genres.Add(testGenre1);
            _context.SaveChanges();

            Genre testGenre2 = new() { Id = 800, Name = "WhenAlreadyExistGenreIDAndGenreNameGiven_InvalidOperationException_ShouldBeReturn" };
            _context.Genres.Add(testGenre2);
            _context.SaveChanges();

            UpdateGenreCommand command = new(_context) ; //bele bir id  movcud olmalidir!
            command.GenreId = testGenre1.Id;
            command.Model = new() { Name =testGenre2.Name };

            //act assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Eyni adlı bir kitab növü artıq mövcuddur");
        }
    }
}
