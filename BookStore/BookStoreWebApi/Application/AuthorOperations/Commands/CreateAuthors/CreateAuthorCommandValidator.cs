using BookStoreWebApi.Entities;
using FluentValidation;
using System;

namespace BookStoreWebApi.Application.AuthorOperations.Commands.CreateAuthors
{
    public class CreateAuthorCommandValidator:AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(a => a.Model.FirstName).NotEmpty();
            RuleFor(a => a.Model.LastName).NotEmpty();
            RuleFor(a => a.Model.DateOfBirth).NotEmpty();
            RuleFor(a => a.Model.DateOfBirth).LessThan(DateTime.Now);
        }
    }
}
