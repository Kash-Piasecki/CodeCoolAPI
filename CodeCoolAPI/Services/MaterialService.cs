using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeCoolAPI.CustomExceptions;
using CodeCoolAPI.DAL.Models;
using CodeCoolAPI.DAL.UnitOfWork;
using CodeCoolAPI.Dtos;
using CodeCoolAPI.Helpers;

namespace CodeCoolAPI.Services
{
    class MaterialService : IMaterialService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MaterialService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<MaterialReadDto> ReadMaterialById(int id)
        {
            var material = await FindMaterial(id);
            var materialReadDto = _mapper.Map<MaterialReadDto>(material);
            return materialReadDto;
        }

        public async Task<IEnumerable<MaterialReadDto>> ReadAllMaterials(string searchByTypeName, SortDirection sortByDateDirection)
        {
            var materialList = await GetMaterialList(searchByTypeName);
            if (!materialList.Any())
                throw new NotFoundException("Material list is empty");

            var materialDtoList = _mapper.Map<IEnumerable<MaterialReadDto>>(materialList);
            SortByDate(ref materialDtoList, sortByDateDirection);
            return materialDtoList;
        }

        private void SortByDate(ref IEnumerable<MaterialReadDto> materialDtoList, SortDirection sortByDateDirection)
        {
            if (sortByDateDirection is SortDirection.ASC)
                materialDtoList = materialDtoList.OrderBy(x => x.PublishTime);
            if (sortByDateDirection is SortDirection.DESC)
                materialDtoList = materialDtoList.OrderByDescending(x => x.PublishTime);
        }


        public async Task<MaterialReadDto> CreateMaterialReadDto(MaterialUpsertDto materialUpsertDto)
        {
            var material = _mapper.Map<Material>(materialUpsertDto);
            await _unitOfWork.Materials.Create(material);
            await _unitOfWork.Save();
            return _mapper.Map<MaterialReadDto>(material);
        }

        public async Task UpdateMaterial(int id, MaterialUpsertDto materialUpsertDto)
        {
            var material = await FindMaterial(id);
            _mapper.Map(materialUpsertDto, material);
            await _unitOfWork.Materials.Update(material);
            await _unitOfWork.Save();
        }

        public async  Task DeleteMaterial(int id)
        {
            var material = await FindMaterial(id);
            await _unitOfWork.Materials.Delete(material);
            await _unitOfWork.Save();
        }
        
        private async Task<Material> FindMaterial(int id)
        {
            var material = await _unitOfWork.Materials.Find(id);
            if (material is null)
                throw new NotFoundException("Material not found");
            return material;
        }
        private async Task<IEnumerable<Material>> GetMaterialList(string searchByTypeName)
        {
            IEnumerable<Material> materialList;
            if (!string.IsNullOrWhiteSpace(searchByTypeName))
                materialList = await _unitOfWork.Materials.FindAllByQuery(searchByTypeName);
            else
                materialList = await _unitOfWork.Materials.FindAll();
            return materialList;
        }
    }
}
