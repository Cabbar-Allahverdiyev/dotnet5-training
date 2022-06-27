using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BookStoreWebApi.Common;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWebApi.Application.AuthorOperations.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorsQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            var authorsList = _context.Authors.OrderBy(x => x.Id);

            List<AuthorsViewModel> obj = _mapper.Map<List<AuthorsViewModel>>(authorsList);
           
            return obj;
        }
    }

    public class AuthorsViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
       // public List<Book> Books { get; set; }
        public DateTime DateOFBirth { get; set; }
    }
}