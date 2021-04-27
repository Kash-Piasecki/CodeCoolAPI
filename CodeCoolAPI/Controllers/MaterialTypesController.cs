using System.Collections.Generic;
using System.Threading.Tasks;
using CodeCoolAPI.Dtos;
using CodeCoolAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeCoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialTypesController : ControllerBase
    {
        private readonly IMaterialTypeService _materialTypeService;

        public MaterialTypesController(IMaterialTypeService materialTypeService)
        {
            _materialTypeService = materialTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialTypeReadDto>>> Get()
        {
            var materialTypeReadDtoList = await _materialTypeService.ReadAllMaterialTypes();
            return Ok(materialTypeReadDtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialTypeReadDto>> Get(int id)
        {
            var readMaterialTypeById = await _materialTypeService.ReadMaterialTypeById(id);
            return Ok(readMaterialTypeById);
        }

        [HttpPost]
        public async Task<ActionResult> Post(MaterialTypeUpsertDto materialTypeUpsertDto)
        {
            var materialTypeReadDto = await _materialTypeService.CreateMaterialTypeReadDto(materialTypeUpsertDto);
            return CreatedAtAction(nameof(Get), new {materialTypeReadDto.Id}, materialTypeReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, MaterialTypeUpsertDto materialTypeUpsertDto)
        {
            await _materialTypeService.UpdateMaterialType(id, materialTypeUpsertDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _materialTypeService.DeleteMaterialType(id);
            return NoContent();
        }
    }
}