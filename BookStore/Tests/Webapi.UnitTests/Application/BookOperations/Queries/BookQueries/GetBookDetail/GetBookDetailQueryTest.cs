using AutoMapper;
using BookStoreWebApi.Application.BookOperations.GetBookDetail;
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

namespace Webapi.UnitTests.Application.BookOperations.Queries.BookQueries.GetBookDetail
{
    public class GetBookDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;

        }

        [Fact]
        public void WhenAlreadyNotExistBookIdGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            Book testBook = new Book() { Id = 800, Title = "Dune", GenreId = 2, PageCount = 540, PublishDate = new DateTime(2002, 05, 23) };

            _context.Books.Add(testBook);
            _context.SaveChanges();

            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = 404;

            //act
            //assert
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitab tapılmadı");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void WhenAlreadyExistBookIdGiven_Book_ShouldBeFind(int bookId)
        {  
            //arrange
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = bookId;
            //act
            Book book = _context.Books.SingleOrDefault(b => b.Id == bookId);
            //assert
            book.Should().NotBeNull();

        }
    }
}
