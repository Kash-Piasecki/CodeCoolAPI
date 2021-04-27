using System.Collections.Generic;
using System.Threading.Tasks;
using CodeCoolAPI.Dtos;

namespace CodeCoolAPI.Services
{
    public interface IMaterialService
    {
        Task<MaterialReadDto> ReadMaterialById(int id);
        Task<IEnumerable<MaterialReadDto>> ReadAllMaterials();
        Task<MaterialReadDto> CreateMaterialReadDto(MaterialUpsertDto materialUpsertDto);
        Task UpdateMaterial(int id, MaterialUpsertDto materialUpsertDto);
        Task DeleteMaterial(int id);
    }
}