using System.Collections.Generic;
using System.Threading.Tasks;
using CodeCoolAPI.Dtos;
using CodeCoolAPI.Helpers;

namespace CodeCoolAPI.Services
{
    public interface IMaterialService
    {
        Task<MaterialReadDto> ReadMaterialById(int id);
        Task<IEnumerable<MaterialReadDto>> ReadAllMaterials(string searchByTypeName, SortDirection sortByDateDirection);
        Task<MaterialReadDto> CreateMaterialReadDto(MaterialUpsertDto materialUpsertDto);
        Task UpdateMaterial(int id, MaterialUpsertDto materialUpsertDto);
        Task DeleteMaterial(int id);
    }
}