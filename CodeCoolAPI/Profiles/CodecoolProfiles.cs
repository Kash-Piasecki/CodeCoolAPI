using AutoMapper;
using CodeCoolAPI.DAL.Models;
using CodeCoolAPI.Dtos;

namespace CodeCoolAPI.Profiles
{
    public class CodecoolProfiles : Profile
    {
        public CodecoolProfiles()
        {
            CreateMap<Author, AuthorReadDto>();
            CreateMap<AuthorUpsertDto, Author>();
            CreateMap<Author, AuthorUpsertDto>();
        }
    }
}