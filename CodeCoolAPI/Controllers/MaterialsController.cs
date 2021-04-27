using System.Collections.Generic;
using System.Threading.Tasks;
using CodeCoolAPI.Dtos;
using CodeCoolAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeCoolAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialsController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        public MaterialsController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialReadDto>>> Get()
        {
            var materialReadDtoList = await _materialService.ReadAllMaterials();
            return Ok(materialReadDtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialTypeReadDto>> Get(int id)
        {
            var readMaterialById = await _materialService.ReadMaterialById(id);
            return Ok(readMaterialById);
        }

        [HttpPost]
        public async Task<ActionResult> Post(MaterialUpsertDto materialUpsertDto)
        {
            var materialReadDto = await _materialService.CreateMaterialReadDto(materialUpsertDto);
            return CreatedAtAction(nameof(Get), new {materialReadDto.Id}, materialReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, MaterialUpsertDto materialUpsertDto)
        {
            await _materialService.UpdateMaterial(id, materialUpsertDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _materialService.DeleteMaterial(id);
            return NoContent();
        }
    }
}