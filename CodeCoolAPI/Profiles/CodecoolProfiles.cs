using AutoMapper;
using CodeCoolAPI.DAL.Models;
using CodeCoolAPI.Dtos;
using CodeCoolAPI.Dtos.MaterialType;

namespace CodeCoolAPI.Profiles
{
    public class CodecoolProfiles : Profile
    {
        public CodecoolProfiles()
        {
            CreateMap<Author, AuthorReadDto>();
            CreateMap<AuthorUpsertDto, Author>();
            CreateMap<Author, AuthorUpsertDto>();
            
            CreateMap<MaterialType, MaterialTypeReadDto>();
            CreateMap<MaterialTypeUpsertDto, MaterialType>();
            CreateMap<MaterialType, MaterialTypeUpsertDto>();
        }
    }
}