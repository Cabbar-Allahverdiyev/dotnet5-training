using BookStoreWebApi.Application.GenreOperations.CreateGenre;
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

namespace Webapi.UnitTests.Application.BookOperations.Commands.GenreCommands
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        public CreateGenreCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Theory]
        [InlineData("")]
        [InlineData("asd")]
        [InlineData("as")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string genreName)
        {
            //arrange
            CreateGenreCommand command = new(_context);
            command.Model = new CreateGenreModel() { Name = genreName };

            CreateGenreCommandValidator validator = new();
            ValidationResult errors = validator.Validate(command);

            //act & assert
            errors.Errors.Count.Should().BeGreaterThan(0);

        }


    }
}
