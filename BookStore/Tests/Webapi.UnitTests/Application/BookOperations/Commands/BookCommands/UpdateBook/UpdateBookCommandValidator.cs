using AutoMapper;
using BookStoreWebApi.Application.BookOperations.GetBookDetail;
using BookStoreWebApi.DBOperations;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSetup;
using Xunit;

namespace Webapi.UnitTests.Application.BookOperations.Commands.BookCommands.UpdateBook
{
    public class UpdateBookCommandValidator:IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBookCommandValidator(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Theory]
        [InlineData(0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnError(int id)
        {
            //arrange
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = id;

            GetBookDetailQueryValidator validationRules = new();
            var errors = validationRules.Validate(query);
            //act & assert

            errors.Errors.Count.Should().BeGreaterThan(0);


        }
    }
}
