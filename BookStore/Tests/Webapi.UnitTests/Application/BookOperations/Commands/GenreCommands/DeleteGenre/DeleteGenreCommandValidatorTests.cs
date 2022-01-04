using BookStoreWebApi.Application.GenreOperations.DeleteGenre;
using BookStoreWebApi.DBOperations;
using FluentAssertions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSetup;
using Xunit;

namespace Webapi.UnitTests.Application.BookOperations.Commands.GenreCommands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        public DeleteGenreCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Theory]
        [InlineData(0)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBereturnErrors(int genreId)
        {
            //arrange
            DeleteGenreCommand command = new(_context) { GenreId = genreId };
            DeleteGenreCommandValidator validator = new();
            ValidationResult errors = validator.Validate(command);

            //act & assert
            errors.Errors.Count.Should().BeGreaterThan(0);
        }

    }
}
