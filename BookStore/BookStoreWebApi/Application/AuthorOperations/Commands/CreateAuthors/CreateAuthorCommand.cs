using AutoMapper;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;
using System;
using System.Linq;

namespace BookStoreWebApi.Application.AuthorOperations.Commands.CreateAuthors
{
    public class CreateAuthorCommand
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorModel Model { get; set; }

        public CreateAuthorCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _context.Authors
                .SingleOrDefault(
                a=>a.FirstName+a.LastName==Model.FirstName+Model.LastName
                );
            if (author is not null)
            {
                throw new InvalidOperationException("Yazıcı movcuddur");
            }
            author = _mapper.Map<Author>(Model);
            _context.Authors.Add(author);
            _context.SaveChanges();

        }
    }

    public class CreateAuthorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
