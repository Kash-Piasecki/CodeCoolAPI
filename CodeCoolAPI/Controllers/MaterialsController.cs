using System.Collections.Generic;
using System.Threading.Tasks;
using CodeCoolAPI.Dtos;
using CodeCoolAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeCoolAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialsController : ControllerBase
    {
        private readonly IMaterialService _materialService;
        private readonly ILogger _logger;

        public MaterialsController(IMaterialService materialService, ILogger<MaterialsController> logger)
        {
            _materialService = materialService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialReadDto>>> Get()
        {
            var materialReadDtoList = await _materialService.ReadAllMaterials();
            _logger.LogInformation("Entities list found");
            return Ok(materialReadDtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialTypeReadDto>> Get(int id)
        {
            var readMaterialById = await _materialService.ReadMaterialById(id);
            _logger.LogInformation("Entity found");
            return Ok(readMaterialById);
        }

        [HttpPost]
        public async Task<ActionResult> Post(MaterialUpsertDto materialUpsertDto)
        {
            var materialReadDto = await _materialService.CreateMaterialReadDto(materialUpsertDto);
            _logger.LogInformation("Entity created");
            return CreatedAtAction(nameof(Get), new {materialReadDto.Id}, materialReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, MaterialUpsertDto materialUpsertDto)
        {
            await _materialService.UpdateMaterial(id, materialUpsertDto);
            _logger.LogInformation("Entity updated");
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _materialService.DeleteMaterial(id);
            _logger.LogInformation("Entity deleted");
            return NoContent();
        }
    }
}