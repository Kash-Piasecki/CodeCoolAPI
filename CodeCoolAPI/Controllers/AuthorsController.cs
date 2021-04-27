using System.Collections.Generic;
using System.Threading.Tasks;
using CodeCoolAPI.Dtos;
using CodeCoolAPI.Services;
using Microsoft.AspNetCore.JsonPatch;
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
        
        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<AuthorUpsertDto> patchDocument)
        {
            var author = await _authorService.FindAuthor(id);
            var authorToPatch = await _authorService.PatchAuthor(author);
            patchDocument.ApplyTo(authorToPatch, ModelState);
            if (!TryValidateModel(authorToPatch))
                return ValidationProblem(ModelState);
            await _authorService.MapPatch(author, authorToPatch);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _authorService.DeleteAuthor(id);
            return NoContent();
        }
    }
}