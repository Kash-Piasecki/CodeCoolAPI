using System.Collections.Generic;
using System.Threading.Tasks;
using CodeCoolAPI.Dtos;
using CodeCoolAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeCoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorReadDto>>> Get()
        {
            var actorReadDtos = await _authorService.ReadAllAuthors();
            return Ok(actorReadDtos);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorReadDto>> Get(int id)
        {
            var readAuthorById = await _authorService.ReadAuthorById(id);
            return Ok(readAuthorById);
        }

        [HttpPost]
        public async Task<ActionResult> Post(AuthorUpsertDto authorUpsertDto)
        {
            var authorReadDto = await _authorService.CreateAuthorReadDto(authorUpsertDto);
            return CreatedAtAction(nameof(Get), new {authorReadDto.Id}, authorReadDto);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, AuthorUpsertDto authorUpsertDto)
        {
            await _authorService.UpdateAuthor(id, authorUpsertDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _authorService.DeleteAuthor(id);
            return NoContent();
        }
    }
}