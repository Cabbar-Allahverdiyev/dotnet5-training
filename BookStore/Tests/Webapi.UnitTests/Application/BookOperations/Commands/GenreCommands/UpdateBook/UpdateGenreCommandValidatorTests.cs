using AutoMapper;
using BookStoreWebApi.Application.GenreOperations.GetGenreDetail;
using BookStoreWebApi.Application.GenreOperations.UpdateGenre;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;
using FluentAssertions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSetup;
using Xunit;

namespace Webapi.UnitTests.Application.BookOperations.Commands.GenreCommands.UpdateBook
{
    public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateGenreCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }


        [Theory]
        [InlineData("a")]
        [InlineData("ab")]
        [InlineData("abc")]
        [InlineData("")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string genreName)
        {
            //arrange
            GetGenreDetailQuery query = new(_context, _mapper);
            query.GenreId = 1;
            GenreDetailViewModel genre = query.Handle();

            UpdateGenreCommand command = new(_context);
            command.GenreId = 1;
            command.Model = new() { Name = genreName };

            //act 
            UpdateGenreCommandValidator validator = new();
            ValidationResult errors = validator.Validate(command);

            //assert
            errors.Errors.Count.Should().BeGreaterThan(0);

        }
    }
}
