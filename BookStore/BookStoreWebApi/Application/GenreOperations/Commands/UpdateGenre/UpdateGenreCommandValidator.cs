using System;
using FluentValidation;

namespace BookStoreWebApi.Application.GenreOperations.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(4).When(x => x.Model.Name != string.Empty);
            RuleFor(command => command.Model.Name).MinimumLength(4);
            RuleFor(command => command.Model.Name).NotEmpty();

        }
    }
}