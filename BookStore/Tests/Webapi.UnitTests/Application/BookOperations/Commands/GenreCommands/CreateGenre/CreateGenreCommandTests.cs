using BookStoreWebApi.Application.GenreOperations.CreateGenre;
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

namespace Webapi.UnitTests.Application.BookOperations.Commands.GenreCommands
{
    public class CreateGenreCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistGenreNameGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            Genre genre = new() { Name = "WhenAlreadyExistGenreNameGiven_InvalidOperationException_ShouldBeReturn" };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new(_context);
            command.Model = new() { Name = genre.Name };

            //act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Bu kitab növü artıq mövcuddur");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            //arrange
            CreateGenreCommand command = new(_context);
            command.Model = new() {Name = "WhenValidInputsAreGiven_Genre_ShouldBeCreated" };

            //act
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            //assert
            Genre genre = _context.Genres.SingleOrDefault(g => g.Name == command.Model.Name);
            genre.Should().NotBeNull();
            
          

        }
    }
}
