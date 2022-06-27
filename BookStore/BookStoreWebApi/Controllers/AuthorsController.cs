using AutoMapper;
using BookStoreWebApi.Application.AuthorOperations.Commands.CreateAuthors;
using BookStoreWebApi.Application.AuthorOperations.Commands.DeleteAuthors;
using BookStoreWebApi.Application.AuthorOperations.GetAuthorDetail;
using BookStoreWebApi.Application.AuthorOperations.GetAuthors;
using BookStoreWebApi.Application.AuthorOperations.UpdateAuthors;
using BookStoreWebApi.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public AuthorsController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //Get All 
        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery command = new GetAuthorsQuery(_context, _mapper);
            var obj = command.Handle();
            return Ok(obj);
        }

        //Get  With FromRoute
        [HttpGet("{id}")]
        public IActionResult GetAuthorById(int id)
        {
            AuthorDetailViewModel obj;

            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context,_mapper);
            query.AuthorId = id;

            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            validator.ValidateAndThrow(query);

            obj = query.Handle();
            return Ok(obj);
        }


        //Update 
        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updateAuthor)
        {

            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = id;
            command.Model = updateAuthor;

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();

        }

        //Add Book
        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor)
        {

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = newAuthor;

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = id;

            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
    }
}
