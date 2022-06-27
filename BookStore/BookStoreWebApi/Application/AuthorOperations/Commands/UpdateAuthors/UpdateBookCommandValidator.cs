using System;
using FluentValidation;

namespace BookStoreWebApi.Application.AuthorOperations.UpdateAuthors
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.AuthorId).GreaterThan(0);
            RuleFor(command => command.Model.FirstName).NotEmpty();
            RuleFor(command => command.Model.LastName).NotEmpty();
            RuleFor(command => command.Model.DateOfBirth).NotEmpty();
            RuleFor(command => command.Model.DateOfBirth.Date).NotEmpty().LessThan(DateTime.Now.Date);
        }
    }
}