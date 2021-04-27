using System.Collections.Generic;
using System.Threading.Tasks;
using CodeCoolAPI.Dtos;

namespace CodeCoolAPI.Services
{
    public interface IMaterialTypeService
    {
        Task<MaterialTypeReadDto> ReadMaterialTypeById(int id);
        Task<IEnumerable<MaterialTypeReadDto>> ReadAllMaterialTypes();
        Task<MaterialTypeReadDto> CreateMaterialTypeReadDto(MaterialTypeUpsertDto materialTypeUpsertDto);
        Task UpdateMaterialType(int id, MaterialTypeUpsertDto materialTypeUpsertDto);
        Task DeleteMaterialType(int id);
    }
}