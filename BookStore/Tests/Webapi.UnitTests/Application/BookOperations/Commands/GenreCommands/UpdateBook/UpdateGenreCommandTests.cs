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
            Genre testGenre = new() { Id = 3000, Name = "WhenAlreadyNotExistGenreIdGiven_InvalidOperationException_ShouldBeReturn" };
            _context.Genres.Add(testGenre);
            _context.SaveChanges();

            UpdateGenreCommand command = new(_context) { GenreId = testGenre.Id+100 }; //bele bir id  movcud olmamalidir!
            command.Model = new() { Name = "Testten Kecti mi" };

            //act assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Belə bir kitab növü tapılmadı");

           

        }
    }
}
