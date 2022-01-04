using BookStoreWebApi.Application.BookOperations.CreateBook;
using BookStoreWebApi.Application.BookOperations.DeleteBook;
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
    public class DeleteBookCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id)
        {
            //arrange
            DeleteBookCommand command = new(null);
            command.BookId = id;
            DeleteBookCommandValidator validator = new();
            var errors = validator.Validate(command);
            //act & assert
            errors.Errors.Count.Should().BeGreaterThan(0);
        }

    }
}
