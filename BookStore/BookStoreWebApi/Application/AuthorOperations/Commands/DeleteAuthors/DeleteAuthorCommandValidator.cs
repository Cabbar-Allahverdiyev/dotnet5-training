using BookStoreWebApi.Application.AuthorOperations.Commands.DeleteAuthors;
using BookStoreWebApi.Entities;
using FluentValidation;
using System;

namespace BookStoreWebApi.Application.AuthorOperations.Commands.CreateAuthors
{
    public class DeleteAuthorCommandValidator:AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(a => a.AuthorId).NotEmpty();
          
        }
    }
}
