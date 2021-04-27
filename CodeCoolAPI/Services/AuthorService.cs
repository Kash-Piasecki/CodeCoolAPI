using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeCoolAPI.CustomExceptions;
using CodeCoolAPI.DAL.Models;
using CodeCoolAPI.DAL.UnitOfWork;
using CodeCoolAPI.Dtos;

namespace CodeCoolAPI.Services
{
    class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<AuthorReadDto> ReadAuthorById(int id)
        {
            var author = await FindAuthor(id);
            var authorReadDto = _mapper.Map<AuthorReadDto>(author);
            return authorReadDto;
        }

        public async Task<IEnumerable<AuthorReadDto>> ReadAllAuthors()
        {
            var authorList = await _unitOfWork.Authors.FindAll();
            if (!authorList.Any())
                throw new NotFoundException("Authors list is empty");

            var authorReadDtoList = _mapper.Map<IEnumerable<AuthorReadDto>>(authorList);
            return authorReadDtoList;
        }

        public async Task<AuthorReadDto> CreateAuthorReadDto(AuthorUpsertDto authorUpsertDtoDto)
        {
            var author = _mapper.Map<Author>(authorUpsertDtoDto);
            await _unitOfWork.Authors.Create(author);
            await _unitOfWork.Save();
            return _mapper.Map<AuthorReadDto>(author);
        }

        public async Task UpdateAuthor(int id, AuthorUpsertDto authorUpsertDto)
        {
            var author = await FindAuthor(id);
            _mapper.Map(authorUpsertDto, author);
            await _unitOfWork.Authors.Update(author);
            await _unitOfWork.Save();
        }

        public async Task<AuthorUpsertDto> PatchAuthor(Author author)
        {
            return await Task.Run(() => _mapper.Map<AuthorUpsertDto>(author));
        }

        public async Task DeleteAuthor(int id)
        {
            var author = await FindAuthor(id);
            await _unitOfWork.Authors.Delete(author);
            await _unitOfWork.Save();
        }

        public async Task MapPatch(Author author, AuthorUpsertDto dto)
        {
            _mapper.Map(dto, author);
            await _unitOfWork.Authors.Update(author);
            await _unitOfWork.Save();
        }

        public async Task<Author> FindAuthor(int id)
        {
            var author = await _unitOfWork.Authors.Find(id);
            if (author is null)
                throw new NotFoundException("Author not found");
            return author;
        }
        
    }
}