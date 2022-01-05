using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BookStoreWebApi.Common;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWebApi.Application.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        public int BookId { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            Book book = _context.Books.Include(x => x.Genre).SingleOrDefault(x => x.Id == BookId);
            if (book is null)
                throw new InvalidOperationException("Kitab tapılmadı");
            Genre genre = _context.Genres.SingleOrDefault(g => g.Id == book.GenreId);
            //BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book); 
            BookDetailViewModel vm = new() { Genre = genre.Name, PageCount = book.PageCount, PublishDate = book.PublishDate, Title = book.Title };
            return vm;
        }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}