using System.Collections.Generic;
using System.Threading.Tasks;
using CodeCoolAPI.Dtos;
using CodeCoolAPI.Helpers;
using CodeCoolAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeCoolAPI.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<IEnumerable<MaterialReadDto>>> Get(string searchByTypeName, SortDirection sortByDateDirection)
        {
            var materialReadDtoList = await _materialService.ReadAllMaterials(searchByTypeName, sortByDateDirection);
            _logger.LogInformation(LogMessages.EntitiesFound);
            return Ok(materialReadDtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialTypeReadDto>> Get(int id)
        {
            var readMaterialById = await _materialService.ReadMaterialById(id);
            _logger.LogInformation(LogMessages.EntityFound);
            return Ok(readMaterialById);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Post(MaterialUpsertDto materialUpsertDto)
        {
            var materialReadDto = await _materialService.CreateMaterialReadDto(materialUpsertDto);
            _logger.LogInformation(LogMessages.EntityCreated);
            return CreatedAtAction(nameof(Get), new {materialReadDto.Id}, materialReadDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Put(int id, MaterialUpsertDto materialUpsertDto)
        {
            await _materialService.UpdateMaterial(id, materialUpsertDto);
            _logger.LogInformation(LogMessages.EntityUpdated);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _materialService.DeleteMaterial(id);
            _logger.LogInformation(LogMessages.EntityDeleted);
            return NoContent();
        }
    }
}