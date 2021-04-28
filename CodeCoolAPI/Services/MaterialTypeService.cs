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
    internal class MaterialTypeService : IMaterialTypeService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MaterialTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MaterialTypeReadDto> ReadMaterialTypeById(int id)
        {
            var materialType = await FindMaterialType(id);
            var materialTypeReadDto = _mapper.Map<MaterialTypeReadDto>(materialType);
            return materialTypeReadDto;
        }

        public async Task<IEnumerable<MaterialTypeReadDto>> ReadAllMaterialTypes()
        {
            var materialTypeList = await _unitOfWork.MaterialTypes.FindAll();
            if (!materialTypeList.Any())
                throw new NotFoundException("Material Types list is empty");

            var materialTypeDtoList = _mapper.Map<IEnumerable<MaterialTypeReadDto>>(materialTypeList);
            return materialTypeDtoList;
        }

        public async Task<MaterialTypeReadDto> CreateMaterialTypeReadDto(MaterialTypeUpsertDto materialTypeUpsertDto)
        {
            var materialType = _mapper.Map<MaterialType>(materialTypeUpsertDto);
            await _unitOfWork.MaterialTypes.Create(materialType);
            await _unitOfWork.Save();
            return _mapper.Map<MaterialTypeReadDto>(materialType);
        }

        public async Task UpdateMaterialType(int id, MaterialTypeUpsertDto materialTypeUpsertDto)
        {
            var materialType = await FindMaterialType(id);
            _mapper.Map(materialTypeUpsertDto, materialType);
            await _unitOfWork.MaterialTypes.Update(materialType);
            await _unitOfWork.Save();
        }

        public async Task DeleteMaterialType(int id)
        {
            var materialType = await FindMaterialType(id);
            await _unitOfWork.MaterialTypes.Delete(materialType);
            await _unitOfWork.Save();
        }

        private async Task<MaterialType> FindMaterialType(int id)
        {
            var materialType = await _unitOfWork.MaterialTypes.Find(id);
            if (materialType is null)
                throw new NotFoundException("Material Type not found");
            return materialType;
        }
    }
}