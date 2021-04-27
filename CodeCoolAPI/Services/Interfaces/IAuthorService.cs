using System.Collections.Generic;
using System.Threading.Tasks;
using CodeCoolAPI.DAL.Models;
using CodeCoolAPI.Dtos;

namespace CodeCoolAPI.Services
{
    public interface IAuthorService
    {
        Task<Author> FindAuthor(int id);
        Task<AuthorReadDto> ReadAuthorById(int id);
        Task<IEnumerable<AuthorReadDto>> ReadAllAuthors();
        Task<AuthorReadDto> CreateAuthorReadDto(AuthorUpsertDto authorUpsertDtoDto);
        Task UpdateAuthor(int id, AuthorUpsertDto authorUpsertDto);
        Task<AuthorUpsertDto> PatchAuthor(Author author);
        Task DeleteAuthor(int id);
        Task MapPatch(Author author, AuthorUpsertDto dto);
    }
}