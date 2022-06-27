using AutoMapper;
using BookStoreWebApi.Application.BookOperations.DeleteBook;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreWebApi.Application.AuthorOperations.Commands.DeleteAuthors
{
    public class DeleteAuthorCommand
    {
        private readonly IBookStoreDbContext _context;

        public int AuthorId { get; set; }

        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(a => a.Id == AuthorId);
            if (author is null)
            {
                throw new InvalidOperationException("Yazıcı movcud deyil");
            }

            List<Book> books = _context.Books.Where(b => b.AuthorId == author.Id).ToList();
                DeleteBookCommand bookDeleteCommand = new DeleteBookCommand(_context);
            foreach (var book in books)
            {
                bookDeleteCommand.BookId = book.Id;
                bookDeleteCommand.Handle();
            }

            _context.Authors.Remove(author);
            _context.SaveChanges();

        }
    }
}
