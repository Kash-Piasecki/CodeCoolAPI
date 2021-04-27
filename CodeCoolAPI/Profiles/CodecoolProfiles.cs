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
            
            CreateMap<MaterialType, MaterialTypeReadDto>();
            CreateMap<MaterialTypeUpsertDto, MaterialType>();
            CreateMap<MaterialType, MaterialTypeUpsertDto>();
            
            CreateMap<Material, MaterialReadDto>()
                .ForMember(x => x.AuthorName, y => y.MapFrom(a => a.Author.Name))
                .ForMember(x => x.MaterialTypeName, y => y.MapFrom(m => m.MaterialType.Name));
            CreateMap<MaterialUpsertDto, Material>();
            CreateMap<Material, MaterialUpsertDto>();
        }
    }
}