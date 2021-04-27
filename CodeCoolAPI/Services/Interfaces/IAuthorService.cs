using System.Collections.Generic;
using System.Threading.Tasks;
using CodeCoolAPI.Dtos;

namespace CodeCoolAPI.Services
{
    public interface IAuthorService
    {
        Task<AuthorReadDto> ReadAuthorById(int id);
        Task<IEnumerable<AuthorReadDto>> ReadAllAuthors();
        Task<AuthorReadDto> CreateAuthorReadDto(AuthorUpsertDto authorUpsertDtoDto);
        Task UpdateAuthor(int id, AuthorUpsertDto authorUpsertDto);
        Task DeleteAuthor(int id);
    }
}